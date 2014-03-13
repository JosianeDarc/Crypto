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
    public partial class MacierzoweAForm : Form
    {
        public MacierzoweAForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MacierzoweA a = new MacierzoweA();

            textBox2.Text = a.Encrypt(textBox1.Text);
        }
    }
}
