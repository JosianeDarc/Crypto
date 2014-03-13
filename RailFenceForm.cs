using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BSKCrypto
{
    public partial class RailFenceForm : Form
    {

        public RailFenceForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                textBox2.Text = RailFence.Encrypt(n, textBox1.Text);
            }
            catch (ArithmeticException) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                textBox2.Text = RailFence.Decrypt(n, textBox1.Text);
            }
            catch (ArithmeticException) { }
        }
    }
}
