using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PM
{
    internal class AppConf
    {
        public static string FormText { get; set; } = "PM";
        public static string Cid { get; } = "PC-" + Guid.NewGuid().ToString().Substring(0, 13);
        public static string PipeName { get; } = "PM_Restore_Pipe";
        public static string Ver { get; set; }
        public static string Copyright { get; set; }
        public static string Attr { get; set; }
        public static string AppPath { get; set; }
        public static string IniPath { get; set; }
        public static string IconPath { get; set; } = "icon";
        public static string DbPath { get; set; }
        public static string DbTable { get; set; } = "lnk";
        public static string AppName { get; set; } = "PM";
        public static string FolderIcon { get; set; } = "folder";
        public static int X { get; set; } = 0;
        public static int Y { get; set; } = 0;
        public static int Width { get; set; } = 300;
        public static int Height { get; set; } = 400;
        public static string[] AppClass { get; set; } = { "办公软件","系统软件","网络通讯","影音媒体","实用工具","安全工具","系统工具","游戏娱乐" };
        public static bool xExit { get; set; } = false;
        public static bool HotKey { get; set; } = false;
        public static string HotKeyCode { get; set; } = "Ctrl+F12";
        public static bool TopMost { get; set; } = false;
        public static bool LineSpace { get; set; } = false;
        public static int LineSpaceValue { get; set; } = 35;
        public static bool DoubleClick { get; set; } = true;
        public static bool OpenMin { get; set; } = false;
        public static bool OpenClose { get; set; } = false;
        public static bool IconSize { get; set; } = false;
        public static int IconSizeValue { get; set; } = 32;
        public static bool FontSize { get; set; } = false;
        public static int FontSizeValue { get; set; } = 20;
        public static bool AdminMode { get; set; } = false;
        public static bool StartUpMin { get; set; } = false;
        public static bool WinStartMin { get; set; } = false;

    }
}
