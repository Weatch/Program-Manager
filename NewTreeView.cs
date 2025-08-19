using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;

public class NewTreeView : TreeView
{
    private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
    //private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
    private const int TVS_EX_DOUBLEBUFFER = 0x0004;
    //private const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        // 启用双缓冲和淡入淡出效果
        SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);

        //可选：禁用扩展按钮的淡入淡出效果
        //int style = (int)SendMessage(this.Handle, TVM_GETEXTENDEDSTYLE, IntPtr.Zero, IntPtr.Zero);
        //style &= ~TVS_EX_FADEINOUTEXPANDOS;
        //SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, IntPtr.Zero, (IntPtr)style);
        this.HotTracking = false;
        this.AllowDrop = true;

    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);

        TreeNode node = this.GetNodeAt(e.Location);

        if (node != null)
        {
            this.SelectedNode = node;
        }
    }

}