using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace paint
{
    public enum FigureType
    {
        Line = 0,
        Curve = 1,
        Rectangle = 2,
        Ellipse = 3,
        Text = 4
    }

    public enum StatusCheck
    {
        NotChecked = 0,
        Good = 1,
        Bad = 2
    }

    [Serializable()]
    public abstract class Figure
    {
        public List<Point> points;
        public List<Point> prevPoints;
        public Color lineColor;
        protected int lineSize;
        public Color solidColor;
        public StatusCheck isCorrect;
        [NonSerialized]
        public bool selected = false;
        [NonSerialized]
        public Point mousePrev;
        [NonSerialized]
        public bool showBox = false;
        [NonSerialized]
        public int boxRad = 10;
        public int type;

        public Figure(Point pointOne, Point pointTwo, Point offset, int lineSize, Color lineColor)
        {
            points = new List<Point>();
            prevPoints = new List<Point>();
            points.Add(new Point(pointOne.X - offset.X, pointOne.Y - offset.Y));
            points.Add(new Point(pointTwo.X - offset.X, pointTwo.Y - offset.Y));

            this.lineSize = lineSize;
            this.lineColor = lineColor;

            isCorrect = StatusCheck.NotChecked;
        }
        public abstract void BaseDraw(Graphics g, Point offset, Pen pen, SolidBrush solidBrush);

        public abstract void Draw(Graphics g, Point offset);

        public abstract void DrawHash(Graphics g, Point offset);

        public abstract void Hide(Graphics g, Point offset);

        public void Normalization(ref Point pointOne, ref Point pointTwo)
        {
            int tmp;
            if ((pointOne.X <= pointTwo.X) && (pointOne.Y >= pointTwo.Y))
            {
                tmp = pointOne.Y;
                pointOne.Y = pointTwo.Y;
                pointTwo.Y = tmp;
            }
            else if ((pointOne.X >= pointTwo.X) && (pointOne.Y <= pointTwo.Y))
            {
                tmp = pointOne.X;
                pointOne.X = pointTwo.X;
                pointTwo.X = tmp;
            }
            else if ((pointOne.X >= pointTwo.X) && (pointOne.Y >= pointTwo.Y))
            {
                tmp = pointOne.Y;
                pointOne.Y = pointTwo.Y;
                pointTwo.Y = tmp;

                tmp = pointOne.X;
                pointOne.X = pointTwo.X;
                pointTwo.X = tmp;

            }
        }

        public virtual void MouseMove(Graphics g, Point mousePosition, Point offset)
        {
            points[1] = new Point(mousePosition.X - offset.X, mousePosition.Y - offset.Y);
            DrawHash(g, offset);
        }

        /* Return Rectangle of figure [leftTop, rightBottom] */
        public virtual List<Point> GetRectangle() => new List<Point> {
            new Point(points.Min(point => point.X), points.Min(point => point.Y)),
            new Point(points.Max(point => point.X), points.Max(point => point.Y))
        };

        /* Move self points to shift */
        public virtual void MovePoints(Point shift)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = new Point(points[i].X + shift.X, points[i].Y + shift.Y);
            }
        }


        public abstract void FinishDraw(Graphics g, Point offset);

        public abstract void Falidate();
        public virtual void Select(bool flag, Point mousePosition, Point offset)
        {
            if (flag)
            {
                prevPoints = new List<Point>();
                foreach (Point point in points)
                {
                    prevPoints.Add(point);
                }
            }
            else
            {
                prevPoints = new List<Point>();
            }
            mousePrev = new Point(mousePosition.X - offset.X, mousePosition.Y - offset.Y);
            selected = flag;
        }
        public virtual void MouseMoveSelected(Graphics g, Point mousePosition, Point offset)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Point point = points[i];
                int x = mousePrev.X - (mousePosition.X - offset.X);
                int y = mousePrev.Y - (mousePosition.Y - offset.Y);
                points[i] = new Point(point.X - x, point.Y - y);
            }
            //DrawHash(g, offset);
            //Console.WriteLine(mousePrev);
            mousePrev = new Point(mousePosition.X - offset.X, mousePosition.Y - offset.Y);
            //Console.WriteLine(mousePrev);
        }
        public virtual void reDrawUnSelected(Graphics g, Point offset)
        {
            selected = false;
            points = new List<Point>();
            for (int i = 0; i < prevPoints.Count; i++)
            {
                points.Add(prevPoints[i]);
            }
        }

        public virtual void Align(int gridPitch, Size size)
        {
            for (int i = 0; i < points.Count(); i++)
            {
                points[i] = new Point(DotRound(points[i].X, gridPitch, size.Width),
                                      DotRound(points[i].Y, gridPitch, size.Height));
            }
        }

        public static int DotRound(int value, int step, int max)
        {
            int leftBorder = value / step * step;
            int rightBorder = leftBorder + step;
            if (Math.Abs(leftBorder - value) < Math.Abs(rightBorder - value))
            {
                return leftBorder;
            }
            return rightBorder;
        }

        public bool IsPointInside(Point mouse)
        {
            List<Point> box = GetRectangle();
            //Console.WriteLine(mouse);
            //Console.WriteLine(box[0]);
            //Console.WriteLine(box[1]);
            if (mouse.X >= box[0].X && mouse.Y >= box[0].Y &&
                mouse.X <= box[1].X && mouse.Y <= box[1].Y)
            {
                return true;
            }
            return false;
        }
        public bool IsPointInside(Point mouse, Point offset)
        {
            Point _mouse = new Point(mouse.X - offset.X, mouse.Y - offset.Y);
            return IsPointInside(_mouse);
        }

        public List<Point> RectangleFromPoint(Point point, int radius) => new List<Point>
            {
                new Point(point.X-radius, point.Y-radius),
                new Point(point.X + radius, point.Y + radius)
            };

        public void DrawRectangle(Graphics g, Pen pen, List<Point> box, Point offset, SolidBrush solid = null)
        {
            Point normPointOne = new Point(box[0].X + offset.X, box[0].Y + offset.Y);
            Point normPointTwo = new Point(box[1].X + offset.X, box[1].Y + offset.Y);
            System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.FromLTRB(normPointOne.X, normPointOne.Y, normPointTwo.X, normPointTwo.Y);
            if (solid != null)
            {
                g.FillRectangle(solid, rectangle);
            }
            g.DrawRectangle(pen, rectangle);
        }

        public List<Point> GetBoxPoint()
        {
            List<Point> box = GetRectangle();
            return new List<Point> {
                // top
                box[0], new Point((box[0].X + box[1].X) / 2, box[0].Y), new Point(box[1].X, box[0].Y),
                // middle
                new Point(box[0].X, (box[0].Y + box[1].Y)/2), new Point(box[1].X, (box[0].Y + box[1].Y) / 2),
                // bottom
                new Point(box[0].X, box[1].Y), new Point((box[0].X + box[1].X) / 2, box[1].Y), box[1]
            };
        }

        public void DrawBox(Graphics g, Point offset)
        {
            List<Point> box = GetRectangle();
            Pen pen = new Pen(Color.Gray, 1);
            SolidBrush solid = new SolidBrush(Color.White);
            DrawRectangle(g, pen, box, offset);

            List<Point> boxPoints = GetBoxPoint();
            boxPoints.ForEach(p => DrawRectangle(g, pen, RectangleFromPoint(p, boxRad), offset, solid));

            solid.Dispose();
            pen.Dispose();
        }

        public bool IsClickInsideBoxes(Point mouse, Point offset)
        {
            Point _mouse = new Point(mouse.X - offset.X, mouse.Y - offset.Y);
            List<Point> boxPoints = GetBoxPoint();
            bool flag = false;
            boxPoints.ForEach(p =>
            {
                List<Point> _box = RectangleFromPoint(p, boxRad);
                System.Drawing.Rectangle rect = System.Drawing.Rectangle.FromLTRB(_box[0].X, _box[0].Y, _box[1].X, _box[1].Y);
                if (rect.Contains(_mouse))
                {
                    flag = true;
                };
            });
            if (!flag)
            {
                List<Point> box = GetRectangle();
                System.Drawing.Rectangle rect = System.Drawing.Rectangle.FromLTRB(box[0].X, box[0].Y, box[1].X, box[1].Y);
                if (rect.Contains(_mouse))
                {
                    flag = true;
                };
            }

            return flag;
        }

        public int GetMarkerId(Point mouse, Point offset)
        {
            Point _mouse = new Point(mouse.X - offset.X, mouse.Y - offset.Y);
            List<Point> boxPoints = GetBoxPoint();
            int mousePosition = -1;
            for (int i = 0; i < boxPoints.Count; i++)
            {
                List<Point> _box = RectangleFromPoint(boxPoints[i], boxRad);
                System.Drawing.Rectangle rect = System.Drawing.Rectangle.FromLTRB(_box[0].X, _box[0].Y, _box[1].X, _box[1].Y);
                if (rect.Contains(_mouse))
                {
                    mousePosition = i;
                    break;
                };
            }
            if (mousePosition == -1)
            {
                List<Point> box = GetRectangle();
                System.Drawing.Rectangle rect = System.Drawing.Rectangle.FromLTRB(box[0].X, box[0].Y, box[1].X, box[1].Y);
                if (rect.Contains(_mouse))
                {
                    mousePosition = 8;
                };
            }

            return mousePosition;
        }

        public virtual void UpdatePoints(List<Point> p, Point _mouse, String x, String y)
        {
            List<Point> _points = new List<Point>();
            _points.AddRange(p);
            int index_X = -1, index_Y = -1;
            if (x == "max")
            {
                index_X = _points[0].X > _points[1].X ? 0 : 1;
            } else if (x == "min")
            {
                index_X = _points[0].X < _points[1].X ? 0 : 1;
            }
            if (y == "max")
            {
                index_Y = _points[0].Y > _points[1].Y ? 0 : 1;
            }
            else if (y == "min")
            {
                index_Y = _points[0].Y < _points[1].Y ? 0 : 1;
            }

            if (index_X != -1 && (
                x == "max" && _mouse.X > _points.Min(pt => pt.X) + boxRad ||
                x == "min" && _mouse.X < _points.Max(pt => pt.X) - boxRad
                ))
            {
                _points[index_X] = new Point(_mouse.X, _points[index_X].Y);
            }

            if (index_Y != -1 && (
                y == "max" && _mouse.Y > _points.Min(pt => pt.Y) + boxRad ||
                y == "min" && _mouse.Y < _points.Max(pt => pt.Y) - boxRad
                ))
            {
                _points[index_Y] = new Point(_points[index_Y].X, _mouse.Y);
            }
            points = _points;
        }

        public void Resize(Point mouse, Point offset, int markerId)
        {
            Point _mouse = new Point(mouse.X - offset.X, mouse.Y - offset.Y);
            switch (markerId)
            {
                // top
                case 0:
                    UpdatePoints(points, _mouse, "min", "min");
                    break;
                case 1:
                    UpdatePoints(points, _mouse, "", "min");
                    break;
                case 2:
                    UpdatePoints(points, _mouse, "max", "min");
                    break;
                // middle
                case 3:
                    UpdatePoints(points, _mouse, "min", "");
                    break;
                case 4:
                    UpdatePoints(points, _mouse, "max", "");
                    break;
                // bottom
                case 5:
                    UpdatePoints(points, _mouse, "min", "max");
                    break;
                case 6:
                    UpdatePoints(points, _mouse, "", "max");
                    break;
                case 7:
                    UpdatePoints(points, _mouse, "max", "max");
                    break;
            }
        }

        public static Point GetMousePoint(Point mouse, Point offset) =>
            new Point(mouse.X - offset.X, mouse.Y - offset.Y);
        public bool IsFigureInCanvas(Size size)
        {
            bool flag = true;
            List<Point> box = GetRectangle();
            System.Drawing.Rectangle rect = System.Drawing.Rectangle.FromLTRB(0, 0, size.Width, size.Height);
            foreach (Point point in box)
            {
                //Console.WriteLine(point);
                if (!rect.Contains(point))
                {
                    flag = false;
                }
            }
            //Console.WriteLine(flag);
            return flag;
        }
        public virtual bool isText() => false;

        public virtual FigureType getType() => FigureType.Line;

        public virtual void setLineColor(Color color) 
        {
            this.lineColor = color;
        }
        
        public virtual void setSolidColor(Color color) { }
        
        public virtual void setLineSize(int size) 
        {
            lineSize = size;
        }

        public virtual void setFont(Font font) { }


        public String getName()
        {
            FigureType type = getType();
            switch (type)
            {
                case FigureType.Line:
                    return "Линия";
                case FigureType.Curve:
                    return "Кривая линия";
                case FigureType.Rectangle:
                    return "Прямоугольник";
                case FigureType.Ellipse:
                    return "Эллипс";
                case FigureType.Text:
                    return "Текст";
            }
            return "";
        }

        public String getCoors()
        {
            return points[0].X.ToString() + ", " + points[0].Y.ToString();
        }

        abstract public List<Control> getControls();
        abstract public void setParameters(Control.ControlCollection controls);

        public FlowLayoutPanel getPanel(int sh = 25) => new FlowLayoutPanel()
        {
            Size = new Size(250, sh)
        };

        public Label getLabel(String text = "", int sw = 150) => new Label()
        {
            Text = text,
            Size = new Size(sw, 25),
            TextAlign = ContentAlignment.MiddleLeft
        };

        public Control getControlLineColor()
        {
            var panel = getPanel();
            var label = getLabel("Цвет линии");
            var textBox = new TextBox()
            {
                BackColor = lineColor,
                Size = new Size(80, 25),
                Tag = lineColor,
                ReadOnly = true
            };
            textBox.MouseClick += (object sender, MouseEventArgs e) =>
            {
                var colorDialog = new ColorDialog()
                {
                    Color = (Color)textBox.Tag
                };
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.BackColor = colorDialog.Color;
                    textBox.Tag = colorDialog.Color;
                }
            };
            panel.Controls.AddRange(new Control[] { label, textBox });
            return panel;
        }

        public void updateLineColor(Control.ControlCollection controls)
        {
            Color lineColor = (Color)controls[1].Tag;
            this.lineColor = lineColor;
        }

        public virtual Control getLineSizeControl()
        {
            var panel = getPanel();
            var label = getLabel("Размер линии");
            var textBox = new TextBox()
            {
                Text = lineSize.ToString(),
                Size = new Size(80, 25),
                Tag = lineSize,
                ReadOnly = true,
                BackColor = SystemColors.Window
            };
            textBox.MouseClick += (object sender, MouseEventArgs e) =>
            {
                LineSizeForm lineSize = new LineSizeForm();

                lineSize.SetSize(this.lineSize);
                if (lineSize.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = lineSize.GetSize().ToString();
                    textBox.Tag = lineSize.GetSize();
                }
            };
            panel.Controls.AddRange(new Control[] { label, textBox });
            return panel;
        }

        public void updateLineSize(Control.ControlCollection controls)
        {
            lineSize = (int)controls[1].Tag;
        }

        public virtual Control getSolidColorControl()
        {
            var panel = getPanel();
            var label = getLabel("Цвет заливки");
            var textBox = new TextBox()
            {
                BackColor = solidColor,
                Size = new Size(80, 25),
                Tag = solidColor,
                ReadOnly = true
            };
            textBox.MouseClick += (object sender, MouseEventArgs e) =>
            {
                var colorDialog = new ColorDialog()
                {
                    Color = (Color)textBox.Tag
                };
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.BackColor = colorDialog.Color;
                    textBox.Tag = colorDialog.Color;
                }
            };
            panel.Controls.AddRange(new Control[] { label, textBox });
            return panel;
        }

        public virtual void updateSolidColor(Control.ControlCollection controls)
        {
            solidColor = (Color)controls[1].Tag;
        }

        public virtual Control getFillControl()
        {
            var panel = getPanel();
            var label = getLabel("Режим заливки");
            var checkBox = new CheckBox()
            {
                Size = new Size(80, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Checked = solidColor != Color.Empty
            };
            panel.Controls.AddRange(new Control[] { label, checkBox });
            return panel;
        }

        public virtual void updateFill(Control.ControlCollection controls)
        {
            var checkbox = (CheckBox)controls[1];
            solidColor = checkbox.Checked ? solidColor : Color.Empty;
        }

        public virtual Control getPointsControl()
        {
            var panel = getPanel(100);
            var label = getLabel("Точки", 70);
            String text = "";
            points.ForEach(point =>
            {
                String n = point.X.ToString() + "," + point.Y.ToString() + "\r\n";
                text += n;
            });
            var textBox = new TextBox()
            {
                BackColor = Color.White,
                Text = text,
                Font = new Font("Times New Roman", 12),
                Size = new Size(160, 100),
                Multiline = true
            };
            panel.Controls.AddRange(new Control[] { label, textBox });
            return panel;
        }

        public void updatePoints(Control.ControlCollection controls)
        {
            String text = (String)controls[1].Text;
            String[] pointsString = Regex.Split(text, "\r\n");
            int last = pointsString.Length;
            if (getType() != FigureType.Curve)
            {
                last = 2;
            }
            for (int i = 0; i < last; i++)
            {
                var pointString = pointsString[i];
                var pointCoordinates = pointString.Split(',');
                if (!int.TryParse(pointCoordinates[0], out int x))
                {
                    return;
                }
                if (!int.TryParse(pointCoordinates[1], out int y))
                {
                    return;
                }
                var point = new Point(x, y);
                if (points.Count -1 < i)
                {
                    points.Add(point);
                }
                else
                {
                    points[i] = point;
                }
            }
        }
    }
}
