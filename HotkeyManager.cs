using PM;
using System;
using System.CodeDom;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class HotkeyManager : NativeWindow, IDisposable
{
    private const int WM_HOTKEY = 0x0312;
    private const uint MOD_ALT = 0x1;
    private const uint MOD_CONTROL = 0x2;
    private const uint MOD_SHIFT = 0x4;
    private const uint MOD_WIN = 0x8;
    private const uint MOD_NOREPEAT = 0x4000;
    private readonly int _hotkeyId;
    private DateTime _lastTriggerTime = DateTime.MinValue;

    public event Action HotkeyPressed;

    public HotkeyManager(int hotkeyId)
    {
        _hotkeyId = hotkeyId;        
        CreateHandle(new CreateParams());   // 创建一个不可见窗口
    }

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == _hotkeyId ) 
        {
            if ((DateTime.Now - _lastTriggerTime).TotalMilliseconds > 200)
            {
                _lastTriggerTime = DateTime.Now;
                HotkeyPressed?.Invoke();
            }
        }
        base.WndProc(ref m);
    }
    public bool Start(string strKeys)
    {
        uint modifiers = 0;
        uint key;
        if (strKeys.Contains("Alt+")) modifiers += MOD_ALT;
        if (strKeys.Contains("Ctrl+")) modifiers += MOD_CONTROL;
        if (strKeys.Contains("Shift+")) modifiers += MOD_SHIFT;
        if (strKeys.Contains("Win+")) modifiers += MOD_WIN;
        modifiers += MOD_NOREPEAT;
        string keyStr = strKeys.Split('+').Last();        
        if (Enum.TryParse(keyStr, out Keys keys))
        {
            key = (uint)keys;
        }
        else
        {
            key = 123;
            modifiers = MOD_NOREPEAT + MOD_CONTROL;
            AppConf.HotKeyCode = "Ctrl+F12";
        }
        bool b= RegisterHotKey(Handle, _hotkeyId, (uint)modifiers, (uint)key);
        return b;
    }

    public void Stop()
    {
        UnregisterHotKey(Handle, _hotkeyId);
        DestroyHandle();
    }

    public void Dispose()
    {
        UnregisterHotKey(Handle, _hotkeyId);
        DestroyHandle();
    }

    [DllImport("user32.dll")]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    [Flags]
    public enum KeyModifiers
    {
        None = 0x0,
        ALT = 0x1,
        CONTROL = 0x2,
        SHIFT = 0x4,
        WIN = 0x8,
        NOREPEAT = 0x4000
    }
}
