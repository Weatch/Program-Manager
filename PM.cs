using Newtonsoft.Json.Linq;
using PM.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Deployment.Internal;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PM
{
    public partial class PM : Form
    {
        private static JArray AppArray;
        private TreeNode sourceNode; // 用于保存拖拽的源节点
        private HotkeyManager _hotkeyManager;
        private static string TreeViewKeyPress = string.Empty;
        private static PM _instance;
        private Thread _ipcListenerThread;
        private volatile bool _listenerRunning;
        private CancellationTokenSource _cts;
        public PM()
        {
            _instance = this;
            InitializeComponent();
            InitCustom();
            InitHotkey();
            StartIpcListener();
        }

        private void InitCustom()
        {
            notifyIcon.Text = AppConf.AppName;
            this.TopMost = AppConf.TopMost;
            if (AppConf.LineSpace) { treeView.ItemHeight = AppConf.LineSpaceValue; }
            if (AppConf.FontSize) { treeView.Font = new Font("微软雅黑", AppConf.FontSizeValue); }
            if (AppConf.IconSize)
            {
                if (AppConf.IconSizeValue == 32)
                {
                    treeView.ImageList = LImageList;
                }
                else
                {
                    treeView.ImageList = SImageList;
                }
            }
            RefreshImageList();
            RefreshTreeView();
        }
        private void RefreshImageList()
        {
            LImageList.Images.Clear();
            SImageList.Images.Clear();
            Icon icon = Properties.Resources.folder;
            LImageList.Images.Add("folder", icon);
            SImageList.Images.Add("folder", icon);
            icon = Properties.Resources._default;
            LImageList.Images.Add("default", icon);
            SImageList.Images.Add("default", icon);
            icon = Properties.Resources.mfc;
            LImageList.Images.Add("mfc", icon);
            SImageList.Images.Add("mfc", icon);

            var sql = new SQLite(AppConf.DbPath);
            string json = sql.ExecuteJson("SELECT * FROM lnk");
            sql = null;
            AppArray = JArray.Parse(json);
            foreach (JToken item in AppArray)
            {
                string str = item["icon"].ToString();
                if (!string.IsNullOrWhiteSpace(str))//if (!string.IsNullOrEmpty(str))
                {
                    if (File.Exists(AppConf.AppPath + AppConf.IconPath + @"\" + item["icon"].ToString()))
                    {
                        Icon ico = new Icon(AppConf.AppPath + AppConf.IconPath + @"\" + item["icon"].ToString());
                        LImageList.Images.Add(item["name"].ToString(), ico);
                        SImageList.Images.Add(item["name"].ToString(), ico);
                    }                    
                }
            }
        }
        private void RefreshTreeView()
        {            
            treeView.Nodes.Clear();
            treeView.BeginUpdate();
            int sucFilePath = 0;
            int errFilePath = 0;
            var sql = new SQLite(AppConf.DbPath);
            string json = sql.ExecuteJson("SELECT * FROM lnk");
            AppArray = JArray.Parse(json);
            sql = null;
            sql = new SQLite(AppConf.DbPath);
            foreach (string appClass in AppConf.AppClass)
            {
                TreeNode treeNode = new TreeNode(appClass)
                {
                    ImageKey = AppConf.FolderIcon,
                    SelectedImageKey = AppConf.FolderIcon,
                    Tag = appClass
                };
                var AppClassArray = AppArray.SelectTokens("$[?(@.class=='" + appClass + "')]");
                foreach (var item in AppClassArray)
                {
                    string filePath = item["path"].ToString();
                    if (File.Exists(filePath))
                    {
                        sucFilePath++;
                        int ind = LImageList.Images.IndexOfKey(item["name"].ToString());
                        if (ind == -1)
                        {
                            ind = LImageList.Images.IndexOfKey("default");
                        }
                        TreeNode child = new TreeNode(item["name"].ToString(), ind, ind)
                        {
                            Tag = item["id"].ToString()
                        };
                        treeNode.Nodes.Add(child);
                    }
                    else
                    {
                        errFilePath++;
                    }
                }
                LabelStripStatus.Text = "正确显示:" + sucFilePath.ToString() + "条,错误信息:" + errFilePath.ToString() + "条";
                treeView.Nodes.Add(treeNode);
            }
            treeView.EndUpdate();
            sql = null;
        }
        
        private void InitHotkey()
        {
            if (AppConf.HotKey)
            {
                _hotkeyManager = new HotkeyManager(1);
                _hotkeyManager.HotkeyPressed += () =>
                {
                    if (this.ShowInTaskbar)
                    {
                        PM_Show(false);
                    }
                    else
                    {
                        PM_Show(true);
                    }
                };
                bool bHotStart = _hotkeyManager.Start(AppConf.HotKeyCode);
                if (!bHotStart) 
                { 
                    LabelStripStatus.Text = "热键开启失败,已停止热键使用"; 
                    AppConf.HotKey = false; 
                }
            }
        }

        private void PM_Show(bool b)
        {
            if (b)
            {                
                this.Visible = true;
                if (this.WindowState == FormWindowState.Minimized) { this.WindowState = FormWindowState.Normal; }
                this.ShowInTaskbar = true;
                this.Show();
                this.Activate();
                this.BringToFront();                
            }
            else
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }
        private void RunApp(string path, int Auth=0,string WorkDir=null,string Arguments = null)
        {
            bool bExt = true;
            string ext = Path.GetExtension(path).ToUpper();
            if (ext==".EXE" || ext == ".MSI") { bExt = false; }
            if (Auth == 1) { bExt = true; }
            //Console.WriteLine($"扩展名：{ext}，运行方式：UseShellExecute = {bExt}");
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,
                Arguments = Arguments ?? string.Empty,
                WorkingDirectory = WorkDir ?? string.Empty,
                Verb = Auth == 0 ? string.Empty : "runas",
                UseShellExecute = bExt
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                LabelStripStatus.Text= ("需要权限提升: " + ex.Message);
            }
            /*
            try
            {
                if (WorkDir!="" && Arguments != "")
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        WorkingDirectory = WorkDir,
                        Arguments = Arguments,
                        UseShellExecute = bExt
                    };
                    Process.Start(startInfo);
                }
                if (WorkDir == "" && Arguments != "")
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = Arguments,
                        UseShellExecute = bExt
                    };
                    Process.Start(startInfo);
                }
                if (WorkDir != "" && Arguments == "")
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        WorkingDirectory = WorkDir,
                        UseShellExecute = bExt
                    };
                    Process.Start(startInfo);
                }
                if (WorkDir == "" && Arguments == "") 
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = bExt
                    });
                }

            }
            catch (Exception ex)
            {
                LabelStripStatus.Text = $"错误:{ex.Message}";
            }
            */
        }
        private TreeNode GetCurNode()
        {
            TreeNode cur = treeView.SelectedNode;
            return cur;
        }
        private string SetStatusText(string text)
        {
            int maxWidth = StatusStrip.Width - 20;
            string txt = text;
            using (Graphics g = StatusStrip.CreateGraphics())
            {
                if (g.MeasureString(text, StatusStrip.Font).Width > maxWidth)
                {
                    string shortText = text;
                    while (g.MeasureString(shortText + "...", StatusStrip.Font).Width > maxWidth && shortText.Length > 3)
                    {
                        shortText = shortText.Substring(0, shortText.Length - 1);
                    }
                    txt = shortText + "...";
                }
            }
            return txt;
        }
        private void UpSysSql(string key,string value)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = $"UPDATE sys SET value=@value WHERE key='{key}'";
            var parameters = new SQLiteParameter[] { new SQLiteParameter("@value", value) };
            sql.ExecuteNonQuery(strsql, parameters);
        }
        public void ExitMe()
        {
            UpSysSql("Top", this.Location.Y.ToString());
            UpSysSql("Left", this.Location.X.ToString());
            UpSysSql("Width", this.Width.ToString());
            UpSysSql("Height", this.Height.ToString());

            _hotkeyManager?.Dispose();
            notifyIcon.Visible = false;
            notifyIcon.Dispose();

            _cts?.Cancel();
            // 等待线程退出（最多1秒）
            if (_ipcListenerThread != null && _ipcListenerThread.IsAlive)
            {
                if (!_ipcListenerThread.Join(1000))
                {
                    Console.WriteLine("IPC 监听线程仍在运行，强制关闭...");
                    // 由于是后台线程，应用程序退出时会自动终止
                }
            }

            this.Visible = false;
            Application.Exit();
            Environment.Exit(0);
            Process.GetCurrentProcess().Kill();
        }
        private void PM_Load(object sender, EventArgs e)
        {
            if (AppConf.X != 0 && AppConf.Y != 0)
            {
                this.Location = new Point(AppConf.X, AppConf.Y);
            }
            this.Width = AppConf.Width;
            this.Height = AppConf.Height;
            if (AppConf.StartUpMin) { PM_Show(false); }
            if (AppConf.WinStartMin) { PM_Show(false); AppConf.WinStartMin = false; }
        }

        private void PM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppConf.xExit)
            {
                e.Cancel = true;
                this.Visible = false;
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else
            {
                ExitMe();
                base.OnFormClosing(e);
            }
        }
        private void PM_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
            base.OnFormClosed(e);
        }

        private void ExitMenu_Click(object sender, EventArgs e)
        {
            ExitMe();
        }

        private void AboutMenu_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog(this);
        }

        private void AddTreeMenu_Click(object sender, EventArgs e)
        {
            TreeNode dNode = GetCurNode();
            string appClass;
            while (dNode.Parent != null)
            {
                dNode = dNode.Parent;
            }
            appClass = dNode.Text;
            AddProg addProg = new AddProg();
            addProg.ShowDialog(this);
            RefreshImageList();
            RefreshTreeView();
        }
        private void AddRootMenu_Click(object sender, EventArgs e)
        {
            string newNode = "新的标签";
            bool repeat = AppConf.AppClass.Contains(newNode);
            if (repeat) { newNode = "新的标签2"; }
            for(int i = 2; i <= 20; i++)
            {
                repeat = AppConf.AppClass.Contains("新的标签" + i.ToString());
                if (repeat) { newNode = "新的标签" + (i + 1).ToString(); }

            }
            TreeNode treeNode = new TreeNode(newNode)
            {
                ImageKey = AppConf.FolderIcon,
                SelectedImageKey = AppConf.FolderIcon,
                Tag = newNode
            };
            treeView.Nodes.Add(treeNode);
            var list = new List<string>(AppConf.AppClass);
            list.Add(newNode);
            AppConf.AppClass = list.ToArray();
            string result = string.Join(",", AppConf.AppClass);
            UpSysSql("Class",result);
        }

        private void QueryBox_TextChanged(object sender, EventArgs e)
        {
            string str = QueryBox.Text.Trim();
            treeView.Nodes.Clear();
            if (str == "")
            {
                RefreshTreeView();
            }
            else
            {
                var QueryArray = AppArray.Where(item =>
                    ((string)item["name"] ?? "").IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0);
                int sucFilePath = 0;
                int errFilePath = 0;
                foreach (var item in QueryArray)
                {
                    string filePath = item["path"].ToString();
                    if (File.Exists(filePath))
                    {
                        int ind = LImageList.Images.IndexOfKey(item["name"].ToString());
                        if (ind == -1)
                        {
                            ind = LImageList.Images.IndexOfKey("default");
                        }
                        TreeNode child = new TreeNode(item["name"].ToString(), ind, ind)
                        {
                            Tag = item["id"].ToString()
                        };
                        treeView.Nodes.Add(child);
                        sucFilePath++;
                    }
                    else
                    {
                        errFilePath++;                        
                    }                        
                }
                LabelStripStatus.Text = "查询到:"+sucFilePath.ToString()+"条,错误信息:" + errFilePath.ToString() + "条";
            }
        }
        private void OptionMenu_Click(object sender, EventArgs e)
        {
            if (AppConf.HotKey) _hotkeyManager.Dispose();
            Option option = new Option();
            option.ShowDialog(this);
            InitHotkey();
            if (AppConf.LineSpace) { treeView.ItemHeight = AppConf.LineSpaceValue; }
            if (AppConf.FontSize) { treeView.Font= new Font("微软雅黑", AppConf.FontSizeValue); }
            if (AppConf.IconSize)
            {
                if (AppConf.IconSizeValue == 32)
                {
                    treeView.ImageList = LImageList;
                }
                else
                {
                    treeView.ImageList = SImageList;
                }
            }
            this.TopMost = AppConf.TopMost;
            if (AppConf.AdminMode)
            {
                AddRootMenu.Visible = true;
                EditRootMenu.Visible = true;
                DelRootMenu.Visible = true;
                AddTreeMenu.Visible = true;
                EditTreeMenu.Visible = true;
                DelTreeMenu.Visible = true;
            }
            else
            {
                AddRootMenu.Visible = false;
                EditRootMenu.Visible = false;
                DelRootMenu.Visible = false;
                AddTreeMenu.Visible = false;
                EditTreeMenu.Visible = false;
                DelTreeMenu.Visible = false;
            }
        }
        private void OptionRootMenu_Click(object sender, EventArgs e)
        {
            if (AppConf.HotKey) _hotkeyManager.Dispose();
            Option option = new Option();
            option.ShowDialog(this);
            InitHotkey();
            if(AppConf.LineSpace){ treeView.ItemHeight = AppConf.LineSpaceValue; }
            if (AppConf.FontSize) { treeView.Font = new Font("微软雅黑", AppConf.FontSizeValue); }
            if (AppConf.IconSize) 
            {
                if (AppConf.IconSizeValue == 32)
                {
                    treeView.ImageList = LImageList;
                }
                else
                {
                    treeView.ImageList = SImageList;
                }
            }
            this.TopMost = AppConf.TopMost;
            if (AppConf.AdminMode)
            {
                AddRootMenu.Visible = true;
                EditRootMenu.Visible = true;
                DelRootMenu.Visible = true;
                AddTreeMenu.Visible = true;
                EditTreeMenu.Visible = true;
                DelTreeMenu.Visible = true;
            }
            else
            {
                AddRootMenu.Visible =   false;
                EditRootMenu.Visible =  false;
                DelRootMenu.Visible =   false;
                AddTreeMenu.Visible =   false;
                EditTreeMenu.Visible =  false;
                DelTreeMenu.Visible =   false;
            }
        }

        private void RefreshRootMenu_Click(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        private void ShowMenu_Click(object sender, EventArgs e)
        {
            if (this.ShowInTaskbar)
            {
                PM_Show(false);
            }
            else
            {
                PM_Show(true);
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PM_Show(true);
            }
        }

        private void OpenTreeMenu_Click(object sender, EventArgs e)
        {
            TreeNode curNode = GetCurNode();
            string CurID = curNode.Tag.ToString().Trim();
            var result = AppArray.FirstOrDefault(item => (string)item["id"] == CurID);
            if (result != null)
            {
                if (AppConf.OpenMin)
                {
                    PM_Show(false);
                }
                RunApp(result["path"].ToString(), int.Parse(result["auth"].ToString()), result["workdir"].ToString(), result["arguments"].ToString());
                if (AppConf.OpenClose)
                {
                    ExitMe();
                }                
            }
        }

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    LabelStripStatus.Text = "";
                    e.Node.Toggle();
                    treeView.SelectedNode = null;
                }
                else
                {
                    TreeNode ClickedNode = e.Node;
                    string str = ClickedNode.Tag.ToString().Trim();
                    var result = AppArray.FirstOrDefault(item => (string)item["id"] == str);
                    
                    if (result == null)
                    {
                        LabelStripStatus.Text = "";
                        LabelStripStatus.ToolTipText = "";
                    }
                    else
                    {
                        LabelStripStatus.ToolTipText = result["description"].ToString();
                        LabelStripStatus.Text = SetStatusText(result["description"].ToString());
                    }
                    if (!AppConf.DoubleClick)
                    {
                        if (result != null)
                        {
                            if (AppConf.OpenMin)
                            {
                                PM_Show(false);
                            }
                            RunApp(result["path"].ToString(), int.Parse(result["auth"].ToString()), result["workdir"].ToString(), result["arguments"].ToString());
                            if (AppConf.OpenClose)
                            {
                                ExitMe();
                            }                            
                        }
                    }
                }
            }

        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (AppConf.DoubleClick)
                {
                    TreeNode doubleClickedNode = e.Node;
                    string str = doubleClickedNode.Tag.ToString().Trim();
                    var result = AppArray.FirstOrDefault(item => (string)item["id"] == str);
                    if (result != null)
                    {
                        if (AppConf.OpenMin)
                        {
                            PM_Show(false);
                        }
                        RunApp(result["path"].ToString(), int.Parse(result["auth"].ToString()), result["workdir"].ToString(), result["arguments"].ToString());
                        if (AppConf.OpenClose)
                        {
                            ExitMe();
                        }                        
                    }
                }
            }
        }

        private void treeView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = treeView.GetNodeAt(e.X, e.Y);
                if (node == null)
                {
                    TreeViewRootMenu.Show(treeView, e.Location);
                }
                else
                {
                    treeView.SelectedNode = node;
                    //CurNode = node.Text;
                    //CurNodeTag = node.Tag.ToString().Trim();
                    //CurTreeNode = node;
                    if (node.Level == 0)
                    {
                        TreeViewRootMenu.Show(treeView, e.Location);
                    }
                    else
                    {                        
                        TreeViewMenu.Show(treeView, e.Location);
                    }
                }
            }
        }

        private void EditTreeMenu_Click(object sender, EventArgs e)
        {
            TreeNode curNode = GetCurNode();
            string curTag = curNode.Tag.ToString().Trim();
            var result = AppArray.FirstOrDefault(item => (string)item["id"] ==curTag );
            if (result != null)
            {
                AddProg ap = new AddProg("edit", curTag);
                ap.ShowDialog(this);
                RefreshImageList();
                RefreshTreeView();
            }
        }

        private void DelTreeMenu_Click(object sender, EventArgs e)
        {
            TreeNode curNode = GetCurNode();
            string CurID = curNode.Tag.ToString().Trim();
            MessBox mb = new MessBox(true,"删除了无法恢复","确认吗？");
            if (mb.ShowDialog(this) == DialogResult.OK)
            {
                var sql = new SQLite(AppConf.DbPath);
                sql.ExecuteNonQuery("DELETE FROM lnk WHERE id='" + CurID + "'");
                RefreshImageList();
                RefreshTreeView();
            }
        }

        private void DelRootMenu_Click(object sender, EventArgs e)
        {
            LabelStripStatus.Text = "";
            TreeNode curNode = GetCurNode();
            string cn= curNode.Text.ToString().Trim();
            if (curNode.Nodes.Count < 1)
            {
                curNode.Remove();
                AppConf.AppClass = AppConf.AppClass.Where(f => f != cn).ToArray();
                string result = string.Join(",", AppConf.AppClass);
                UpSysSql("Class", result);
                LabelStripStatus.Text = cn + "-已经删除！";
            }
            else
            {
                LabelStripStatus.Text = cn + "-不是空的目录,无法删除！";
            }
        }

        private void EditRootMenu_Click(object sender, EventArgs e)
        {
            treeView.LabelEdit = true;
            if (treeView.SelectedNode != null)
            {
                treeView.SelectedNode.BeginEdit();
            }
        }

        private void QueryBox_Click(object sender, EventArgs e)
        {
            QueryBox.SelectAll();
        }
        public TreeNode GetRootNode(TreeNode node)
        {
            while (node.Parent != null)
            {
                node = node.Parent;
            }
            return node;
        }
        private bool IsParentNode(TreeNode parentNode, TreeNode childNode)
        {
            TreeNode node = childNode.Parent;
            while (node != null)
            {
                if (node == parentNode) return true;
                node = node.Parent;
            }
            return false;
        }
        private bool IsChildNode(TreeNode parent, TreeNode child)
        {
            while (child != null)
            {
                if (child.Parent == parent) return true;
                child = child.Parent;
            }
            return false;
        }
        private bool IsRootNode(TreeNode node)
        {
            return node.Parent == null;
        }
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sourceNode = (TreeNode)e.Item;
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            treeView.LabelEdit = false;
            string OldClass = e.Node.Text;
            if (e.Label != null)  // 检查用户是否输入了新文本（不是取消编辑）
            {
                if (e.Label.Length > 0)  // 检查新文本是否为空
                {
                    // 在这里处理有效的编辑
                    e.Node.Text = e.Label;  // 这行通常会自动执行，但你可以在这里添加额外逻辑
                    string NewClass = e.Label;
                    for (int i = 0; i < AppConf.AppClass.Length; i++)
                    {
                        if (AppConf.AppClass[i] == OldClass)
                        {
                            AppConf.AppClass[i] = e.Label;
                        }
                    }
                    string result = string.Join(",", AppConf.AppClass);
                    var sql = new SQLite(AppConf.DbPath);
                    string strsql = "UPDATE sys SET value=@result WHERE key='Class'";
                    var parameters = new SQLiteParameter[] { new SQLiteParameter("@result", result) };
                    sql.ExecuteNonQuery(strsql, parameters);
                    strsql = "UPDATE lnk SET class=@NewClass WHERE class='" + OldClass + "'";
                    parameters = new SQLiteParameter[] { new SQLiteParameter("@NewClass", NewClass) };
                    sql.ExecuteNonQuery(strsql, parameters);
                }
                else
                {
                    LabelStripStatus.Text = "节点文本不能为空";
                    e.CancelEdit = true;  // 取消编辑，恢复原文本
                }
            }
            //Console.WriteLine($"Class名：{string.Join(", ", AppConf.AppClass)}");

        }
        
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = treeView.PointToClient(new Point(e.X, e.Y));
            TreeNode dNode = treeView.GetNodeAt(pt);
            if (AppConf.AdminMode)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files.Length > 1) { return; }
                    if (dNode != null)
                    {
                        string appClass;
                        if (dNode.Parent == null)
                        {
                            appClass = dNode.Text;
                        }
                        else
                        {
                            while (dNode.Parent != null)
                            {
                                dNode = dNode.Parent;
                            }
                            appClass = dNode.Text;
                        }
                        foreach (string file in files)
                        {
                            if (!File.Exists(file)) continue;
                            AddProg ap = new AddProg("add", file, appClass);
                            ap.ShowDialog(this);
                            RefreshImageList();
                            RefreshTreeView();
                        }
                    }
                    else
                    {
                        foreach (string file in files)
                        {
                            if (!File.Exists(file)) continue;
                            AddProg ap = new AddProg("add", file);
                            ap.ShowDialog(this);
                            RefreshImageList();
                            RefreshTreeView();
                        }
                    }
                    return;
                }
            }
            else
            {
                LabelStripStatus.Text = "请开启管理员模式！";
            }
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                TreeNode draggedNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
                if (dNode != null && draggedNode != dNode && !IsParentNode(draggedNode, dNode) && dNode != GetRootNode(draggedNode))
                {
                    string strID = draggedNode.Tag.ToString().Trim();
                    var result = AppArray.FirstOrDefault(item => (string)item["id"] == strID);
                    if (result == null)
                    {
                        LabelStripStatus.ToolTipText = "操作异常！";
                        LabelStripStatus.Text = SetStatusText("操作异常！");
                    }
                    else
                    {
                        string strClass = dNode.Text;
                        int intID = int.Parse(result["id"].ToString());
                        result["class"] = strClass;
                        var sql = new SQLite(AppConf.DbPath);
                        string strsql = "UPDATE lnk SET class=@strClass WHERE id='" + intID + "'";
                        var parameters = new SQLiteParameter[] { new SQLiteParameter("@strClass", strClass) };
                        sql.ExecuteNonQuery(strsql, parameters);
                        draggedNode.Remove();
                        dNode.Nodes.Add(draggedNode);
                        dNode.Expand();
                    }
                }
                return;
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                e.Effect = DragDropEffects.Move;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                Point pt = treeView.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = treeView.GetNodeAt(pt);
                if (targetNode != null && IsRootNode(targetNode))
                {
                    e.Effect = DragDropEffects.Move;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void ExpandRootMenu_Click(object sender, EventArgs e)
        {
            treeView.BeginUpdate();
            try
            {
                treeView.ExpandAll();
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        private void CollapseRootMenu_Click(object sender, EventArgs e)
        {
            treeView.BeginUpdate();
            try
            {
                treeView.CollapseAll();
            }
            finally
            {
                treeView.EndUpdate();
            }
        }

        private void treeView_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                TimerKeyPress.Stop();
                TimerKeyPress.Start();
                TreeViewKeyPress += e.KeyChar.ToString();
                QueryBox.Text = TreeViewKeyPress;
            }
            if (e.KeyChar == '\b')
            {
                if (TreeViewKeyPress.Length > 0)
                {
                    TreeViewKeyPress = TreeViewKeyPress.Substring(0, TreeViewKeyPress.Length - 1);
                    QueryBox.Text = TreeViewKeyPress;
                }
                else
                {
                    TreeViewKeyPress = string.Empty;
                    QueryBox.Text = TreeViewKeyPress;
                }
            }
            if (e.KeyChar == (char)Keys.Enter)
            {
                TreeNode selectedNode = treeView.SelectedNode;
                if (selectedNode != null)
                {
                    string str = selectedNode.Tag.ToString().Trim();
                    var result = AppArray.FirstOrDefault(item => (string)item["id"] == str);
                    Console.WriteLine(result);
                    if (result == null)
                    {
                        LabelStripStatus.Text = "";
                        LabelStripStatus.ToolTipText = "";
                    }
                    else
                    {
                        LabelStripStatus.ToolTipText = result["description"].ToString();
                        LabelStripStatus.Text = SetStatusText(result["description"].ToString());
                    }
                    if (AppConf.OpenMin)
                    {
                        PM_Show(false);
                    }
                    RunApp(result["path"].ToString(), int.Parse(result["auth"].ToString()), result["workdir"].ToString(), result["arguments"].ToString());
                    if (AppConf.OpenClose)
                    {
                        ExitMe();
                    }                    
                }
            }
        }

        private void TimerKeyPress_Tick(object sender, EventArgs e)
        {
            TreeViewKeyPress = string.Empty;
            TimerKeyPress.Stop();
        }

        private void QueryBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (treeView.Nodes.Count > 0)
                {
                    treeView.SelectedNode = treeView.Nodes[0];
                    treeView.Focus();
                }
            }
        }

        private void StartIpcListener()
        {
            _cts = new CancellationTokenSource();
            _ipcListenerThread = new Thread(() =>
            {
                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        using (var pipeServer = new NamedPipeServerStream(
                            AppConf.PipeName,
                            PipeDirection.In,
                            NamedPipeServerStream.MaxAllowedServerInstances,
                            PipeTransmissionMode.Message,
                            PipeOptions.Asynchronous))
                        {
                            // 异步等待连接（支持取消）
                            var connectTask = pipeServer.WaitForConnectionAsync(_cts.Token);
                            connectTask.Wait(_cts.Token); // 等待连接或取消

                            if (pipeServer.IsConnected)
                            {
                                using (var reader = new StreamReader(pipeServer))
                                {
                                    string command = reader.ReadLine();
                                    if (command == "RESTORE")
                                    {
                                        this.Invoke((MethodInvoker)(() => RestoreWindow()));
                                    }
                                }
                            }
                        }
                    }
                    catch (OperationCanceledException)
                    {
                        break; // 收到取消信号，退出循环
                    }
                    catch (IOException ex) when (IsPipeClosed(ex))
                    {
                        Thread.Sleep(1000); // 管道关闭，等待后重试
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"IPC监听错误: {ex.Message}");
                        Thread.Sleep(5000); // 其他错误，等待后重试
                    }
                }
            });

            _ipcListenerThread.IsBackground = true;
            _ipcListenerThread.Start();
        }

        private bool IsPipeClosed(Exception ex)
        {
            return ex is IOException ioEx &&
                   (ioEx.HResult == unchecked((int)0x800700E8) ||
                    ioEx.HResult == unchecked((int)0x80070040));
        }

        public static void RestoreWindow()
        {
            if (_instance != null && !_instance.IsDisposed)
            {                
                _instance.Visible = true;
                if (_instance.WindowState == FormWindowState.Minimized) { _instance.WindowState = FormWindowState.Normal; }
                _instance.ShowInTaskbar = true;
                _instance.Show();
                _instance.Activate();
                _instance.BringToFront();
            }
        }

        
    }
}
