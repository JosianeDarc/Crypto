using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*int n = Convert.ToInt32(numericUpDown1.Value);
            RailFence rf = new RailFence(n);
            //textBox2.Text = rf.Encrypt(textBox1.Text);
            try
            {
                using (StreamReader sr = new StreamReader(listBox1.SelectedItem.ToString()))
                {
                    String line = sr.ReadToEnd();
                    textBox1.Text = line;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }*/
            int n = Convert.ToInt32(numericUpDown1.Value);
            RailFence rf = new RailFence(n);
            textBox2.Text = rf.Encrypt(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(numericUpDown1.Value);
            RailFence rf = new RailFence(n);
            textBox2.Text = rf.Decrypt(textBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            String[] files = Directory.GetFiles("files", "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                listBox1.Items.Add(files[i]);
            }*/
        }
    }
}
