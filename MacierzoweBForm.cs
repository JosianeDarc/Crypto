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
    public partial class MacierzoweBForm : Form, FormInterface
    {
        public MacierzoweBForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textResult.Text = MacierzoweB.Encrypt(textBox3.Text, text.Text);
            //textBox2.Text = MacierzoweB.Encrypt("CONVENIENCE", "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textResult.Text = MacierzoweB.Decrypt(textBox3.Text, text.Text);
            //textBox2.Text = MacierzoweB.Decrypt("CONVENIENCE", "HECRNCEYIISEPSGDIRNTOAAESRMPNSSROEEBTETIAEEHS");
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
