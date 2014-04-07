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
    public partial class DESForm : Form, FormInterface
    {
        public DESForm()
        {
            InitializeComponent();
        }

        public string getOutput()
        {
            return textResult.Text;
        }

        public void setInput(string value)
        {
            text.Text = value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                DES d = new DES();
                //textResult.Text = d.EncryptText("0E329232EA6D0D73", "Your lips are smoother than vaseline");
                //textResult.Text = d.EncryptText(textBox3.Text, text.Text);
                textResult.Text = d.Encrypt3DES(textBox3.Text, textBox4.Text, text.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DES d = new DES();
                //textResult.Text = d.DecryptText("0E329232EA6D0D73", text.Text);
                //textResult.Text = d.DecryptText(textBox3.Text, text.Text);
                textResult.Text = d.Decrypt3DES(textBox3.Text, textBox4.Text, text.Text);
            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StringBuilder b = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < 16; i++)
            {
                b.Append(toChar(rand.Next(0, 15)));
            }
            textBox3.Text = b.ToString();

            b.Clear();
            for (int i = 0; i < 16; i++)
            {
                b.Append(toChar(rand.Next(0, 15)));
            }
            textBox4.Text = b.ToString();
        }

        private char toChar(int p)
        {
            if (p < 10)
                return (char) (p + 48);
            else
                return (char) (p + 55);
        }
    }
}
