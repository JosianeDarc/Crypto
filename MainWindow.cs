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
    public partial class MainWindow : Form
    {
        private Dictionary<string, Control> tabs;
        private EncryptingBlocks blocks;
        public MainWindow()
        {
            InitializeComponent();

            tabs = new Dictionary<string, Control>();
            
            tabs.Add("RailFence", new RailFenceForm());
            tabs.Add("MacierzoweA", new MacierzoweAForm());
            tabs.Add("MacierzoweB", new MacierzoweBForm());
            tabs.Add("Kodowanie bloków", blocks = new EncryptingBlocks());

            createTabs();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            String[] files = Directory.GetFiles("files", "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                listBox1.Items.Add(files[i]);
            }
        }

        private void createTabs()
        {
            foreach (KeyValuePair<string, Control> pair in tabs)
            {
                Form formControl = (Form)pair.Value;
                TabPage tbp = new TabPage(pair.Key);
                formControl.TopLevel = false;
                formControl.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                formControl.WindowState = FormWindowState.Maximized;
                formControl.Show();
                tbp.Controls.Add(formControl);
                tabControl.TabPages.Add(tbp);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                String[] files = Directory.GetFiles(dialog.SelectedPath, "*.txt");
                for (int i = 0; i < files.Length; i++)
                {
                    listBox1.Items.Add(files[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            blocks.rtb.Clear();
            //listBox1.SelectedItem
            //blocks.rtb.Text = "aaaaa\naaaa";
            try
            {
                using (StreamReader sr = new StreamReader(listBox1.SelectedItem.ToString()))
                {
                    String line = sr.ReadToEnd();
                    blocks.rtb.Text += line;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
