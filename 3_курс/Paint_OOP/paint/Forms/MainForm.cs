using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Imaging;
using paint.Forms;

namespace paint
{
    public partial class MainForm : Form
    {
        public Color solidColor;
        private Color tmpSolidColor;
        public Color lineColor;
        public int lineSize;
        public FigureType figureType;
        public Size canvasSize;
        public Font canvasDefaultFont;
        public Font canvasFont;
        public bool selectMode;
        public bool deleteMode;
        public bool gridMode;
        public bool autoGridMode;
        public int gridPitch;
        public MainForm()
        {
            InitializeComponent();
            solidColor = Color.White;
            lineColor = Color.Black;
            canvasSize = new Size(640, 480);
            figureType = FigureType.Line;
            lineToolStripMenuItem.Checked = true;
            lineSize = 1;
            fillColorToolStripMenuItem.Enabled = false;
            fillToolStripMenuItem.Checked = true;
            canvasDefaultFont = new Font("Times New Roman", 12);
            canvasFont = canvasDefaultFont;
            selectMode = false;
            gridMode = false;
            autoGridMode = false;
            gridPitch = 10;
            UpdateEditButtons();
        }

        public void DisableOrEnableSave(int count = 0)
        {
            bool flag = true;
            if (MdiChildren.Length <= count)
            {
                flag = false;
            }
            saveToolStripMenuItem.Enabled = flag;
            saveAsToolStripMenuItem.Enabled = flag;
            saveToolStripButton.Enabled = flag;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new CanvasForm(canvasSize)
            {
                MdiParent = this,
                Text = "Рисунок " + MdiChildren.Length.ToString(),
            };
            DisableOrEnableSave();

            f.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory
            };

            DialogResult dialogResult = openFileDialog.ShowDialog();


            for (int i = 0; i < MdiChildren.Length; ++i)
            {
                CanvasForm canvas = (CanvasForm)MdiChildren[i];
                if (canvas.FilePathSave == openFileDialog.FileName)
                {
                    MessageBox.Show("Файл с данным именем уже открыт!");
                    return;
                }
            }


            if (dialogResult == DialogResult.OK)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                List<Figure> array = (List<Figure>)formatter.Deserialize(stream);
                Size size = (Size)formatter.Deserialize(stream);
                stream.Close();

                CanvasForm canvas = new CanvasForm(size)
                {
                    Array = array,
                    Text = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1),
                    FilePathSave = openFileDialog.FileName
                };

                Form f = canvas;
                f.MdiParent = this;

                DisableOrEnableSave();

                f.Show();
            }
        }

        public void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;

            if (canvas.FilePathSave == "")
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(canvas.FilePathSave, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, canvas.Array);
                formatter.Serialize(stream, canvas.workPlaceSize);
                stream.Close();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = "pic",
                Title = "Сохранить",
                FileName = "Изображение",
                InitialDirectory = Environment.CurrentDirectory
            };

            DialogResult dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                CanvasForm canvas = (CanvasForm)ActiveMdiChild;

                canvas.FilePathSave = saveFileDialog.FileName;
                canvas.Text = saveFileDialog.FileName.Substring(saveFileDialog.FileName.LastIndexOf('\\') + 1);

                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, canvas.Array);
                formatter.Serialize(stream, canvas.workPlaceSize);
                stream.Close();
            }
        }
        
        private void lineColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                CanvasForm canvas = (CanvasForm)ActiveMdiChild;
                if (canvas != null && canvas.getEditFigure() != null)
                {
                    Figure figure = canvas.getEditFigure();
                    figure.setLineColor(colorDialog.Color);
                }
                else
                {
                    lineColor = colorDialog.Color;
                    drawStatusBar();
                }
            }
        }

        private void fillColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                CanvasForm canvas = (CanvasForm)ActiveMdiChild;
                if (canvas != null && canvas.getEditFigure() != null)
                {
                    Figure figure = canvas.getEditFigure();
                    figure.setSolidColor(colorDialog.Color);
                }
                else
                {
                    solidColor = colorDialog.Color;
                    drawStatusBar();
                }
            }
        }

        private void lineSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LineSizeForm lineSize = new LineSizeForm();

            lineSize.SetSize(this.lineSize);
            if (lineSize.ShowDialog() == DialogResult.OK)
            {
                CanvasForm canvas = (CanvasForm)ActiveMdiChild;
                if (canvas != null && canvas.getEditFigure() != null)
                {
                    Figure figure = canvas.getEditFigure();
                    figure.setLineSize(lineSize.GetSize());
                }
                else
                {
                    this.lineSize = lineSize.GetSize();
                    drawStatusBar();
                }
            }
        }

        private void canvasSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSizeForm canvasSize = new CanvasSizeForm();

            if (canvasSize.ShowDialog() == DialogResult.OK)
            {
                this.canvasSize = canvasSize.size;
                drawStatusBar();
            }
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool flag;
            if (fillToolStripMenuItem.Checked == true)
            {
                tmpSolidColor = solidColor;
                solidColor = Color.Empty;
                flag = false;
            }
            else
            {
                solidColor = tmpSolidColor;
                flag = true;
            }
            fillColorToolStripMenuItem.Enabled = flag;
            fillToolStripMenuItem.Checked = flag;

            fillColorToolStripButton.Enabled = flag;
            fillToolStripButton.Checked = flag;
        }

        public void chooseFigure()
        {
            // uncheck all
            lineToolStripMenuItem.Checked = false;
            curveToolStripMenuItem.Checked = false;
            rectangleToolStripMenuItem.Checked = false;
            ellipseToolStripMenuItem.Checked = false;
            textToolStripMenuItem.Checked = false;

            lineToolStripButton.Checked = false;
            curveToolStripButton.Checked = false;
            rectangleToolStripButton.Checked = false;
            ellipseToolStripButton.Checked = false;
            textStripButton.Checked = false;

            // check color fill enable
            if (fillToolStripMenuItem.Checked == true && 
                (figureType == FigureType.Rectangle || figureType == FigureType.Ellipse))
            {
                fillColorToolStripMenuItem.Enabled = true;
                fillColorToolStripButton.Enabled = true;
            } else
            {
                fillColorToolStripMenuItem.Enabled = false;
                fillColorToolStripButton.Enabled = false;
            }
            // set check figure
            if (figureType == FigureType.Line)
            {
                lineToolStripMenuItem.Checked = true;
                lineToolStripButton.Checked = true;
            } else if (figureType == FigureType.Curve) 
            {
                curveToolStripMenuItem.Checked = true;
                curveToolStripButton.Checked = true;
            }
            else if (figureType == FigureType.Rectangle)
            {
                rectangleToolStripMenuItem.Checked = true;
                rectangleToolStripButton.Checked = true;
            }
            else if (figureType == FigureType.Ellipse)
            {
                ellipseToolStripMenuItem.Checked = true;
                ellipseToolStripButton.Checked = true;
            }
            else if (figureType == FigureType.Text)
            {
                textToolStripMenuItem.Checked = true;
                textStripButton.Checked = true;
            }
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figureType = FigureType.Line;
            chooseFigure();
        }

        private void curveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figureType = FigureType.Curve;
            chooseFigure();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figureType = FigureType.Rectangle;
            chooseFigure();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figureType = FigureType.Ellipse;
            chooseFigure();
        }
        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            figureType = FigureType.Text;
            chooseFigure();
        }
        public void drawMouseCoordinates(Point m, bool delete = false)
        {
            if (!delete)
            {
                coordinatesStatusBar.Text = "X:" + Convert.ToString(m.X) + "Y:" + Convert.ToString(m.Y);
            }
            else
            {
                coordinatesStatusBar.Text = String.Empty;
            }
        }
        public void drawFontType(Font f, bool delete = false)
        {
            if (!delete)
            {
                fontTypeStatusBar.Text = "Шрифт:" + f.Name + "  Размер шрифта:" + f.Size;
            }
            else 
            {
                fontTypeStatusBar.Text = String.Empty;
            }
        }
        private void drawStatusBar()
        {
            lineWidthStatusBar.Text =  "Размер линии: " + Convert.ToString(lineSize);
            if (ActiveMdiChild != null)
            {
                CanvasForm canvasForm = (CanvasForm)ActiveMdiChild;
                canvasSizeStatusBar.Text = Convert.ToString(canvasForm.workPlaceSize.Width) + "x" + Convert.ToString(canvasForm.workPlaceSize.Height);
            }
            else
            {
                canvasSizeStatusBar.Text = System.String.Empty;
            }
            // lineColor
            Graphics g = statusBar1.CreateGraphics();
            Point s1 = new Point(lineWidthStatusBar.Width + 1, 3);
            Point s2 = new Point(s1.X + lineColorStatusBar.Width - 5, 17);
            System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.FromLTRB(s1.X, s1.Y, s2.X, s2.Y);
            g.FillRectangle(new SolidBrush(lineColor), rectangle);
            // fillColor
            s1 = new Point(s2.X + 5, 3);
            s2 = new Point(s1.X + fillColorStatusBar.Width - 5, 17);
            rectangle = System.Drawing.Rectangle.FromLTRB(s1.X, s1.Y, s2.X, s2.Y);
            g.FillRectangle(new SolidBrush(solidColor), rectangle);
        }
        private void statusBar1_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
        {
            drawStatusBar();
        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            drawStatusBar();
            UpdateEditButtons();
            updateBTNEditForm();
        }

        private void fontSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                CanvasForm canvas = (CanvasForm)ActiveMdiChild;
                if (canvas != null && canvas.getEditFigure() != null)
                {
                    Figure figure = canvas.getEditFigure();
                    figure.setFont(fontDialog.Font);
                }
                else
                {
                    canvasFont = fontDialog.Font;
                }
            }
        }

        private void fontDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            canvasFont = canvasDefaultFont;
        }

        private void selectToolStripButton_Click(object sender, EventArgs e)
        {
            if (selectToolStripButton.Checked)
            {
                selectMode = false;
                selectToolStripButton.Checked = false;
                ((CanvasForm)ActiveMdiChild).UnselectAll();
            }
            else
            {
                selectToolStripButton.Checked = true;
                selectMode = true;
            }
        }

        private void deleteStripButton_Click(object sender, EventArgs e)
        {
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            canvas.deleteSelected();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            DataObject dataObject = new DataObject();
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            formatter.Serialize(stream, canvas.GetSelectedFigures());
            dataObject.SetData("PAINT", stream);
            Clipboard.SetDataObject(dataObject, true);
            stream.Close();
            UpdateEditButtons();
        }

        private void copyMetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = CreateGraphics();
            IntPtr intPtr = graphics.GetHdc();
            Metafile metafile = new Metafile(intPtr, EmfType.EmfOnly);
            graphics.ReleaseHdc(intPtr);
            graphics.Dispose();
            graphics = Graphics.FromImage(metafile);

            // draw
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            canvas.GetSelectedFigures().ForEach(figure =>
            {
                figure.selected = false;
                figure.Draw(graphics, Point.Empty);
            });
            graphics.Dispose();

            ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, metafile);
            UpdateEditButtons();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            canvas.deleteSelected();
            UpdateEditButtons();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectMode = true;
            MemoryStream stream = (MemoryStream)Clipboard.GetDataObject().GetData("PAINT");
            BinaryFormatter formatter = new BinaryFormatter();
            List<Figure> buff = (List<Figure>)formatter.Deserialize(stream);
            stream.Close();
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            canvas.PasteSelectedFigures(buff);
            UpdateEditButtons();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectToolStripButton.Checked = true;
            selectMode = true;
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            canvas.SelectAll();
            UpdateEditButtons();
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            UpdateEditButtons();
        }

        public void UpdateEditButtons()
        {
            selectAllToolStripMenuItem.Enabled = false;
            copyMetaToolStripMenuItem.Enabled = false;
            copyToolStripMenuItem.Enabled = false;
            cutToolStripMenuItem.Enabled = false;
            pasteToolStripMenuItem.Enabled = false;
            
            selectAllStripButton.Enabled = false;
            copyMetaStripButton.Enabled = false;
            copyStripButton.Enabled = false;
            cutStripButton.Enabled = false;
            pasteStripButton.Enabled = false;
            
            if (ActiveMdiChild != null)
            {
                selectAllToolStripMenuItem.Enabled = true;
                selectAllStripButton.Enabled = true;

                if (((CanvasForm)ActiveMdiChild).GetSelectedFigures().Count() > 0)
                {
                    copyMetaToolStripMenuItem.Enabled = true;
                    copyToolStripMenuItem.Enabled = true;
                    cutToolStripMenuItem.Enabled = true;

                    copyMetaStripButton.Enabled = true;
                    copyStripButton.Enabled = true;
                    cutStripButton.Enabled = true;
                }

                IDataObject ido = Clipboard.GetDataObject();
                if (ido != null && ido.GetDataPresent("PAINT"))
                {
                    pasteToolStripMenuItem.Enabled = true;
                    pasteStripButton.Enabled = true;
                }
            }
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridToolStripMenuItem.Checked)
            {
                gridMode = false;
                gridToolStripMenuItem.Checked = false;
                GridToolStripButton.Checked = false;
            } else
            {
                gridMode = true;
                gridToolStripMenuItem.Checked = true;
                GridToolStripButton.Checked = true;
            }
        }

        private void gridPitchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridSizeForm gridSizeForm = new GridSizeForm();

            gridSizeForm.SetPitch(gridPitch);
            if (gridSizeForm.ShowDialog() == DialogResult.OK)
            {
                gridPitch = gridSizeForm.GetPitch();
            }
        }

        private void alignOnTheGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            if (canvas != null)
            {
                canvas.AlignToGrid(gridPitch);
            }
        }

        private void bindingToTheGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bindingToTheGridToolStripMenuItem.Checked)
            {
                bindingToTheGridToolStripMenuItem.Checked = false;
                GridBindToolStripButton.Checked = false;
                autoGridMode = false;
            } else
            {
                gridMode = true;
                gridToolStripMenuItem.Checked = true;
                GridBindToolStripButton.Checked = true;
                GridToolStripButton.Checked = true;
                bindingToTheGridToolStripMenuItem.Checked = true;
                autoGridMode = true;
            }
        }

        public void updateBTNEditForm()
        {
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            if (canvas != null)
            {
                editFormToolStripMenuItem.Enabled = true;
            }
            else
            {
                editFormToolStripMenuItem.Enabled = false;
            }
        }

        private void editFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasForm canvas = (CanvasForm)ActiveMdiChild;
            if (canvas != null)
            {
                EditForm form = new EditForm(canvas);
                form.ShowDialog();
            }
        }
    }
}
