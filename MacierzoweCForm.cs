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
    public partial class MacierzoweCForm : Form, FormInterface
    {
        public MacierzoweCForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textResult.Text = MacierzoweC.Encrypt(textBox3.Text, text.Text);
            //textResult.Text = MacierzoweC.Encrypt("CONVENIENCE", "HERE IS A SECRET MESSAGE ENCIPHERED BY TRANSPOSITION");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textResult.Text = MacierzoweC.Decrypt(textBox3.Text, text.Text);
            //textBox2.Text = MacierzoweC.Decrypt("CONVENIENCE", "HECRNCEYIISEPSGDIRNTOAAESRMPNSSROEEBTETIAEEHS");
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
