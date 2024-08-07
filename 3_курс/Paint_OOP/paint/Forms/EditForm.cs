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
    public partial class EditForm : Form
    {
        private CanvasForm canvas; 
        private Figure currentFigure;

        public EditForm(CanvasForm canvas)
        {
            InitializeComponent();
            this.canvas = canvas;
            currentFigure = null;
            UpdateListView();
        }

        private void UpdateListView()
        {
            listView.Items.Clear();
            List<Figure> array = canvas.getArray();
            for (int i = 0; i < array.Count; i++)
            {
                Figure figure = array[i];
                String[] item = new String[]{
                    i.ToString(),
                    figure.getName(),
                    figure.getCoors()
                };
                listView.Items.Add(new ListViewItem(item));
            }
        }

        private void UpdateFlowLayoutPanel()
        {
            flowLayoutPanel.Controls.Clear();
            currentFigure = null;
            canvas.UnselectedBox();
            if (listView.SelectedIndices.Count > 0)
            {
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                currentFigure = array[selected];
                currentFigure.showBox = true;
                array[selected] = currentFigure;
                flowLayoutPanel.Controls.AddRange(currentFigure.getControls().ToArray());
                flowLayoutPanel.SetFlowBreak(canvas, true);
            }
            canvas.Refresh();
        }

        private void UpdateButtons()
        {
            DeleteBtn.Enabled = false;
            SaveBtn.Enabled = false;
            ToUpBtn.Enabled = false;
            ToDownBtn.Enabled = false;
            ToBeginBtn.Enabled = false;
            ToEndBtn.Enabled = false;
            if (listView.Items.Count > 0 && listView.SelectedIndices.Count > 0 && currentFigure != null)
            {
                DeleteBtn.Enabled = true;
                SaveBtn.Enabled = true;
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                if (array.Count > 1)
                {
                    if (selected != 0)
                    {
                        ToUpBtn.Enabled = true;
                        ToBeginBtn.Enabled = true;
                    }
                    if (selected != array.Count - 1)
                    {
                        ToDownBtn.Enabled = true;
                        ToEndBtn.Enabled = true;
                    }
                }
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                array.RemoveAt(selected);
            }
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            CreateForm createForm = new CreateForm();
            if (createForm.ShowDialog() == DialogResult.OK)
            {
                FigureType figureType = createForm.figure;
                canvas.createFigure(figureType, new Point(0,0), new Point(0,0));
            }
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            int selected = listView.SelectedIndices[0];
            List<Figure> array = canvas.getArray();
            currentFigure.setParameters(flowLayoutPanel.Controls);
            array[selected] = currentFigure;
            canvas.Refresh();
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ToUpBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                Figure figurePrev = array[selected - 1];
                array[selected] = figurePrev;
                array[selected - 1] = currentFigure;
            }
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void ToDownBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                Figure figureNext = array[selected + 1];
                array[selected] = figureNext;
                array[selected + 1] = currentFigure;
            }
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void ToEndBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                array.RemoveAt(selected);
                array.Add(currentFigure);
            }
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void ToBeginBtn_Click(object sender, EventArgs e)
        {
            if (listView.SelectedIndices.Count > 0)
            {
                int selected = listView.SelectedIndices[0];
                List<Figure> array = canvas.getArray();
                array.RemoveAt(selected);
                array.Insert(0, currentFigure);
            }
            UpdateListView();
            UpdateFlowLayoutPanel();
            UpdateButtons();
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            canvas.UnselectedBox();
        }
    }
}
