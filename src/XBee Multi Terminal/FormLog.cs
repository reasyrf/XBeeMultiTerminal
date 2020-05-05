using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace XBee_Multi_Terminal
{
    public partial class FormLog : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public FormLog()
        {
            InitializeComponent();
        }

        private void FormLog_Load(object sender, EventArgs e)
        {
            timerTrimLog.Start();
        }

        private void timerTrimLog_Tick(object sender, EventArgs e)
        {
            Int32 maxsize = 100000;
            Int32 dropsize = maxsize / 4;

            if (richTextBoxLog.Text.Length > maxsize)
            {
                // this method preserves the text colouring
                // find the first end-of-line past the endmarker

                Int32 endmarker = richTextBoxLog.Text.IndexOf('\n', dropsize) + 1;
                if (endmarker < dropsize)
                    endmarker = dropsize;

                richTextBoxLog.Select(0, endmarker);
                richTextBoxLog.SelectedText = "";
            }
        }

        private void richTextBoxLog_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; //Ignore all keys. Normally we would use the read only attribute, but we need this to be true to "scroll" the logs
        }
    }
}
