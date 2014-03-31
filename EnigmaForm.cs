using Enigma;
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
    public partial class EnigmaForm : Form, FormInterface
    {

        private Rotor rr, rm, rl, reflector;
        public String CurrentPath { get; set; }

        public EnigmaForm()
        {
            InitializeComponent();
            rr = new Rotor("BDFHJLCPRTXVZNYEIWGAKMUSQO", label2, 'V', "RR");
            rm = new Rotor("AJDKSIRUXBLHWTMCQGZNPYFVOE", label3, 'E', "RM");
            rl = new Rotor("EKMFLGDQVZNTOWYHXUSPAIBRCJ", label5, 'Q', "RL");
            reflector = new Rotor("YRUHQSLDPXNGOKMIEBFZCWVJAT", null, '\0', "reflector");

            //J,Z

            rr.SetNextRotor(rm);
            rm.SetNextRotor(rl);
            rl.SetNextRotor(reflector);
            rm.SetPreviousRotor(rr);
            rl.SetPreviousRotor(rm);
            reflector.SetPreviousRotor(rl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] chIn = text.Text.ToUpper().ToCharArray();
            textResult.Text = "";
            for (int i = 0; i < chIn.Length; i++)
            {
                if (chIn[i] >= 65 && chIn[i] <= 90)
                {
                    rr.Move();
                    rr.PutDataIn(chIn[i]);
                    textResult.AppendText("" + rr.GetDataOut());
                }
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
            rl.Move();
            //Console.WriteLine(rl.GetOffset().ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rl.MoveBack();
            //Console.WriteLine(rl.GetOffset().ToString());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            rr.Move();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            rr.MoveBack();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rm.Move();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            rm.MoveBack();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int offset1, offset2, offset3;
            offset1 = rl.GetOffset(); offset2 = rm.GetOffset(); offset3 = rr.GetOffset();

            char[] chIn = text.Text.ToUpper().ToCharArray();
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < chIn.Length; i++)
            {
                if (chIn[i] >= 65 && chIn[i] <= 90)
                {
                    rr.Move();
                    rr.PutDataIn(chIn[i]);
                    result.Append("" + rr.GetDataOut());
                }
            }

            using (Stream stream = File.Open(CurrentPath + "/" + "ekey" + Path.GetFileNameWithoutExtension("Enigma") + ".bin", FileMode.Create))
            {
                var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                bformatter.Serialize(stream, new EnigmaKey(offset1, offset2, offset3));
            }

            System.IO.StreamWriter file = new System.IO.StreamWriter(CurrentPath + "/" + "enigma.txt");
            file.Write(result.ToString());
            file.Close();

            textResult.Text = result.ToString();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (Stream stream = File.Open(dialog.FileName, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                        EnigmaKey key = (EnigmaKey)bformatter.Deserialize(stream);
                        //offset1 = rl.GetOffset(); offset2 = rm.GetOffset(); offset3 = rr.GetOffset();
                        //bformatter.Serialize(stream, new EnigmaKey(offset1, offset2, offset3));

                        rl.SetOffset(key.Key1); rl.ResetLabel();
                        rm.SetOffset(key.Key2); rm.ResetLabel();
                        rr.SetOffset(key.Key3); rr.ResetLabel();

                        char[] chIn = text.Text.ToUpper().ToCharArray();
                        textResult.Text = "";
                        for (int i = 0; i < chIn.Length; i++)
                        {
                            if (chIn[i] >= 65 && chIn[i] <= 90)
                            {
                                rr.Move();
                                rr.PutDataIn(chIn[i]);
                                textResult.AppendText("" + rr.GetDataOut());
                            }
                        }
                        rl.SetOffset(key.Key1); rl.ResetLabel();
                        rm.SetOffset(key.Key2); rm.ResetLabel();
                        rr.SetOffset(key.Key3); rr.ResetLabel();
                    }
                }
                catch (Exception) { }
            }
        }
    }

    [Serializable]
    class EnigmaKey
    {
        public int Key1 { get; set; }
        public int Key2 { get; set; }
        public int Key3 { get; set; }

        public EnigmaKey(int k1, int k2, int k3)
        {
            Key1 = k1;
            Key2 = k2;
            Key3 = k3;
        }
    }
}
