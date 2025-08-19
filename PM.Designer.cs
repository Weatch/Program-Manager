namespace PM
{
    partial class PM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PM));
            this.NotifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.LabelStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TreeViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenTreeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTreeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditTreeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DelTreeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel = new System.Windows.Forms.Panel();
            this.treeView = new NewTreeView();
            this.LImageList = new System.Windows.Forms.ImageList(this.components);
            this.QueryBox = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TreeViewRootMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RefreshRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpandRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DelRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionRootMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerKeyPress = new System.Windows.Forms.Timer(this.components);
            this.SImageList = new System.Windows.Forms.ImageList(this.components);
            this.NotifyMenu.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            this.TreeViewMenu.SuspendLayout();
            this.Panel.SuspendLayout();
            this.TreeViewRootMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // NotifyMenu
            // 
            this.NotifyMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NotifyMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.NotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowMenu,
            this.AboutMenu,
            this.OptionMenu,
            this.ExitMenu});
            this.NotifyMenu.Name = "NotifyMenu";
            this.NotifyMenu.Size = new System.Drawing.Size(123, 124);
            // 
            // ShowMenu
            // 
            this.ShowMenu.Name = "ShowMenu";
            this.ShowMenu.Size = new System.Drawing.Size(122, 30);
            this.ShowMenu.Text = "显示";
            this.ShowMenu.Click += new System.EventHandler(this.ShowMenu_Click);
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.Size = new System.Drawing.Size(122, 30);
            this.AboutMenu.Text = "关于";
            this.AboutMenu.Click += new System.EventHandler(this.AboutMenu_Click);
            // 
            // OptionMenu
            // 
            this.OptionMenu.Name = "OptionMenu";
            this.OptionMenu.Size = new System.Drawing.Size(122, 30);
            this.OptionMenu.Text = "选项";
            this.OptionMenu.Click += new System.EventHandler(this.OptionMenu_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.Size = new System.Drawing.Size(122, 30);
            this.ExitMenu.Text = "退出";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelStripStatus});
            this.StatusStrip.Location = new System.Drawing.Point(0, 339);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(284, 22);
            this.StatusStrip.TabIndex = 2;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // LabelStripStatus
            // 
            this.LabelStripStatus.AutoToolTip = true;
            this.LabelStripStatus.Name = "LabelStripStatus";
            this.LabelStripStatus.Size = new System.Drawing.Size(32, 17);
            this.LabelStripStatus.Text = "描述";
            this.LabelStripStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TreeViewMenu
            // 
            this.TreeViewMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TreeViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenTreeMenu,
            this.AddTreeMenu,
            this.EditTreeMenu,
            this.DelTreeMenu});
            this.TreeViewMenu.Name = "TreeViewMenu";
            this.TreeViewMenu.Size = new System.Drawing.Size(123, 124);
            // 
            // OpenTreeMenu
            // 
            this.OpenTreeMenu.Name = "OpenTreeMenu";
            this.OpenTreeMenu.Size = new System.Drawing.Size(122, 30);
            this.OpenTreeMenu.Text = "打开";
            this.OpenTreeMenu.Click += new System.EventHandler(this.OpenTreeMenu_Click);
            // 
            // AddTreeMenu
            // 
            this.AddTreeMenu.Name = "AddTreeMenu";
            this.AddTreeMenu.Size = new System.Drawing.Size(122, 30);
            this.AddTreeMenu.Text = "添加";
            this.AddTreeMenu.Visible = false;
            this.AddTreeMenu.Click += new System.EventHandler(this.AddTreeMenu_Click);
            // 
            // EditTreeMenu
            // 
            this.EditTreeMenu.Name = "EditTreeMenu";
            this.EditTreeMenu.Size = new System.Drawing.Size(122, 30);
            this.EditTreeMenu.Text = "编辑";
            this.EditTreeMenu.Visible = false;
            this.EditTreeMenu.Click += new System.EventHandler(this.EditTreeMenu_Click);
            // 
            // DelTreeMenu
            // 
            this.DelTreeMenu.Name = "DelTreeMenu";
            this.DelTreeMenu.Size = new System.Drawing.Size(122, 30);
            this.DelTreeMenu.Text = "删除";
            this.DelTreeMenu.Visible = false;
            this.DelTreeMenu.Click += new System.EventHandler(this.DelTreeMenu_Click);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.treeView);
            this.Panel.Controls.Add(this.QueryBox);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(0, 0);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(284, 339);
            this.Panel.TabIndex = 3;
            // 
            // treeView
            // 
            this.treeView.AllowDrop = true;
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.LImageList;
            this.treeView.Indent = 35;
            this.treeView.ItemHeight = 36;
            this.treeView.Location = new System.Drawing.Point(0, 29);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowLines = false;
            this.treeView.ShowPlusMinus = false;
            this.treeView.ShowRootLines = false;
            this.treeView.Size = new System.Drawing.Size(284, 310);
            this.treeView.TabIndex = 1;
            this.treeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            this.treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseClick);
            this.treeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_NodeMouseDoubleClick);
            this.treeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_DragDrop);
            this.treeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_DragEnter);
            this.treeView.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_DragOver);
            this.treeView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeView_KeyPress);
            this.treeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseUp);
            // 
            // LImageList
            // 
            this.LImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.LImageList.ImageSize = new System.Drawing.Size(32, 32);
            this.LImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // QueryBox
            // 
            this.QueryBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.QueryBox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.QueryBox.Location = new System.Drawing.Point(0, 0);
            this.QueryBox.Name = "QueryBox";
            this.QueryBox.ShortcutsEnabled = false;
            this.QueryBox.Size = new System.Drawing.Size(284, 29);
            this.QueryBox.TabIndex = 0;
            this.QueryBox.Text = "搜索栏";
            this.QueryBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.QueryBox.Click += new System.EventHandler(this.QueryBox_Click);
            this.QueryBox.TextChanged += new System.EventHandler(this.QueryBox_TextChanged);
            this.QueryBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QueryBox_KeyPress);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.NotifyMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "PM";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // TreeViewRootMenu
            // 
            this.TreeViewRootMenu.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TreeViewRootMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshRootMenu,
            this.ExpandRootMenu,
            this.CollapseRootMenu,
            this.AddRootMenu,
            this.EditRootMenu,
            this.DelRootMenu,
            this.OptionRootMenu});
            this.TreeViewRootMenu.Name = "TreeViewMenu";
            this.TreeViewRootMenu.Size = new System.Drawing.Size(123, 214);
            // 
            // RefreshRootMenu
            // 
            this.RefreshRootMenu.Name = "RefreshRootMenu";
            this.RefreshRootMenu.Size = new System.Drawing.Size(122, 30);
            this.RefreshRootMenu.Text = "刷新";
            this.RefreshRootMenu.Click += new System.EventHandler(this.RefreshRootMenu_Click);
            // 
            // ExpandRootMenu
            // 
            this.ExpandRootMenu.Name = "ExpandRootMenu";
            this.ExpandRootMenu.Size = new System.Drawing.Size(122, 30);
            this.ExpandRootMenu.Text = "展开";
            this.ExpandRootMenu.Click += new System.EventHandler(this.ExpandRootMenu_Click);
            // 
            // CollapseRootMenu
            // 
            this.CollapseRootMenu.Name = "CollapseRootMenu";
            this.CollapseRootMenu.Size = new System.Drawing.Size(122, 30);
            this.CollapseRootMenu.Text = "折叠";
            this.CollapseRootMenu.Click += new System.EventHandler(this.CollapseRootMenu_Click);
            // 
            // AddRootMenu
            // 
            this.AddRootMenu.Name = "AddRootMenu";
            this.AddRootMenu.Size = new System.Drawing.Size(122, 30);
            this.AddRootMenu.Text = "添加";
            this.AddRootMenu.Visible = false;
            this.AddRootMenu.Click += new System.EventHandler(this.AddRootMenu_Click);
            // 
            // EditRootMenu
            // 
            this.EditRootMenu.Name = "EditRootMenu";
            this.EditRootMenu.Size = new System.Drawing.Size(122, 30);
            this.EditRootMenu.Text = "编辑";
            this.EditRootMenu.Visible = false;
            this.EditRootMenu.Click += new System.EventHandler(this.EditRootMenu_Click);
            // 
            // DelRootMenu
            // 
            this.DelRootMenu.Name = "DelRootMenu";
            this.DelRootMenu.Size = new System.Drawing.Size(122, 30);
            this.DelRootMenu.Text = "删除";
            this.DelRootMenu.Visible = false;
            this.DelRootMenu.Click += new System.EventHandler(this.DelRootMenu_Click);
            // 
            // OptionRootMenu
            // 
            this.OptionRootMenu.Name = "OptionRootMenu";
            this.OptionRootMenu.Size = new System.Drawing.Size(122, 30);
            this.OptionRootMenu.Text = "选项";
            this.OptionRootMenu.Click += new System.EventHandler(this.OptionRootMenu_Click);
            // 
            // TimerKeyPress
            // 
            this.TimerKeyPress.Interval = 1500;
            this.TimerKeyPress.Tick += new System.EventHandler(this.TimerKeyPress_Tick);
            // 
            // SImageList
            // 
            this.SImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.SImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.SImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // PM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.StatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 400);
            this.Name = "PM";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PM_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PM_FormClosed);
            this.Load += new System.EventHandler(this.PM_Load);
            this.NotifyMenu.ResumeLayout(false);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.TreeViewMenu.ResumeLayout(false);
            this.Panel.ResumeLayout(false);
            this.Panel.PerformLayout();
            this.TreeViewRootMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip NotifyMenu;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ContextMenuStrip TreeViewMenu;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.TextBox QueryBox;
        private System.Windows.Forms.ImageList LImageList;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem ShowMenu;
        private System.Windows.Forms.ToolStripMenuItem AboutMenu;
        private System.Windows.Forms.ToolStripMenuItem OptionMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenu;
        private System.Windows.Forms.ToolStripMenuItem AddTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem EditTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem DelTreeMenu;
        private System.Windows.Forms.ContextMenuStrip TreeViewRootMenu;
        private System.Windows.Forms.ToolStripMenuItem RefreshRootMenu;
        private System.Windows.Forms.ToolStripMenuItem AddRootMenu;
        private System.Windows.Forms.ToolStripMenuItem EditRootMenu;
        private System.Windows.Forms.ToolStripMenuItem DelRootMenu;
        private System.Windows.Forms.ToolStripMenuItem OptionRootMenu;
        private System.Windows.Forms.ToolStripMenuItem OpenTreeMenu;
        private NewTreeView treeView;
        private System.Windows.Forms.ToolStripStatusLabel LabelStripStatus;
        private System.Windows.Forms.ToolStripMenuItem ExpandRootMenu;
        private System.Windows.Forms.ToolStripMenuItem CollapseRootMenu;
        private System.Windows.Forms.Timer TimerKeyPress;
        private System.Windows.Forms.ImageList SImageList;
    }
}