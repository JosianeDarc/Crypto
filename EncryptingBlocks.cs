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
        private List<BlockInfo> operations;
        public EncryptingBlocks()
        {
            InitializeComponent();
            operations = new List<BlockInfo>();
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
            textResult.Text = "";
            startIndex = 0;
            labCount.Text = Convert.ToString(startIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textResult.Text += Encrypt(text.Text.Substring(startIndex, Convert.ToInt32(numBlock.Value)));
                startIndex += Convert.ToInt32(numBlock.Value);
            }
            catch (ArgumentOutOfRangeException)
            {
                if (startIndex < text.Text.Length)
                {
                    int size = text.Text.Length;
                    int endIndex = size - startIndex;
                    textResult.Text += Encrypt(text.Text.Substring(startIndex, endIndex - 1));
                    startIndex += endIndex;
                }
            }
            labCount.Text = Convert.ToString(startIndex);
        }

        private void EncryptingBlocks_Load(object sender, EventArgs e)
        {
            numBlock.Value = 10;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textResult.Text = "";
            startIndex = 0;
            labCount.Text = Convert.ToString(startIndex);
        }
    }

    public class BlockInfo
    {
        public int BlockSize { get; set; }
        public String EncryptMethod { get; set; }
        public int EncryptCount { get; set; }
    }
}
