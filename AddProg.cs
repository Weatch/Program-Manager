using IWshRuntimeLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace PM
{
    public partial class AddProg : Form
    {
        private static string _key;

        public AddProg()
        {
            InitializeComponent();
            InitClassComboBox();
        }
        public AddProg(string operate, string key,string appClass=null)
        {
            InitializeComponent();
            InitClassComboBox();
            _key = key;
            if (appClass == null) 
            {
                ClassComboBox.SelectedIndex = 0;
            } else 
            { 
                ClassComboBox.SelectedItem = appClass; 
            }

            if (operate == "add")
            {
                ConfirmBTN.Text = "添加";
                string ext= Path.GetExtension(key).TrimStart('.').ToLower();
                if (ext == "lnk")
                {
                    WshShell shell = new WshShell();
                    IWshShortcut sc = (IWshShortcut)shell.CreateShortcut(key);
                    PathTextBox.Text = sc.TargetPath;
                    NameTextBox.Text = Path.GetFileNameWithoutExtension(sc.FullName);
                    DescriptionTextBox.Text = sc.Description;
                    RemTextBox.Text = NameTextBox.Text;
                    WorkDirTextBox.Text = sc.WorkingDirectory;
                    IconTextBox.Text = NameTextBox.Text + ".ico";
                }
                else
                {
                    
                    PathTextBox.Text = key;
                    NameTextBox.Text = Path.GetFileNameWithoutExtension(key);
                    DescriptionTextBox.Text = Path.GetFileNameWithoutExtension(key);
                    RemTextBox.Text = Path.GetFileNameWithoutExtension(key);
                    WorkDirTextBox.Text = Path.GetDirectoryName(key);
                    IconTextBox.Text = NameTextBox.Text + ".ico";
                }
                GetIcon gi = new GetIcon();
                gi.FileToIcon(key, AppConf.AppPath + AppConf.IconPath + @"\" + IconTextBox.Text);
            }
            if (operate == "edit")
            {
                ConfirmBTN.Text = "保存";
                var sql = new SQLite(AppConf.DbPath);
                string json = sql.ExecuteJson("SELECT * FROM lnk WHERE id='"+key+"'");
                JArray jn = JArray.Parse(json);
                PathTextBox.Text = jn[0]["path"].ToString();
                NameTextBox.Text = jn[0]["name"].ToString();
                DescriptionTextBox.Text = jn[0]["description"].ToString();
                RemTextBox.Text = jn[0]["rem"].ToString();
                ClassComboBox.SelectedItem= jn[0]["class"].ToString();
                AuthComboBox.SelectedIndex = int.Parse(jn[0]["auth"].ToString());
                WorkDirTextBox.Text = jn[0]["workdir"].ToString();
                ArgumentsTextBox.Text= jn[0]["arguments"].ToString();
                IconTextBox.Text = jn[0]["icon"].ToString();
            }
        }
        private void InitClassComboBox()
        {
            foreach (var v in AppConf.AppClass)
            {
                ClassComboBox.Items.Add(v);
            }
            ClassComboBox.SelectedIndex = 0;
            AuthComboBox.SelectedIndex = 0;
        }
        private void WorkDirBTN_Click(object sender, EventArgs e)
        {
            try
            {
                using (var folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "请选择文件夹";
                    folderDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                    folderDialog.ShowNewFolderButton = true;
                    DialogResult result = folderDialog.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                    {
                        WorkDirTextBox.Text= folderDialog.SelectedPath;
                    }
                }
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Security error.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
            }
        }

        private void PathBTN_Click(object sender, EventArgs e)
        {
            string strPath = @"d:\";
            if (!string.IsNullOrWhiteSpace(PathTextBox.Text))
            {
                strPath = Path.GetDirectoryName(PathTextBox.Text);
            }
            try
            {
                OpenFileDialog op = new OpenFileDialog()
                {
                    FileName = "请选择应用程序",
                    Filter = "(*.exe)|*.exe",
                    Title = "打开文件",
                    InitialDirectory = strPath
                };
                if (op.ShowDialog() == DialogResult.OK)
                {
                    var filePath = op.FileName;
                    var fn = Path.GetFileNameWithoutExtension(op.SafeFileName);
                    PathTextBox.Text = filePath;
                    NameTextBox.Text = fn;
                    DescriptionTextBox.Text = fn;
                    RemTextBox.Text = fn;
                    IconTextBox.Text = fn + ".ico";
                    WorkDirTextBox.Text = Path.GetDirectoryName(filePath);
                    GetIcon gi = new GetIcon();
                    gi.FileToIcon(filePath, AppConf.AppPath + AppConf.IconPath + @"\" + IconTextBox.Text);
                }
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Security error.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
            }
        }

        private void CancelBTN_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ConfirmBTN_Click(object sender, EventArgs e)
        {
            if (ConfirmBTN.Text == "保存")
            {
                try
                {
                    var sql = new SQLite(AppConf.DbPath);
                    string strsql = "UPDATE lnk SET icon=@icon,name=@name, class=@class,auth=@auth,description=@description, arguments=@arguments, path=@path, workdir=@workdir, rem=@rem WHERE id='" + _key + "'";
                    var parameters = new SQLiteParameter[] {
                        new SQLiteParameter("@name", NameTextBox.Text),
                        new SQLiteParameter("@class", ClassComboBox.Text),
                        new SQLiteParameter("@auth", AuthComboBox.Text=="用户"?0:1),
                        new SQLiteParameter("@description", DescriptionTextBox.Text),
                        new SQLiteParameter("@arguments", ArgumentsTextBox.Text),
                        new SQLiteParameter("@path", PathTextBox.Text),
                        new SQLiteParameter("@workdir", WorkDirTextBox.Text),
                        new SQLiteParameter("@icon", IconTextBox.Text),
                        new SQLiteParameter("@rem", RemTextBox.Text)
                       };
                    sql.ExecuteNonQuery(strsql, parameters);
                    sql = null;
                    this.Close();
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine($"Security error.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
                }
            }
            else
            {
                try
                {
                    var sql = new SQLite(AppConf.DbPath);
                    string strsql = "INSERT INTO lnk (icon,name, class, auth,description, arguments, path, workdir, rem) VALUES (@icon,@name, @class, @auth, @description, @arguments, @path, @workdir, @rem)";
                    var parameters = new SQLiteParameter[] {
                        new SQLiteParameter("@icon", IconTextBox.Text),
                        new SQLiteParameter("@name", NameTextBox.Text),
                        new SQLiteParameter("@class", ClassComboBox.Text),
                        new SQLiteParameter("@auth", AuthComboBox.Text=="用户"?0:1),
                        new SQLiteParameter("@description", DescriptionTextBox.Text),
                        new SQLiteParameter("@arguments", ArgumentsTextBox.Text),
                        new SQLiteParameter("@path", PathTextBox.Text),
                        new SQLiteParameter("@workdir", WorkDirTextBox.Text),
                        new SQLiteParameter("@rem", RemTextBox.Text)
                       };
                    sql.ExecuteNonQuery(strsql, parameters);
                    sql = null;
                    this.Close();
                }
                catch (SecurityException ex)
                {
                    Console.WriteLine($"Security error.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void IconBTN_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog()
                {
                    FileName = "请选择图标文件",
                    Filter = "(*.ico)|*.ico",
                    Title = "打开文件",
                    InitialDirectory = AppConf.AppPath + AppConf.IconPath
                };
                if (op.ShowDialog() == DialogResult.OK)
                {
                    IconTextBox.Text = Path.GetFileName(op.FileName);
                }
            }
            catch (SecurityException ex)
            {
                Console.WriteLine($"Security error.\n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
            }
        }
        public static void GetShortcutTarget(string shortcutPath)
        {
            if (!System.IO.File.Exists(shortcutPath)) return;

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

        }


    }
}
