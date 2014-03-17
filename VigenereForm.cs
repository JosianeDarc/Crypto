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
    public partial class VigenereForm : Form, FormInterface
    {
        public VigenereForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vigenere v = new Vigenere(textKey.Text);
            textResult.Text = v.Encrypt(text.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Vigenere v = new Vigenere(textKey.Text);
            textResult.Text = v.Decrypt(text.Text);
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
