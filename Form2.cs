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

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private String[] names = { "Form1" };
        public Form2()
        {
            InitializeComponent();
            Form1 form = new Form1();
            TabPage tp = new TabPage("test");
            form.TopLevel = false;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Show();
            tp.Controls.Add(form);
            tabControl.TabPages.Add(tp);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            String[] files = Directory.GetFiles("files", "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                listBox1.Items.Add(files[i]);
            }
        }
    }
}
