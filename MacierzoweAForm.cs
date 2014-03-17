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
    public partial class MacierzoweAForm : Form, FormInterface
    {
        public MacierzoweAForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textResult.Text = MacierzoweA.Encrypt(textBox3.Text, text.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textResult.Text = MacierzoweA.Decrypyt(textBox3.Text, text.Text);
        }

        public string getOutput()
        {
            return textResult.Text;
        }

        public void setInput(string value)
        {
            text.Text = value;
        }
    }
}
