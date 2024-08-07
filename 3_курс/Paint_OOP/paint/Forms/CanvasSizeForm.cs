using System;
using System.Drawing;
using System.Windows.Forms;

namespace paint
{
    public partial class CanvasSizeForm : Form
    {
        public Size size;
        public CanvasSizeForm()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Enabled = false;
                textBoxHeight.Enabled = true;
                textBoxWidth.Enabled = true;
            }
            else
            {
                textBoxHeight.Enabled = false;
                textBoxWidth.Enabled = false;
                groupBox1.Enabled = true;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                size = new Size(Convert.ToInt32(textBoxWidth.Text), Convert.ToInt32(textBoxHeight.Text));
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    size = new Size(320, 240);
                }
                else if (radioButton2.Checked == true)
                {
                    size = new Size(640, 480);
                }
                else if (radioButton3.Checked == true)
                {
                    size = new Size(800, 600);
                }
            }
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if (System.String.IsNullOrEmpty(textBoxWidth.Text))
            {
                buttonOk.Enabled = false;
            }
            else
            {
                buttonOk.Enabled = true;
            }
        }

        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            if (System.String.IsNullOrEmpty(textBoxHeight.Text))
            {
                buttonOk.Enabled = false;
            }
            else
            {
                buttonOk.Enabled = true;
            }
        }

        private void textBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || (e.KeyChar == '\b'/*backspace*/))
            {
                return;
            }

            e.Handled = true;
        }

        private void textBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || (e.KeyChar == '\b'/*backspace*/))
            {
                return;
            }

            e.Handled = true;
        }
    }
}
