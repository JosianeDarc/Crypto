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
    public partial class CezarBForm : Form, FormInterface
    {
        //1,3,5,7,9,11,15,17,19,21,23,25
        private decimal prev1, prev2;
        private bool change1, change2;
        private decimal[] values = { 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, 25 };
        public CezarBForm()
        {
            InitializeComponent();
            prev1 = numericUpDown1.Value;
            prev2 = numericUpDown2.Value;
            change1 = change2 = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textResult.Text = CezaraB.Encrypt(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), text.Text);
            }
            catch (ArithmeticException) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textResult.Text = CezaraB.Decrypt(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value), text.Text);
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
            for (int i = values.Length - 1; i >= 0; i-- )
            {
                if (values[i] <= s)
                    return values[i];
            }
            return s;
        }
    }
}
