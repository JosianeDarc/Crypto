﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BSKCrypto
{
    public partial class RailFenceForm : Form, FormInterface
    {

        public RailFenceForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                textResult.Text = RailFence.Encrypt(n, text.Text);
            }
            catch (ArithmeticException) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                textResult.Text = RailFence.Decrypt(n, text.Text);
            }
            catch (ArithmeticException) { }
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
