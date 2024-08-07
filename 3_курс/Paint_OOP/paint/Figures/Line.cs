using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace paint
{
    [Serializable()]
    class Line : Figure
    {
        public Line(Point pointOne, Point pointTwo, Point offset, int lineSize, Color lineColor) :
            base(pointOne, pointTwo, offset, lineSize, lineColor)
        { }

        public override void BaseDraw(Graphics g, Point offset, Pen pen, SolidBrush solidBrush=null)
        {
            Point pointOneOffset = new Point(points[0].X + offset.X, points[0].Y + offset.Y);
            Point pointTwoOffset = new Point(points[1].X + offset.X, points[1].Y + offset.Y);

            g.DrawLine(pen, pointOneOffset, pointTwoOffset);
        }

        public override void Draw(Graphics g, Point offset)
        {
            Pen pen;
            if (!selected)
            {
                pen = new Pen(lineColor, lineSize);
            }
            else
            {
                pen = new Pen(lineColor, lineSize)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                };
            }

            BaseDraw(g, offset, pen);
            pen.Dispose();
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
            BaseDraw(g, offset, pen);
            pen.Dispose();
        }

        public override void Hide(Graphics g, Point offset)
        {
            Pen pen = new Pen(Color.White, lineSize);
            BaseDraw(g, offset, pen);
            pen.Dispose();
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

        public override FigureType getType() => FigureType.Line;

        public override List<Control> getControls()
        {
            List<Control> control = new List<Control>
            {
                getControlLineColor(),
                getLineSizeControl(),
                getPointsControl()
            };
            return control;
        }

        public override void setParameters(Control.ControlCollection controls) 
        {
            updateLineColor(((FlowLayoutPanel)controls[0]).Controls);
            updateLineSize(((FlowLayoutPanel)controls[1]).Controls);
            updatePoints(((FlowLayoutPanel)controls[2]).Controls);
        }

    }
}
