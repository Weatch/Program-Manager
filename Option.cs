using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PM
{
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
        }
        private bool IsNumeric(string input)
        {
            // 匹配整数或浮点数
            Regex regex = new Regex(@"^-?\d+(\.\d+)?$");
            return regex.IsMatch(input);
        }
        private void Option_Load(object sender, EventArgs e)
        {
            if (AppConf.xExit) { xExit.Checked = true; }
            if (AppConf.OpenMin) { OpenMin.Checked = true; }
            if (AppConf.OpenClose) { OpenClose.Checked = true; }
            if (AppConf.DoubleClick) { DClick.Checked = true; }
            if (AppConf.HotKey) { HotKey.Checked = true; }
            if (AppConf.TopMost) { TopMostCheck.Checked = true; }
            if (AppConf.LineSpace) { LineSpace.Checked = true; }
            if (AppConf.AdminMode) { AdminMode.Checked = true; }
            if (AppConf.StartUpMin) { StartUpMin.Checked = true; }
            LineSpaceValue.Text= AppConf.LineSpaceValue.ToString();
            HotKeyText.Text = AppConf.HotKeyCode;
            if (AppConf.IconSize) { IconSize.Checked = true; }
            IconSizeValue.Text = AppConf.IconSizeValue.ToString();
            if (AppConf.FontSize) { FontSize.Checked = true; }
            FontSizeValue.Text= AppConf.FontSizeValue.ToString();
            WinStartUp wsu = new WinStartUp();
            StartUP.Checked = wsu.RegIsStartupEnabled();
            //StartUP.Checked = wsu.TaskExists(AppConf.AppName);
        }
        private static void UpSql(string key,string value)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = "UPDATE sys SET value=@value WHERE key='"+key+"'";
            var parameters = new SQLiteParameter[] { new SQLiteParameter("@value", value) };
            int i =sql.ExecuteNonQuery(strsql, parameters);
            if (i < 1)
            {
                strsql = "INSERT INTO sys (key,value) VALUES (@key,@value)";
                parameters = new SQLiteParameter[] {
                        new SQLiteParameter("@key", key),
                        new SQLiteParameter("@value", value)
                       };
                sql.ExecuteNonQuery(strsql, parameters);
            }
        }
        private void xExit_CheckedChanged(object sender, EventArgs e)
        {
            UpSql("xExit", xExit.Checked ? "1" : "0");
            AppConf.xExit = xExit.Checked;
        }

        private void TopMost_CheckedChanged(object sender, EventArgs e)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = "UPDATE sys SET value=@value WHERE key='TopMost'";
            var parameters = new SQLiteParameter[] { new SQLiteParameter("@value", TopMostCheck.Checked) };
            sql.ExecuteNonQuery(strsql, parameters);
            AppConf.TopMost = TopMostCheck.Checked;
        }

        private void OpenClose_CheckedChanged(object sender, EventArgs e)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = "UPDATE sys SET value=@value WHERE key='OpenClose'";
            var parameters = new SQLiteParameter[] { new SQLiteParameter("@value", OpenClose.Checked) };
            sql.ExecuteNonQuery(strsql, parameters);
            AppConf.OpenClose = OpenClose.Checked;
        }

        private void OpenMin_CheckedChanged(object sender, EventArgs e)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = "UPDATE sys SET value=@value WHERE key='OpenMin'";
            var parameters = new SQLiteParameter[] { new SQLiteParameter("@value", OpenMin.Checked) };
            sql.ExecuteNonQuery(strsql, parameters);
            AppConf.OpenMin = OpenMin.Checked;
        }

        private void DClick_CheckedChanged(object sender, EventArgs e)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = "UPDATE sys SET value=@value WHERE key='DoubleClick'";
            var parameters = new SQLiteParameter[] { new SQLiteParameter("@value", DClick.Checked) };
            sql.ExecuteNonQuery(strsql, parameters);
            AppConf.DoubleClick = DClick.Checked;
        }

        private void LineSpace_CheckedChanged(object sender, EventArgs e)
        {
            UpSql("LineSpace", LineSpace.Checked ? "1" : "0");
            AppConf.LineSpace = LineSpace.Checked;
        }

        private void HotKey_CheckedChanged(object sender, EventArgs e)
        {
            UpSql("HotKey", HotKey.Checked ? "1" : "0");
            AppConf.HotKey = HotKey.Checked;
        }

        private void HotKeyText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control || e.Alt || e.Shift)
            {
                string combination = "";
                if (e.Control) combination += "Ctrl+";
                if (e.Alt) combination += "Alt+";
                if (e.Shift) combination += "Shift+";

                if (e.KeyCode != Keys.ControlKey &&
                    e.KeyCode != Keys.Menu &&
                    e.KeyCode != Keys.ShiftKey)
                {
                    combination += e.KeyCode.ToString();
                    HotKeyText.Text = combination;
                    e.SuppressKeyPress = true;

                    HotKey.Checked = true;
                    UpSql("HotKey", HotKey.Checked ? "1" : "0");
                    AppConf.HotKey = HotKey.Checked;
                    UpSql("HotKeyCode", HotKeyText.Text);
                    AppConf.HotKeyCode = HotKeyText.Text;
                }
            }
        }
        
        private void FontSize_CheckedChanged(object sender, EventArgs e)
        {
            UpSql("FontSize", FontSize.Checked ? "1" : "0");
            AppConf.FontSize = FontSize.Checked;
        }

        private void FontSizeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true; // 阻止输入非数字字符
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IconSize_CheckedChanged(object sender, EventArgs e)
        {
            UpSql("IconSize", IconSize.Checked ? "1" : "0");
            AppConf.IconSize = IconSize.Checked;
        }

        private void IconSizeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpSql("IconSizeValue", IconSizeValue.Text);
            AppConf.IconSizeValue = int.Parse(IconSizeValue.Text);
        }

        private void IconSizeValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void AdminMode_CheckedChanged(object sender, EventArgs e)
        {
            AppConf.AdminMode = AdminMode.Checked;
        }

        private void StartUP_CheckedChanged(object sender, EventArgs e)
        {            
            WinStartUp wsu = new WinStartUp();            
            if (StartUP.Checked)
            {
                wsu.RegEnableStartup();
                //su.TaskCreateStartup();
            }
            else
            {
                wsu.RegDisableStartup();
                //su.TaskDelete(AppConf.AppName);
            }

        }

        private void StartUpMin_CheckedChanged(object sender, EventArgs e)
        {
            UpSql("StartUpMin",StartUpMin.Checked ? "1" : "0");
            AppConf.StartUpMin = StartUpMin.Checked;
        }

        private void FontSizeValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpSql("FontSizeValue", FontSizeValue.Text);
            AppConf.FontSizeValue = int.Parse(FontSizeValue.Text);
        }

        private void LineSpaceValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpSql("LineSpaceValue", LineSpaceValue.Text);
            AppConf.LineSpaceValue = int.Parse(LineSpaceValue.Text);
        }

    }
}
