using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace PM
{
    internal class WinStartUp
    {
        //LocalMachine  CurrentUser
        public void RegEnableStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(AppConf.AppName, "\"" + Application.ExecutablePath + "\" -Min");
            }
        }

        public void RegDisableStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(AppConf.AppName, false);
            }
        }

        public bool RegIsStartupEnabled()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                return key.GetValue(AppConf.AppName) != null;
            }
        }

        /// <summary>
        /// 创建开机启动计划任务
        /// </summary>
        /*
        public void TaskCreateStartup()
        {
            using (TaskService ts = new TaskService())
            {
                // 创建任务定义
                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "启动PM";

                // 设置触发器（用户登录时）
                td.Triggers.Add(new LogonTrigger());

                // 设置操作（启动程序）
                td.Actions.Add(new ExecAction("\"" + Application.ExecutablePath + "\"", "-Min", Path.GetDirectoryName(Application.ExecutablePath)));

                // 设置以最高权限运行
                td.Principal.RunLevel = TaskRunLevel.Highest;

                // 注册任务（需要管理员权限）
                ts.RootFolder.RegisterTaskDefinition(
                    AppConf.AppName,
                    td,
                    TaskCreation.CreateOrUpdate,
                    null,  // 用户账户（null表示当前用户）
                    null,  // 密码
                    TaskLogonType.InteractiveToken
                );
            }
        }
        public void TaskDelete(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                ts.RootFolder.DeleteTask(taskName, false);
            }
        }
        public bool TaskExists(string taskName)
        {
            using (TaskService ts = new TaskService())
            {
                return ts.GetTask(taskName) != null;
            }
        }
        */
    }
}
