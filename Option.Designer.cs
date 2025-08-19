namespace PM
{
    partial class Option
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
            this.xExit = new System.Windows.Forms.CheckBox();
            this.HotKey = new System.Windows.Forms.CheckBox();
            this.LineSpace = new System.Windows.Forms.CheckBox();
            this.DClick = new System.Windows.Forms.CheckBox();
            this.OpenMin = new System.Windows.Forms.CheckBox();
            this.OpenClose = new System.Windows.Forms.CheckBox();
            this.TopMostCheck = new System.Windows.Forms.CheckBox();
            this.HotKeyText = new System.Windows.Forms.TextBox();
            this.StartUP = new System.Windows.Forms.CheckBox();
            this.IconSize = new System.Windows.Forms.CheckBox();
            this.FontSize = new System.Windows.Forms.CheckBox();
            this.IconSizeValue = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.AdminMode = new System.Windows.Forms.CheckBox();
            this.StartUpMin = new System.Windows.Forms.CheckBox();
            this.LineSpaceValue = new System.Windows.Forms.ComboBox();
            this.FontSizeValue = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // xExit
            // 
            this.xExit.AutoSize = true;
            this.xExit.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.xExit.Location = new System.Drawing.Point(12, 12);
            this.xExit.Name = "xExit";
            this.xExit.Size = new System.Drawing.Size(114, 29);
            this.xExit.TabIndex = 0;
            this.xExit.Text = "\"x\"最小化";
            this.xExit.UseVisualStyleBackColor = true;
            this.xExit.CheckedChanged += new System.EventHandler(this.xExit_CheckedChanged);
            // 
            // HotKey
            // 
            this.HotKey.AutoSize = true;
            this.HotKey.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HotKey.Location = new System.Drawing.Point(12, 375);
            this.HotKey.Name = "HotKey";
            this.HotKey.Size = new System.Drawing.Size(69, 29);
            this.HotKey.TabIndex = 14;
            this.HotKey.Text = "热键";
            this.HotKey.UseVisualStyleBackColor = true;
            this.HotKey.CheckedChanged += new System.EventHandler(this.HotKey_CheckedChanged);
            // 
            // LineSpace
            // 
            this.LineSpace.AutoSize = true;
            this.LineSpace.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineSpace.Location = new System.Drawing.Point(12, 276);
            this.LineSpace.Name = "LineSpace";
            this.LineSpace.Size = new System.Drawing.Size(88, 29);
            this.LineSpace.TabIndex = 8;
            this.LineSpace.Text = "行间距";
            this.LineSpace.UseVisualStyleBackColor = true;
            this.LineSpace.CheckedChanged += new System.EventHandler(this.LineSpace_CheckedChanged);
            // 
            // DClick
            // 
            this.DClick.AutoSize = true;
            this.DClick.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.DClick.Location = new System.Drawing.Point(12, 144);
            this.DClick.Name = "DClick";
            this.DClick.Size = new System.Drawing.Size(107, 29);
            this.DClick.TabIndex = 4;
            this.DClick.Text = "双击打开";
            this.DClick.UseVisualStyleBackColor = true;
            this.DClick.CheckedChanged += new System.EventHandler(this.DClick_CheckedChanged);
            // 
            // OpenMin
            // 
            this.OpenMin.AutoSize = true;
            this.OpenMin.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OpenMin.Location = new System.Drawing.Point(12, 177);
            this.OpenMin.Name = "OpenMin";
            this.OpenMin.Size = new System.Drawing.Size(145, 29);
            this.OpenMin.TabIndex = 5;
            this.OpenMin.Text = "打开后最小化";
            this.OpenMin.UseVisualStyleBackColor = true;
            this.OpenMin.CheckedChanged += new System.EventHandler(this.OpenMin_CheckedChanged);
            // 
            // OpenClose
            // 
            this.OpenClose.AutoSize = true;
            this.OpenClose.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.OpenClose.Location = new System.Drawing.Point(12, 210);
            this.OpenClose.Name = "OpenClose";
            this.OpenClose.Size = new System.Drawing.Size(126, 29);
            this.OpenClose.TabIndex = 6;
            this.OpenClose.Text = "打开后关闭";
            this.OpenClose.UseVisualStyleBackColor = true;
            this.OpenClose.CheckedChanged += new System.EventHandler(this.OpenClose_CheckedChanged);
            // 
            // TopMostCheck
            // 
            this.TopMostCheck.AutoSize = true;
            this.TopMostCheck.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TopMostCheck.Location = new System.Drawing.Point(12, 111);
            this.TopMostCheck.Name = "TopMostCheck";
            this.TopMostCheck.Size = new System.Drawing.Size(126, 29);
            this.TopMostCheck.TabIndex = 3;
            this.TopMostCheck.Text = "最上层显示";
            this.TopMostCheck.UseVisualStyleBackColor = true;
            this.TopMostCheck.CheckedChanged += new System.EventHandler(this.TopMost_CheckedChanged);
            // 
            // HotKeyText
            // 
            this.HotKeyText.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HotKeyText.Location = new System.Drawing.Point(87, 376);
            this.HotKeyText.Name = "HotKeyText";
            this.HotKeyText.Size = new System.Drawing.Size(170, 29);
            this.HotKeyText.TabIndex = 15;
            this.HotKeyText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.HotKeyText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HotKeyText_KeyDown);
            // 
            // StartUP
            // 
            this.StartUP.AutoSize = true;
            this.StartUP.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartUP.Location = new System.Drawing.Point(12, 45);
            this.StartUP.Name = "StartUP";
            this.StartUP.Size = new System.Drawing.Size(172, 29);
            this.StartUP.TabIndex = 1;
            this.StartUP.Text = "随Windows启动";
            this.StartUP.UseVisualStyleBackColor = true;
            this.StartUP.CheckedChanged += new System.EventHandler(this.StartUP_CheckedChanged);
            // 
            // IconSize
            // 
            this.IconSize.AutoSize = true;
            this.IconSize.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IconSize.Location = new System.Drawing.Point(12, 309);
            this.IconSize.Name = "IconSize";
            this.IconSize.Size = new System.Drawing.Size(107, 29);
            this.IconSize.TabIndex = 10;
            this.IconSize.Text = "图标尺寸";
            this.IconSize.UseVisualStyleBackColor = true;
            this.IconSize.CheckedChanged += new System.EventHandler(this.IconSize_CheckedChanged);
            // 
            // FontSize
            // 
            this.FontSize.AutoSize = true;
            this.FontSize.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FontSize.Location = new System.Drawing.Point(12, 342);
            this.FontSize.Name = "FontSize";
            this.FontSize.Size = new System.Drawing.Size(107, 29);
            this.FontSize.TabIndex = 12;
            this.FontSize.Text = "字体大小";
            this.FontSize.UseVisualStyleBackColor = true;
            this.FontSize.CheckedChanged += new System.EventHandler(this.FontSize_CheckedChanged);
            // 
            // IconSizeValue
            // 
            this.IconSizeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IconSizeValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IconSizeValue.FormattingEnabled = true;
            this.IconSizeValue.ItemHeight = 16;
            this.IconSizeValue.Items.AddRange(new object[] {
            "16",
            "32"});
            this.IconSizeValue.Location = new System.Drawing.Point(211, 311);
            this.IconSizeValue.Name = "IconSizeValue";
            this.IconSizeValue.Size = new System.Drawing.Size(46, 24);
            this.IconSizeValue.TabIndex = 11;
            this.IconSizeValue.SelectedIndexChanged += new System.EventHandler(this.IconSizeValue_SelectedIndexChanged);
            this.IconSizeValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.IconSizeValue_KeyPress);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(202, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(12, 411);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 30);
            this.button2.TabIndex = 16;
            this.button2.Text = "默认";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // AdminMode
            // 
            this.AdminMode.AutoSize = true;
            this.AdminMode.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AdminMode.Location = new System.Drawing.Point(12, 243);
            this.AdminMode.Name = "AdminMode";
            this.AdminMode.Size = new System.Drawing.Size(126, 29);
            this.AdminMode.TabIndex = 7;
            this.AdminMode.Text = "管理员模式";
            this.AdminMode.UseVisualStyleBackColor = true;
            this.AdminMode.CheckedChanged += new System.EventHandler(this.AdminMode_CheckedChanged);
            // 
            // StartUpMin
            // 
            this.StartUpMin.AutoSize = true;
            this.StartUpMin.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartUpMin.Location = new System.Drawing.Point(12, 78);
            this.StartUpMin.Name = "StartUpMin";
            this.StartUpMin.Size = new System.Drawing.Size(145, 29);
            this.StartUpMin.TabIndex = 2;
            this.StartUpMin.Text = "启动后最小化";
            this.StartUpMin.UseVisualStyleBackColor = true;
            this.StartUpMin.CheckedChanged += new System.EventHandler(this.StartUpMin_CheckedChanged);
            // 
            // LineSpaceValue
            // 
            this.LineSpaceValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LineSpaceValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LineSpaceValue.FormattingEnabled = true;
            this.LineSpaceValue.ItemHeight = 16;
            this.LineSpaceValue.Items.AddRange(new object[] {
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30",
            "32",
            "34",
            "36",
            "38",
            "40",
            "42",
            "44",
            "46",
            "48",
            "50",
            "52",
            "54",
            "56",
            "58",
            "60"});
            this.LineSpaceValue.Location = new System.Drawing.Point(211, 278);
            this.LineSpaceValue.Name = "LineSpaceValue";
            this.LineSpaceValue.Size = new System.Drawing.Size(46, 24);
            this.LineSpaceValue.TabIndex = 9;
            this.LineSpaceValue.SelectedIndexChanged += new System.EventHandler(this.LineSpaceValue_SelectedIndexChanged);
            // 
            // FontSizeValue
            // 
            this.FontSizeValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FontSizeValue.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FontSizeValue.FormattingEnabled = true;
            this.FontSizeValue.ItemHeight = 16;
            this.FontSizeValue.Items.AddRange(new object[] {
            "8",
            "10",
            "12",
            "14",
            "16",
            "18",
            "20",
            "22",
            "24",
            "26",
            "28",
            "30",
            "32",
            "34",
            "36",
            "38",
            "40"});
            this.FontSizeValue.Location = new System.Drawing.Point(211, 344);
            this.FontSizeValue.Name = "FontSizeValue";
            this.FontSizeValue.Size = new System.Drawing.Size(46, 24);
            this.FontSizeValue.TabIndex = 13;
            this.FontSizeValue.SelectedIndexChanged += new System.EventHandler(this.FontSizeValue_SelectedIndexChanged);
            // 
            // Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 448);
            this.Controls.Add(this.FontSizeValue);
            this.Controls.Add(this.LineSpaceValue);
            this.Controls.Add(this.StartUpMin);
            this.Controls.Add(this.AdminMode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.IconSizeValue);
            this.Controls.Add(this.FontSize);
            this.Controls.Add(this.IconSize);
            this.Controls.Add(this.StartUP);
            this.Controls.Add(this.HotKeyText);
            this.Controls.Add(this.TopMostCheck);
            this.Controls.Add(this.OpenClose);
            this.Controls.Add(this.OpenMin);
            this.Controls.Add(this.DClick);
            this.Controls.Add(this.LineSpace);
            this.Controls.Add(this.HotKey);
            this.Controls.Add(this.xExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Option";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选项";
            this.Load += new System.EventHandler(this.Option_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox xExit;
        private System.Windows.Forms.CheckBox HotKey;
        private System.Windows.Forms.CheckBox LineSpace;
        private System.Windows.Forms.CheckBox DClick;
        private System.Windows.Forms.CheckBox OpenMin;
        private System.Windows.Forms.CheckBox OpenClose;
        private System.Windows.Forms.CheckBox TopMostCheck;
        private System.Windows.Forms.TextBox HotKeyText;
        private System.Windows.Forms.CheckBox StartUP;
        private System.Windows.Forms.CheckBox IconSize;
        private System.Windows.Forms.CheckBox FontSize;
        private System.Windows.Forms.ComboBox IconSizeValue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox AdminMode;
        private System.Windows.Forms.CheckBox StartUpMin;
        private System.Windows.Forms.ComboBox LineSpaceValue;
        private System.Windows.Forms.ComboBox FontSizeValue;
    }
}