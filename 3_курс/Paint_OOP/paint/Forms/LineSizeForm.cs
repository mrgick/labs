using System;
using System.Windows.Forms;

namespace paint
{
    public partial class LineSizeForm : Form
    {
        public LineSizeForm()
        {
            InitializeComponent();
        }

        public int GetSize()
        {
            Console.WriteLine(comboBox.Text);
            return Convert.ToInt32(comboBox.Text);
        }

        public void SetSize(int size)
        {
            comboBox.Text = Convert.ToString(size);
        }

        private void comboBox_TextChanged(object sender, EventArgs e)
        {
            if (System.String.IsNullOrEmpty(comboBox.Text))
            {
                buttonOk.Enabled = false;
            }
            else
            {
                buttonOk.Enabled = true;
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsNumber(e.KeyChar)) || (e.KeyChar == '\b'/*backspace*/))
            {
                return;
            }

            e.Handled = true;
        }
    }
}
