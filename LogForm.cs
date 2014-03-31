using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSKCrypto
{
    public partial class LogForm : Form
    {
        private static LogForm instance;
        private LogForm()
        {
            InitializeComponent();
        }

        public static LogForm getInstance()
        {
            if (instance == null)
                instance = new LogForm();
            return instance;
        }

        public static void addLog(String s)
        {
            instance.rtbLog.AppendText(s+"\n");
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
