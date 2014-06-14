using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MW2_Server
{
    public enum level
    {
        None,
        System,
        Output,
        Input
    }

    class Log
    {

        private RichTextBox logBox;
        private Main form;

        public Log(RichTextBox _logbox, Main _form)
        {
            logBox = _logbox;
            form = _form;
        }

        private void CearLog()
        {
            if (logBox.Text.Count(a => a == '\n') > 200)
            {
                logBox.Text = "";
            }
        }

        public void Print(level loglevel, string message)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action<level, string>(this.Print), new object[] { loglevel, message });
                return;
            }

            logBox.Text += (logBox.Text == "" ? "" : "\n");

            CearLog();

            if (loglevel == level.None)
            {

            }
            else if (loglevel == level.System)
            {
                logBox.Text += "System - ";
            }
            else if (loglevel == level.Output)
            {
                logBox.Text += "Output - ";
            }
            else if (loglevel == level.Input)
            {
                logBox.Text += "Input  - ";
            }

            logBox.Text += message;
        }
    }
}