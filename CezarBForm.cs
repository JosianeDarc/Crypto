﻿using System;
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
    public partial class CezarBForm : Form, FormInterface
    {
        public CezarBForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                textResult.Text = CezaraB.Encrypt(n, text.Text);
            }
            catch (ArithmeticException) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int n = Convert.ToInt32(numericUpDown1.Value);
                textResult.Text = CezaraB.Decrypt(n, text.Text);
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
