using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint
{
    [Serializable()]
    public partial class CanvasForm : Form
    {
        private List<Figure> array;
        public BufferedGraphics buffer;
        public BufferedGraphicsContext contex;
        [NonSerialized]
        bool isMousePresed = false;
        [NonSerialized]
        bool isMouseMoved = false;
        [NonSerialized]
        bool isFiguresSelected = false;
        [NonSerialized]
        bool isMouseSelectedMoved = false;
        [NonSerialized]
        bool isFiguresSelectedMouseDown = false;
        [NonSerialized]
        bool MouseDownOutSide = false;
        [NonSerialized]
        public bool isModificated = false;
        [NonSerialized]
        bool isResizeMode = false;
        [NonSerialized]
        int markerId = -1;
        [NonSerialized]
        bool isUpdateText = false;
        public string FilePathSave = System.String.Empty;
        public Size workPlaceSize;
        public CanvasForm(Size size)
        {
            InitializeComponent();
            array = new List<Figure>();
            workPlaceSize = size;
            Size = size;
            AutoScrollMinSize = size;
        }

        private bool MouseNotInCanvas(MouseEventArgs e)
        {
            return (e.X > workPlaceSize.Width) || (e.Y > workPlaceSize.Height);
        }

        private void CanvasForm_MouseDown(object sender, MouseEventArgs e)
        {
            // not in canvas
            if (MouseNotInCanvas(e)) { return; }

            isMousePresed = true;
            MainForm m = (MainForm)ParentForm;
            //resized mode
            Figure figure = array.Find(fig => fig.showBox == true);
            if (m.selectMode && figure != null)
            {
                isResizeMode = true;
                markerId = -1;
                if (figure.IsClickInsideBoxes(e.Location, AutoScrollPosition))
                {
                    markerId = figure.GetMarkerId(e.Location, AutoScrollPosition);
                    figure.prevPoints = new List<Point> { };
                    figure.prevPoints.AddRange(figure.points);
                    figure.mousePrev = Figure.GetMousePoint(e.Location, AutoScrollPosition);
                    if (markerId == 8 && figure.getType() == FigureType.Text)
                    {
                        isUpdateText = true;
                    }
                }
            }
            // selected figures
            else if (m.selectMode && isFiguresSelected)
            {
                MouseDownOutSide = true;
                {
                    foreach (Figure x in array)
                    {
                        if (x.selected)
                        {
                            x.Select(true, e.Location, AutoScrollPosition);
                            Point p = new Point(e.Location.X - AutoScrollPosition.X, e.Location.Y - AutoScrollPosition.Y);
                            if (IsInsideSelectedDown(x, p))
                            {
                                isFiguresSelectedMouseDown = true;
                                MouseDownOutSide = false;
                            }
                        }
                    }
                }
                if (MouseDownOutSide)
                {
                    foreach (Figure x in array)
                    {
                        if (x.selected)
                        {
                            x.Select(false, e.Location, AutoScrollPosition);
                        }
                    }
                    array.Add(new Rectangle(e.Location, e.Location, AutoScrollPosition, 2, Color.Blue, Color.Empty, true));
                    isMouseMoved = false;
                    isFiguresSelected = false;
                    isMouseSelectedMoved = false;
                    isFiguresSelectedMouseDown = false;
                    MouseDownOutSide = false;
                }
            }

            // Add select rectangle
            else if (m.selectMode)
            {
                array.Add(new Rectangle(e.Location, e.Location, AutoScrollPosition, 2, Color.Blue, Color.Empty, true));
            }
            // Add figues
            else
            {
                Point mouse = e.Location;
                if (m.autoGridMode)
                {
                    mouse = new Point(
                        Figure.DotRound(e.Location.X, m.gridPitch, Size.Width),
                        Figure.DotRound(e.Location.Y, m.gridPitch, Size.Height)
                    );
                }
                switch (m.figureType)
                {
                    case FigureType.Line:
                        {
                            array.Add(new Line(mouse, mouse, AutoScrollPosition, m.lineSize, m.lineColor));
                            break;
                        }

                    case FigureType.Curve:
                        {
                            array.Add(new Curve(mouse, mouse, AutoScrollPosition, m.lineSize, m.lineColor));
                            break;
                        }

                    case FigureType.Rectangle:
                        {
                            array.Add(new Rectangle(mouse, mouse, AutoScrollPosition, m.lineSize, m.lineColor, m.solidColor));
                            break;
                        }

                    case FigureType.Ellipse:
                        {
                            array.Add(new Ellipse(mouse, mouse, AutoScrollPosition, m.lineSize, m.lineColor, m.solidColor));
                            break;
                        }
                    case FigureType.Text:
                        {
                            array.Add(new Text(mouse, mouse, AutoScrollPosition, m.lineSize, m.lineColor, m.canvasFont, this));
                            break;
                        }
                }
            }
        }

        private void CanvasForm_MouseMove(object sender, MouseEventArgs e)
        {
            MainForm m = (MainForm)ParentForm;
            m.drawMouseCoordinates(e.Location);
            if (m.selectMode && isResizeMode)
            {
                isUpdateText = false;
                if (markerId != -1)
                {
                    Figure figure = array.Find(fig => fig.showBox == true);
                    if (figure != null)
                    {
                        if (markerId == 8)
                        {
                            Point mouse = Figure.GetMousePoint(e.Location, AutoScrollPosition);
                            Point shift = new Point(
                                -figure.mousePrev.X + mouse.X,
                                -figure.mousePrev.Y + mouse.Y);
                            figure.MovePoints(shift);
                            figure.mousePrev = mouse;
                        }
                        else
                        {
                            figure.Resize(e.Location, AutoScrollPosition, markerId);
                        }
                    }
                }
            }
            // move selected figures
            else if (m.selectMode && isFiguresSelected && isFiguresSelectedMouseDown)
            {
                foreach (Figure x in array)
                {
                    if (x.selected)
                    {
                        x.MouseMoveSelected(buffer.Graphics, e.Location, AutoScrollPosition);
                        isMouseSelectedMoved = true;
                    }
                }
            }
            else if (MouseDownOutSide)
            {

            }
            // redraw select rectangle
            else if (isMousePresed)
            {
                Point mouse = e.Location;
                if (m.autoGridMode && m.figureType != FigureType.Curve && !m.selectMode)
                {
                    mouse = new Point(
                        Figure.DotRound(e.Location.X, m.gridPitch, Size.Width),
                        Figure.DotRound(e.Location.Y, m.gridPitch, Size.Height)
                    );
                }
                array.Last().MouseMove(buffer.Graphics, mouse, AutoScrollPosition);
                isMouseMoved = true;
            }
            Invalidate();
        }

        private void CanvasForm_MouseUp(object sender, MouseEventArgs e)
        {
            MainForm m = (MainForm)ParentForm;
            if (m.selectMode && isResizeMode)
            {
                Figure figure = array.Find(fig => fig.showBox == true);
                if (figure != null)
                {
                    if (isUpdateText && figure.getType() == FigureType.Text && markerId == 8)
                    {
                        Form form = new Form();
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.StartPosition = FormStartPosition.Manual;
                        List<Point> rect = figure.GetRectangle();
                        Point pts = PointToScreen(new Point(rect[0].X, rect[0].Y));
                        form.SetDesktopLocation(pts.X, pts.Y);
                        form.BackColor = Color.Magenta;
                        form.TransparencyKey = Color.Magenta;
                        TextBox tmpTextBox = new TextBox();
                        tmpTextBox.Parent = form;
                        tmpTextBox.Width = rect[1].X - rect[0].X;
                        tmpTextBox.Height = rect[1].Y - rect[0].Y;
                        tmpTextBox.ForeColor = figure.lineColor;
                        tmpTextBox.Font = ((Text)figure).font;
                        tmpTextBox.Multiline = true;
                        tmpTextBox.Text = ((Text)figure).text;
                        tmpTextBox.KeyPress += UpdateTextBox_KeyPress;
                        form.Size = new Size(tmpTextBox.Size.Width, tmpTextBox.Size.Height);
                        tmpTextBox.Visible = true;
                        form.ShowDialog();
                    }
                    if (m.gridMode && m.autoGridMode && markerId != -1)
                    {
                        figure.Align(m.gridPitch, Size);
                    }
                    if (markerId == -1)
                    {
                        array.ForEach(f => f.showBox = false);
                    }
                    else
                    {
                        if (!figure.IsFigureInCanvas(workPlaceSize))
                        {
                            figure.points = figure.prevPoints;
                        }
                    }
                }
                isResizeMode = false;
                markerId = -1;
                Invalidate();
            }
            // first select figures
            else if (m.selectMode && !isMouseSelectedMoved && !isFiguresSelectedMouseDown && !MouseDownOutSide)
            {
                Point startSelect = Array.Last().points[0];
                Point endSelect = Array.Last().points.Last();
                array.RemoveAt(array.Count - 1);
                isFiguresSelected = false;
                for (int i = 0; i < array.Count; i++)
                {
                    Point startFigure = Array[i].points[0];
                    Point endFigure = Array[i].points.Last();
                    array[i].Select(false, e.Location, AutoScrollPosition);
                    if (IsInsideSelected(array[i], startFigure, endFigure, startSelect, endSelect))
                    {
                        array[i].Select(true, e.Location, AutoScrollPosition);
                        isFiguresSelected = true;
                    }
                }
            }
            // moved selected figures
            else if (m.selectMode && isMouseSelectedMoved)
            {
                bool flagReDraw = false;
                foreach (Figure figure in array)
                {
                    if (figure.selected)
                    {
                        if (!IsFigureInCanvas(figure, e.Location))
                        {
                            flagReDraw = true;
                        }
                    }
                }


                foreach (Figure figure in array)
                {
                    if (figure.selected)
                    {
                        if (flagReDraw)
                        {
                            figure.reDrawUnSelected(buffer.Graphics, AutoScrollPosition);
                            //figure.FinishDraw(buffer.Graphics, AutoScrollPosition);
                        }
                    }
                    figure.Select(false, e.Location, AutoScrollPosition);
                }

                isMouseSelectedMoved = false;
                isFiguresSelected = false;
            }
            else if (m.selectMode)
            {
                //Console.WriteLine(132);
                foreach (Figure figure in array)
                {
                    figure.Select(false, e.Location, AutoScrollPosition);
                }
                isMouseSelectedMoved = false;
                isFiguresSelected = false;
            }
            // draw just figure
            else
            {
                if (isMousePresed && !isMouseMoved)
                {
                    array.RemoveAt(array.Count - 1);
                }
                else if (isMousePresed && isMouseMoved)
                {
                    if (!IsFigureInCanvas(array.Last(), e.Location))
                    {
                        array.RemoveAt(array.Count - 1);
                    }
                    else
                    {
                        if (m.autoGridMode && m.figureType == FigureType.Curve)
                        {
                            array.Last().Align(m.gridPitch, Size);
                        }
                        array.Last().FinishDraw(buffer.Graphics, AutoScrollPosition);
                        isModificated = true;
                    }
                }
            }
            Invalidate();
            isMousePresed = false;
            isMouseMoved = false;
            isFiguresSelectedMouseDown = false;
            MouseDownOutSide = false;
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Point startPoint = new System.Drawing.Point(0, 0);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(startPoint, this.workPlaceSize);
            System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);

            buffer.Graphics.FillRectangle(solidBrush, rectangle);

            MainForm m = (MainForm)this.ParentForm;
            if (m.gridMode)
            {
                //Console.WriteLine(1);
                Pen p = new Pen(Color.LightGray, 1);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                for (int i = 0; i < Size.Width; i += m.gridPitch)
                    buffer.Graphics.DrawLine(p, i, 0, i, Size.Height);

                for (int i = 0; i < Size.Height; i += m.gridPitch)
                    buffer.Graphics.DrawLine(p, 0, i, Size.Width, i);
            }


            for (int i = 0; i < array.Count(); ++i)
            {
                if (array[i].isCorrect == StatusCheck.Bad)
                {
                    array.RemoveAt(i);
                }
            }
            foreach (Figure i in array)
            {
                i.Draw(buffer.Graphics, AutoScrollPosition);
            }
            buffer.Render(e.Graphics);
        }

        private void CanvasForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm m = (MainForm)this.ParentForm;
            m.DisableOrEnableSave(1);
            m.drawMouseCoordinates(Point.Empty, true);
            m.updateBTNEditForm();
            buffer.Dispose();
            Dispose();
        }

        private void CanvasForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isModificated)
            {
                DialogResult dialogResult = MessageBox.Show("Сохранить последние изменения?", Text, MessageBoxButtons.YesNoCancel);

                if (dialogResult == DialogResult.Yes)
                {
                    MainForm mainWindow = (MainForm)this.MdiParent;

                    mainWindow.saveToolStripMenuItem_Click(sender, e);
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        internal List<Figure> Array { get => array; set => array = value; }

        private void CanvasForm_Load(object sender, EventArgs e)
        {
            // для уменьшения мерцания
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            contex = BufferedGraphicsManager.Current;
            contex.MaximumBuffer = new Size(workPlaceSize.Width, workPlaceSize.Height);

            buffer = contex.Allocate(CreateGraphics(), new System.Drawing.Rectangle(0, 0, workPlaceSize.Width, workPlaceSize.Height));

            System.Drawing.Point startPoint = new System.Drawing.Point(0, 0);
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(startPoint, workPlaceSize);
            System.Drawing.SolidBrush solidBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);

            buffer.Graphics.FillRectangle(solidBrush, rectangle);
            MainForm m = (MainForm)this.ParentForm;

        }
        private bool IsFigureInCanvas(Figure f, Point p)
        {
            foreach (Point i in f.points)
            {
                if (!IsPointInWorkplace(i))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPointInWorkplace(Point point)
        {
            return ((point.X <= workPlaceSize.Width) && (point.Y <= workPlaceSize.Height) &&
                   (point.X >= 0) && (point.Y >= 0));
        }
        private bool IsPointInsideTwo(Point p1, Point p2, Point p3)
        {
            return (p2.X < p1.X && p2.Y < p1.Y &&
                    p3.X > p1.X && p3.Y > p1.Y ||
                    p2.X < p1.X && p2.Y > p1.Y &&
                    p3.X > p1.X && p3.Y < p1.Y);
        }
        private bool IsInsedeBoth(Point startFigure, Point endFigure, Point startSelect, Point endSelect)
        {
            return (IsPointInsideTwo(startSelect, startFigure, endFigure) ||
                IsPointInsideTwo(endSelect, startFigure, endFigure) ||
                IsPointInsideTwo(startSelect, endFigure, startFigure) ||
                IsPointInsideTwo(endSelect, endFigure, startFigure) ||

                IsPointInsideTwo(startFigure, startSelect, endSelect) ||
                IsPointInsideTwo(endFigure, startSelect, endSelect) ||
                IsPointInsideTwo(startFigure, endSelect, startSelect) ||
                IsPointInsideTwo(endFigure, endSelect, startSelect));
        }
        private bool IsInsideSelected(Figure f, Point startFigure, Point endFigure, Point startSelect, Point endSelect)
        {
            bool flag = false;
            if (f is Curve)
            {
                foreach (Point point in f.points)
                {
                    if (IsPointInsideTwo(point, startSelect, endSelect) ||
                        IsPointInsideTwo(point, endSelect, startSelect))
                    {
                        flag = true;
                    }
                }

                int y_max = f.points[0].Y, y_min = f.points[0].Y, x_min = f.points[0].X, x_max = f.points[0].X;
                foreach (Point point in f.points)
                {
                    if (point.Y > y_max) { y_max = point.Y; }
                    if (point.Y < y_min) { y_min = point.Y; }
                    if (point.X < x_min) { x_min = point.X; }
                    if (point.X > x_max) { x_max = point.X; }
                }
                startFigure = new Point(x_min, y_min);
                endFigure = new Point(x_max, y_max);
                if (
                    IsPointInsideTwo(startSelect, startFigure, endFigure) ||
                    IsPointInsideTwo(endSelect, startFigure, endFigure))
                {
                    flag = true;
                }

            }
            else
            {
                if (IsInsedeBoth(startFigure, endFigure, startSelect, endSelect))
                {
                    flag = true;
                }
            }


            return flag;
        }

        private bool IsInsideSelectedDown(Figure f, Point MousePosition)
        {
            bool flag = false;
            if (f is Curve)
            {
                int y_max = f.points[0].Y, y_min = f.points[0].Y, x_min = f.points[0].X, x_max = f.points[0].X;
                foreach (Point point in f.points)
                {
                    if (point.Y > y_max) { y_max = point.Y; }
                    if (point.Y < y_min) { y_min = point.Y; }
                    if (point.X < x_min) { x_min = point.X; }
                    if (point.X > x_max) { x_max = point.X; }
                }
                Point startFigure = new Point(x_min, y_min);
                Point endFigure = new Point(x_max, y_max);
                if (
                    IsPointInsideTwo(MousePosition, startFigure, endFigure) ||
                    IsPointInsideTwo(MousePosition, startFigure, endFigure))
                {
                    flag = true;
                }

            }
            else
            {
                Point startFigure = f.points[0];
                Point endFigure = f.points[f.points.Count - 1];
                if (
                    IsPointInsideTwo(MousePosition, startFigure, endFigure) ||
                    IsPointInsideTwo(MousePosition, startFigure, endFigure) ||
                    IsPointInsideTwo(MousePosition, endFigure, startFigure) ||
                    IsPointInsideTwo(MousePosition, endFigure, startFigure))
                {
                    flag = true;
                }

            }


            return flag;
        }
        public void deleteSelected()
        {
            int c = 0;
            foreach (Figure x in array)
            {
                if (x.selected)
                {
                    c++;
                }
            }

            for (; c != 0; c--)
            {
                for (int j = 0; j < array.Count; j++)
                {
                    if (array[j].selected)
                    {
                        array.RemoveAt(j);
                        break;
                    }
                }
            }

            isMouseMoved = false;
            isFiguresSelectedMouseDown = false;
            isFiguresSelected = false;
            isMouseSelectedMoved = false;
            isFiguresSelectedMouseDown = false;
            MouseDownOutSide = false;
            isModificated = true;
        }
        private void CanvasForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteSelected();
            }
        }

        public void SelectAll()
        {
            MainForm m = (MainForm)this.ParentForm;
            if (m.selectMode && array.Count > 0)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    array[i].Select(true, new Point(0, 0), AutoScrollPosition);
                }

                isFiguresSelected = true;
                isMousePresed = false;
                isMouseMoved = false;
                isFiguresSelectedMouseDown = false;
                MouseDownOutSide = false;

            }

        }

        public List<Figure> GetSelectedFigures()
        {
            List<Figure> buff = new List<Figure>();
            foreach (Figure f in array)
            {
                if (f.selected)
                {
                    buff.Add(f);
                }
            }
            //Console.WriteLine(buff.Count());
            return buff;
        }

        public void PasteSelectedFigures(List<Figure> buff)
        {
            Point leftTop = new Point(buff.Min(f => f.GetRectangle()[0].X), buff.Min(f => f.GetRectangle()[0].Y));
            Point rightBottom = new Point(buff.Max(f => f.GetRectangle()[1].X), buff.Max(f => f.GetRectangle()[1].Y));

            if (rightBottom.X > workPlaceSize.Width || rightBottom.Y > workPlaceSize.Height)
            {
                MessageBox.Show("Блок вставки превышает допустимые размеры!");
                return;
            }

            Point shift = new Point(-leftTop.X, -leftTop.Y);
            //Console.WriteLine(shift);
            buff.ForEach(figure =>
            {
                figure.selected = true;
                figure.MovePoints(shift);
                array.Add(figure);
            });
            isFiguresSelected = true;
            isMousePresed = false;
            isMouseMoved = false;
            isFiguresSelectedMouseDown = false;
            MouseDownOutSide = false;
            Invalidate();
        }

        public void UnselectAll()
        {
            array.ForEach(f => { f.selected = false; });
            isFiguresSelected = false;
            isMousePresed = false;
            isMouseMoved = false;
            isFiguresSelectedMouseDown = false;
            MouseDownOutSide = false;
            Invalidate();
        }

        public void AlignToGrid(int gridPitch)
        {
            MainForm m = (MainForm)this.ParentForm;
            if (m.gridMode && array.Count > 0) {
                array.ForEach(f => f.Align(gridPitch, Size));
                Invalidate();
            }
        }

        private void CanvasForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            MainForm m = (MainForm)this.ParentForm;
            if (m.selectMode && array.Count > 0)
            {
                array.ForEach(f => f.showBox = false);
                for (int i = 0; i < array.Count; i++)
                {
                    if (array[i].IsPointInside(e.Location, AutoScrollPosition))
                    {
                        array[i].showBox = true;
                        break;
                    }
                }
                Figure figure = getEditFigure();
                m.figureType = figure.getType();
                m.chooseFigure();
            }

            Invalidate();
        }

        private void UpdateTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Figure figure = array.Find(f => f.showBox);
                ((Text)figure).text = ((TextBox)sender).Text;
                ((Form)((TextBox)sender).Parent).Close();
            }
        }

        public Figure getEditFigure() => array.Find(f => f.showBox);
        public List<Figure> getArray() => array;
        public void setArray(List<Figure> arrayNew)
        {
            array.Clear();
            array.AddRange(arrayNew);
        }
        public void UnselectedBox()
        {
            array.ForEach(f => f.showBox = false);
        }

        public void createFigure(FigureType figureType, Point mouse, Point offset)
        {
            MainForm m = (MainForm)this.ParentForm;
            switch (figureType)
            {
                case FigureType.Line:
                    {
                        array.Add(new Line(mouse, mouse, offset, m.lineSize, m.lineColor));
                        break;
                    }

                case FigureType.Curve:
                    {
                        array.Add(new Curve(mouse, mouse, offset, m.lineSize, m.lineColor));
                        break;
                    }

                case FigureType.Rectangle:
                    {
                        array.Add(new Rectangle(mouse, mouse, offset, m.lineSize, m.lineColor, m.solidColor));
                        break;
                    }

                case FigureType.Ellipse:
                    {
                        array.Add(new Ellipse(mouse, mouse, offset, m.lineSize, m.lineColor, m.solidColor));
                        break;
                    }
                case FigureType.Text:
                    {
                        array.Add(new Text(mouse, mouse, offset, m.lineSize, m.lineColor, m.canvasFont, this));
                        break;
                    }
            }
        }


     }
}
