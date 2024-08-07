using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace paint
{
    [Serializable()]
    class Rectangle : Figure
    {
        public bool blue_selected = false;
        public Rectangle(Point pointOne, Point pointTwo, Point offset, int lineSize, Color lineColor, Color fillColor, bool selected = false) :
            base(pointOne, pointTwo, offset, lineSize, lineColor)
        {
            solidColor = fillColor;
            if (selected)
            {
                blue_selected = true;
            }
        }
        public override void BaseDraw(Graphics g, Point offset, Pen pen, SolidBrush solidBrush)
        {
            Point normPointOne = new Point(points[0].X + offset.X, points[0].Y + offset.Y);
            Point normPointTwo = new Point(points[1].X + offset.X, points[1].Y + offset.Y);
            Normalization(ref normPointOne, ref normPointTwo);            
            System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.FromLTRB(normPointOne.X, normPointOne.Y, normPointTwo.X, normPointTwo.Y);

            if (solidBrush != null)
            {
                g.FillRectangle(solidBrush, rectangle);
            }

            g.DrawRectangle(pen, rectangle);
        }

        public override void Draw(Graphics g, Point offset)
        {
            Pen pen;
            if (!selected && !blue_selected)
            {
                pen = new Pen(lineColor, lineSize);
            } else
            {
                pen = new Pen(lineColor, lineSize)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                };
            }
            
            SolidBrush solidBrush = new SolidBrush(solidColor);
            BaseDraw(g, offset, pen, solidBrush);
            pen.Dispose();
            solidBrush.Dispose();
            if (showBox)
            {
                DrawBox(g, offset);
            }
        }

        public override void DrawHash(Graphics g, Point offset)
        {
            Pen pen = new Pen(lineColor, lineSize)
            {
                DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
            };
            BaseDraw(g, offset, pen, null);
            pen.Dispose();
        }

        public override void Hide(Graphics g, Point offset)
        {
            Pen pen = new Pen(Color.White, lineSize);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            BaseDraw(g, offset, pen, solidBrush);
            pen.Dispose();
            solidBrush.Dispose();
        }

        public override void FinishDraw(Graphics g, Point offset)
        {
            Falidate();
        }
        public override void Falidate()
        {
            if (points[0] == points[1])
            {
                isCorrect = StatusCheck.Bad;
            }
            else
            {
                isCorrect = StatusCheck.Good;
            }
        }

        public override FigureType getType() => FigureType.Rectangle;

        public override void setSolidColor(Color color)
        {
            solidColor = color;
        }

        public override List<Control> getControls()
        {
            List<Control> control = new List<Control>
            {
                getControlLineColor(),
                getLineSizeControl(),
                getSolidColorControl(),
                getFillControl(),
                getPointsControl()
            };
            return control;
        }
        public override void setParameters(Control.ControlCollection controls)
        {
            updateLineColor(((FlowLayoutPanel)controls[0]).Controls);
            updateLineSize(((FlowLayoutPanel)controls[1]).Controls);
            updateSolidColor(((FlowLayoutPanel)controls[2]).Controls);
            updateFill(((FlowLayoutPanel)controls[3]).Controls);
            updatePoints(((FlowLayoutPanel)controls[4]).Controls);
        }
    }
}
