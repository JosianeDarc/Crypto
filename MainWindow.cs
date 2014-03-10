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
    public partial class MainWindow : Form
    {
        private Dictionary<string, Control> tabs;

        public MainWindow()
        {
            InitializeComponent();

            tabs = new Dictionary<string, Control>();

            
            tabs.Add("RailFence", new RailFenceForm());
            tabs.Add("MacierzoweA", new MacierzoweA());
            tabs.Add("MacierzoweB", new MacierzoweB());

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
                formControl.Show();
                tbp.Controls.Add(formControl);
                tabControl.TabPages.Add(tbp);
            }
            /*
            RailFenceForm form = new RailFenceForm();
            TabPage tp = new TabPage("test");
            form.TopLevel = false;
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            form.Show();
            tp.Controls.Add(form);
            tabControl.TabPages.Add(tp);
             */
        }
    }
}
