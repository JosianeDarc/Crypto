using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        private decimal prev1, prev2;
        private bool change1, change2;
        private decimal[] values = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };
        public EncryptingBlocks()
        {
            InitializeComponent();
            operations = new List<BlockInfo>();
            algo = new String[] { "RailFence", "MacierzoweA", "MacierzoweB", "MacierzoweC", "CezaraA", "CezaraB", "Vigenere" };
            foreach (String s in algo)
            {
                cbAlgorithms.Items.Add(s);
            }
            change1 = change2 = true;
            //panel2.Visible = false;
            //panel3.Visible = false;   
        }

        public string getOutput()
        {
            return textResult.Text;
        }

        public List<BlockInfo> getKey()
        {
            return operations;
        }

        public void setInput(string value)
        {
            text.Text = value;
            textResult.Text = "";
            startIndex = 0;
            operations.Clear();
            labCount.Text = Convert.ToString(startIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int rot = Convert.ToInt32(numericUpDown4.Value);
                int bSize = Convert.ToInt32(numBlock.Value);
                String txt = text.Text.Substring(startIndex, bSize);
                for (int i = 0; i < rot; i++)
                {
                    txt = Encrypt(txt);
                }

                BlockInfo bi = new BlockInfo(bSize, cbAlgorithms.SelectedItem.ToString(), rot);
                setKeys(bi);
                operations.Add(bi);
                //textResult.Text += Encrypt(text.Text.Substring(startIndex, Convert.ToInt32(numBlock.Value)));
                textResult.Text += txt;
                startIndex += Convert.ToInt32(numBlock.Value);
            }
            catch (ArgumentOutOfRangeException)
            {
                if (startIndex < text.Text.Length)
                {
                    int size = text.Text.Length;
                    int endIndex = size - startIndex;

                    int rot = Convert.ToInt32(numericUpDown4.Value);
                    String txt = text.Text.Substring(startIndex, endIndex);
                    for (int i = 0; i < rot; i++)
                    {
                        txt = Encrypt(txt);
                    }

                    BlockInfo bi = new BlockInfo(endIndex - 1, cbAlgorithms.SelectedItem.ToString(), rot);
                    setKeys(bi);
                    operations.Add(bi);
                    textResult.Text += txt;
                    //textResult.Text += Encrypt(text.Text.Substring(startIndex, endIndex - 1));
                    startIndex += endIndex;
                }
            }
            labCount.Text = Convert.ToString(startIndex);
        }

        private void setKeys(BlockInfo bi)
        {
            if (cbAlgorithms.SelectedItem.Equals(algo[5])) // CezaraB
            {
                bi.keyOne = Convert.ToString(numericUpDown1.Value);
                bi.keyTwo = Convert.ToString(numericUpDown2.Value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[4]) || cbAlgorithms.SelectedItem.Equals(algo[0]))
            {
                bi.keyOne = Convert.ToString(numericUpDown3.Value);
            }
            else
            {
                bi.keyOne = textKey.Text;
            }
            //Console.WriteLine("Dodaje klucze dla: " + bi.ToString());
        }

        private void EncryptingBlocks_Load(object sender, EventArgs e)
        {
            numBlock.Value = 10;
            cbAlgorithms.SelectedIndex = 0;
        }

        private String Encrypt(String value)
        {
            //algo = new String[] { "RailFence", "MacierzoweA", "MacierzoweB", "MacierzoweC", "CezaraA", "CezaraB", "Vigenere" };
            if (cbAlgorithms.SelectedIndex == -1)
                return "";
            if (cbAlgorithms.SelectedItem.Equals(algo[0]))
            {
                return RailFence.Encrypt(Convert.ToInt32(numericUpDown3.Value), value);
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
                return MacierzoweC.Encrypt(textKey.Text, value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[4]))
            {
                return CezaraA.Encrypt(Convert.ToInt32(numericUpDown3.Value), value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[5]))
            {
                return CezaraB.Encrypt(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), value);
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[6]))
            {
                Vigenere v = new Vigenere(textKey.Text);
                return v.Encrypt(value);
            }
            return "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textResult.Text = "";
            startIndex = 0;
            operations.Clear();
            labCount.Text = Convert.ToString(startIndex);
        }

        private void cbAlgorithms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAlgorithms.SelectedIndex == -1)
            {

            }
            if (cbAlgorithms.SelectedItem.Equals(algo[0]))
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = true;
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[1]))
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
                label8.Text = "Klucz (liczby oddzielony myślnikami)";
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[2]))
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
                label8.Text = "Klucz (tekst)";
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[3]))
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
                label8.Text = "Klucz (tekst)";
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[4]))
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = true;
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[5]))
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
            }
            else if (cbAlgorithms.SelectedItem.Equals(algo[6]))
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
                label8.Text = "Klucz (tekst)";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (change1)
            {
                change1 = !change1;
                if (numericUpDown1.Value > prev1)
                {
                    numericUpDown1.Value = nextNumber(numericUpDown1.Value);
                }
                else if (numericUpDown1.Value < prev1)
                {
                    numericUpDown1.Value = prevNumber(numericUpDown1.Value);
                }
                prev1 = numericUpDown1.Value;
                change1 = !change1;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (change2)
            {
                change2 = !change2;
                if (numericUpDown2.Value > prev2)
                {
                    numericUpDown2.Value = nextNumber(numericUpDown2.Value);
                }
                else if (numericUpDown2.Value < prev2)
                {
                    numericUpDown2.Value = prevNumber(numericUpDown2.Value);
                }
                prev2 = numericUpDown2.Value;
                change2 = !change2;
            }
        }

        private decimal nextNumber(decimal s)
        {
            foreach (decimal d in values)
            {
                if (d >= s)
                    return d;
            }
            return s;
        }
        private decimal prevNumber(decimal s)
        {
            for (int i = values.Length - 1; i >= 0; i--)
            {
                if (values[i] <= s)
                    return values[i];
            }
            return s;
        }

        public override string ToString()
        {
            return "Blocks";
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(dialog.FileName);
                try
                {
                    using (Stream stream = File.Open(dialog.FileName, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                        List<BlockInfo> op = (List<BlockInfo>)bformatter.Deserialize(stream);
                        linkLabel2.Text = Path.GetFileName(dialog.FileName);
                        operations.Clear();
                        foreach (BlockInfo bi in op)
                        {
                            Console.WriteLine(bi.ToString());
                            operations.Add(bi);
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            startIndex = 0;
            textResult.Text = "";
            foreach (BlockInfo bi in operations)
            {
                try
                    {
                        int rot = bi.EncryptCount;
                        int bSize = bi.BlockSize;
                        String txt = text.Text.Substring(startIndex, bSize);
                        for (int i = 0; i < rot; i++)
                        {
                            txt = Decrypt(bi, txt);
                        }

                        //textResult.Text += Encrypt(text.Text.Substring(startIndex, Convert.ToInt32(numBlock.Value)));
                        textResult.Text += txt;
                        startIndex += bi.BlockSize;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        if (startIndex < text.Text.Length)
                        {
                            int size = text.Text.Length;
                            int endIndex = size - startIndex;

                            int rot = Convert.ToInt32(bi.EncryptCount);
                            String txt = text.Text.Substring(startIndex, endIndex);
                            for (int i = 0; i < rot; i++)
                            {
                                txt = Decrypt(bi, txt);
                            }

                            textResult.Text += txt;
                            //textResult.Text += Encrypt(text.Text.Substring(startIndex, endIndex - 1));
                            startIndex += endIndex;
                        }
                    }
                //labCount.Text = Convert.ToString(startIndex);
            }
        }

        private String Decrypt(BlockInfo bi, String value)
        {
            //algo = new String[] { "RailFence", "MacierzoweA", "MacierzoweB", "MacierzoweC", "CezaraA", "CezaraB", "Vigenere" };
            if (bi == null)
                return "";
            if (bi.EncryptMethod.Equals(algo[0]))
            {
                return RailFence.Decrypt(Convert.ToInt32(bi.keyOne), value);
            }
            else if (bi.EncryptMethod.Equals(algo[1]))
            {
                return MacierzoweA.Decrypt(bi.keyOne, value);
            }
            else if (bi.EncryptMethod.Equals(algo[2]))
            {
                return MacierzoweB.Decrypt(bi.keyOne, value);
            }
            else if (bi.EncryptMethod.Equals(algo[3]))
            {
                return MacierzoweC.Decrypt(bi.keyOne, value);
            }
            else if (bi.EncryptMethod.Equals(algo[4]))
            {
                return CezaraA.Decrypt(Convert.ToInt32(bi.keyOne), value);
            }
            else if (bi.EncryptMethod.Equals(algo[5]))
            {
                return CezaraB.Decrypt(Convert.ToInt32(bi.keyOne), Convert.ToInt32(bi.keyTwo), value);
            }
            else if (bi.EncryptMethod.Equals(algo[6]))
            {
                Vigenere v = new Vigenere(bi.keyOne);
                return v.Encrypt(value);
            }
            return "";
        }
    }

    [Serializable]
    public class BlockInfo
    {
        public int BlockSize { get; set; }
        public String EncryptMethod { get; set; }
        public int EncryptCount { get; set; }
        public String keyOne { get; set; }
        public String keyTwo { get; set; }

        public BlockInfo(int bs, String em, int ec)
        {
            BlockSize = bs;
            EncryptMethod = em;
            EncryptCount = ec;
        }
        public override string ToString()
        {
            return Convert.ToString(BlockSize) + " " + EncryptMethod + " " + Convert.ToString(EncryptCount);
        }
    }
}
