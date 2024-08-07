using System;
using System.Windows.Forms;


namespace paint.Forms
{
    public partial class GridSizeForm : Form
    {
        public GridSizeForm()
        {
            InitializeComponent();
        }

        public int GetPitch()
        {
            Console.WriteLine(textBox.Text);
            return Convert.ToInt32(textBox.Text);
        }

        public void SetPitch(int pitch)
        {
            textBox.Text = Convert.ToString(pitch);
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox.Text))
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
    }
}
