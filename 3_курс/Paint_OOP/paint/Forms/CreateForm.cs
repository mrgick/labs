using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint.Forms
{
    public partial class CreateForm : Form
    {
        public FigureType figure = FigureType.Line;

        public CreateForm()
        {
            InitializeComponent();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedIndex = comboBox.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    figure = FigureType.Line;
                    break;
                case 1:
                    figure = FigureType.Curve;
                    break;
                case 2:
                    figure = FigureType.Rectangle;
                    break;
                case 3:
                    figure = FigureType.Ellipse;
                    break;
                case 4:
                    figure = FigureType.Text;
                    break;
                default:
                    break;
            }
            buttonOk.Enabled = true;
        }
    }
}
