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
    public partial class EncryptingBlocks : Form, FormInterface
    {
        private int startIndex = 0;
        private String[] algo;
        public RichTextBox rtb { get { return text; } }
        public EncryptingBlocks()
        {
            InitializeComponent();
            algo = new String[] { "RailFence", "MacierzoweA", "MacierzoweB", "Vigenere" };
            foreach (String s in algo)
            {
                cbAlgorithms.Items.Add(s);
            }
        }

        public string getOutput()
        {
            return textResult.Text;
        }

        public void setInput(string value)
        {
            text.Text = value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textResult.Text += text.Text.Substring(startIndex, Convert.ToInt32(numBlock.Value));
                startIndex += Convert.ToInt32(numBlock.Value);
                labCount.Text = Convert.ToString(startIndex);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        private void EncryptingBlocks_Load(object sender, EventArgs e)
        {
            numBlock.Value = 100;
        }

        private String Encrypt(String value)
        {
            if (cbAlgorithms.SelectedIndex == -1)
                return "";
            if (cbAlgorithms.SelectedItem.Equals(algo[0]))
            {
                return RailFence.Encrypt(Convert.ToInt32(textKey.Text), value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[1]))
            {
                return MacierzoweA.Encrypt(textKey.Text, value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[2]))
            {
                return MacierzoweB.Encrypt(textKey.Text, value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[3]))
            {
                Vigenere v = new Vigenere(textKey.Text);
                return v.Encrypt(text.Text);
            }
            return "";
        }
    }

    public class BlockInfo
    {
        public int BlockSize { get; set; }
        public String EncryptMethod { get; set; }
        public int EncryptCount { get; set; }
    }
}
