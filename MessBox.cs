using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PM
{
    public partial class MessBox : Form
    {
        public MessBox()
        {
            InitializeComponent();
        }
        public MessBox(bool showYes, string text1, string text2="", string caption="确认")
        {
            InitializeComponent();
            if (!showYes) { button2.Visible = false; }
            label1.Text = text1;
            label2.Text = text2;
            this.Text = caption;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
