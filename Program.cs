using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM
{
    internal static class Program
    {
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        private const int SW_RESTORE = 9; // 恢复窗口（如果最小化）
        private const int SW_SHOW = 5;    // 显示窗口（如果隐藏）

        private static Mutex _mutex;
        private const string AppMutexName = "PM";

        [STAThread]   
        static void Main(string[] args)
        {
            bool createdNew;
            _mutex = new Mutex(true, AppMutexName, out createdNew);
            if (!createdNew)
            {
                TryActivateExistingInstance();
                return;
            }
            AppInit(args);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PM());
            _mutex?.ReleaseMutex();
        }
        private static void AppInit(string[] strArgs)
        {
            //if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1) { return; }
            bool ReSetup = false;
            bool RePosition = false;
            foreach(string str in strArgs)
            {
                string strArg = str.ToUpper();
                char[] charsToRemove = { '/', '-', ' ' };
                foreach (char c in charsToRemove)
                {
                    strArg = strArg.Replace(c.ToString(), string.Empty);
                }
                if (strArg == "MIN" ) { AppConf.WinStartMin = true; }
                if (strArg == "REPOSITION") { RePosition = true; }
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            AppConf.Ver = assemblyName.Version.ToString();

            var copyrightAttribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            AppConf.Copyright = copyrightAttribute.Copyright;

            var productAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            AppConf.Attr = productAttribute.Product;

            AppConf.AppPath = AppDomain.CurrentDomain.BaseDirectory;
            AppConf.DbPath = AppConf.AppPath + AppConf.AppName + ".db";
            Console.WriteLine($"AppPath:{AppConf.AppPath},DbPath:{AppConf.DbPath}");
            if (!File.Exists(AppConf.DbPath))
            {                
                FileStream fs = File.Create(AppConf.DbPath);
                fs.Dispose();
                InitDb();
            }
            InitConf();
            if (RePosition)
            {
                AppConf.Y = 0;
                AppConf.X = 0;
            }
        }
        private static void InitConf()
        {
            string Conf = QueryConf("Class");           if (Conf != null) AppConf.AppClass = Conf.Split(',');
            Conf = QueryConf("Top");                    if (Conf != null) AppConf.Y = int.Parse(Conf);
            Conf = QueryConf("Left");                   if (Conf != null) AppConf.X = int.Parse(Conf);
            Conf = QueryConf("Width");                  if (Conf != null) AppConf.Width = int.Parse(Conf);
            Conf = QueryConf("Height");                 if (Conf != null) AppConf.Height = int.Parse(Conf);
            Conf = QueryConf("FormText");               if (Conf != null) AppConf.FormText = Conf;
            Conf = QueryConf("xExit");                  if (Conf != null) AppConf.xExit = int.Parse(Conf) == 1;
            Conf = QueryConf("DoubleClick");            if (Conf != null) AppConf.DoubleClick = int.Parse(Conf) == 1;
            Conf = QueryConf("OpenClose");              if (Conf != null) AppConf.OpenClose = int.Parse(Conf) == 1;
            Conf = QueryConf("OpenMin");                if (Conf != null) AppConf.OpenMin = int.Parse(Conf) == 1;
            Conf = QueryConf("TopMost");                if (Conf != null) AppConf.TopMost = int.Parse(Conf) == 1;
            Conf = QueryConf("HotKey");                 if (Conf != null) AppConf.HotKey = int.Parse(Conf) == 1;
            Conf = QueryConf("HotKeyCode");             if (Conf != null) AppConf.HotKeyCode = Conf;
            Conf = QueryConf("LineSpace");              if (Conf != null) AppConf.LineSpace = int.Parse(Conf) == 1;
            Conf = QueryConf("LineSpaceValue");         if (Conf != null) AppConf.LineSpaceValue = int.Parse(Conf);
            Conf = QueryConf("IconSize");               if (Conf != null) AppConf.IconSize = int.Parse(Conf) == 1;
            Conf = QueryConf("IconSizeValue");          if (Conf != null) AppConf.IconSizeValue = int.Parse(Conf);
            Conf = QueryConf("FontSize");               if (Conf != null) AppConf.FontSize = int.Parse(Conf) == 1;
            Conf = QueryConf("FontSizeValue");          if (Conf != null) AppConf.FontSizeValue = int.Parse(Conf);
            Conf = QueryConf("StartUpMin");             if (Conf != null) AppConf.StartUpMin = int.Parse(Conf) == 1;
        }
        private static string QueryConf(string key)
        {
            var sql = new SQLite(AppConf.DbPath);
            string querySql = "SELECT value FROM sys WHERE key=@key";
            var queryParams = new SQLiteParameter[] { new SQLiteParameter("@key", key) };
            string str = sql.ExecuteString(querySql, queryParams);
            if (str == null) 
            { 
                return null;
            } else
            {
                return str.Trim();
            }                
        }
        private static void InitDb()
        {
            var sql = new SQLite(AppConf.DbPath);
            sql.CreateTable(@"CREATE TABLE 'lnk' (
	                            'id'	INTEGER,
	                            'name'	TEXT,
	                            'class'	TEXT,
	                            'description'	TEXT,
	                            'icon'	TEXT,
	                            'path'	TEXT,
	                            'workdir'	TEXT,
	                            'arguments'	TEXT,
	                            'rem'	TEXT,
	                            PRIMARY KEY('id' AUTOINCREMENT))");
            sql.CreateTable(@"CREATE TABLE 'sys' (
	                            'id'	INTEGER,
	                            'key'	TEXT,
	                            'value'	TEXT,
	                            PRIMARY KEY('id' AUTOINCREMENT))");
            sql = null;
            InsertSys("Class", "办公软件,系统软件,网络通讯,影音媒体,实用工具,安全工具,系统工具,游戏娱乐");
            InsertSys("Top","0");
            InsertSys("Left","0");
            InsertSys("Width", "300");
            InsertSys("Height", "400");
            InsertSys("FormText", "PM");
            InsertSys("xExit", "0");
            InsertSys("DoubleClick", "1");
            InsertSys("OpenClose", "0");
            InsertSys("OpenMin", "0");
            InsertSys("TopMost", "0");
            InsertSys("HotKey", "0");
            InsertSys("HotKeyCode", "Ctrl+Oemtilde");
            InsertSys("LineSpace", "0");
            InsertSys("LineSpaceValue", "32");
            InsertSys("IconSize", "0");
            InsertSys("IconSizeValue", "32");
            InsertSys("FontSize", "0");
            InsertSys("FontSizeValue", "15");
            InsertSys("StartUpMin", "0");
        }
        private static void InsertSys(string key,string value)
        {
            var sql = new SQLite(AppConf.DbPath);
            string strsql = "INSERT INTO sys (key,value) VALUES (@key,@value)";
            var parameters = new SQLiteParameter[] {
                        new SQLiteParameter("@key", key),
                        new SQLiteParameter("@value", value)
                       };
            sql.ExecuteNonQuery(strsql, parameters);
        }
        private static void TryActivateExistingInstance()
        {
            var processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            foreach (var process in processes)
            {
                if (process.Id != Process.GetCurrentProcess().Id)
                {
                    IntPtr hWnd = process.MainWindowHandle;
                    if (hWnd != IntPtr.Zero)
                    {
                        ShowWindow(hWnd, SW_RESTORE);
                        ShowWindow(hWnd, SW_SHOW);
                        SetForegroundWindow(hWnd);
                        return;
                    }
                }
            }
            try
            {
                using (var pipeClient = new NamedPipeClientStream(
                    ".",
                    AppConf.PipeName,
                    PipeDirection.Out,
                    PipeOptions.Asynchronous))
                {
                    pipeClient.Connect(3000);
                    if (pipeClient.IsConnected)
                    {
                        using (var writer = new StreamWriter(pipeClient))
                        {
                            writer.WriteLine("RESTORE");
                            writer.Flush();                            
                            pipeClient.WaitForPipeDrain();  // 可选：等待确认
                        }
                    }
                }
            }
            catch (TimeoutException)
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IPC连接错误: {ex.Message}");
            }
        }

    }

}
