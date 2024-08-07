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
    class Curve : Figure
    {
        public Curve(Point pointOne, Point pointTwo, Point offset, int lineSize, Color lineColor) :
            base(pointOne, pointTwo, offset, lineSize, lineColor)
        { }

        public override void BaseDraw(Graphics g, Point offset, Pen pen, SolidBrush solidBrush = null)
        {
            Point[] p = points.ToArray();

            for (int i = 0; i < p.Count(); ++i)
            {
                p[i].X += offset.X;
                p[i].Y += offset.Y;
            }

            g.DrawCurve(pen, p);
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

        public override void MouseMove(Graphics g, Point mousePosition, Point offset)
        {
            points.Add(new Point(mousePosition.X - offset.X, mousePosition.Y - offset.Y));
        }

        public override void FinishDraw(Graphics g, Point offset)
        {
            Falidate();
        }

        public override void Falidate()
        {
            if (points.Count() == 2)
            {
                isCorrect = StatusCheck.Bad;
            }
            else
            {
                isCorrect = StatusCheck.Good;
            }
        }

        public override void Align(int gridPitch, Size size)
        {
            int last = points.Count - 1;
            Point start = new Point(DotRound(points[0].X, gridPitch, size.Width),
                                  DotRound(points[0].Y, gridPitch, size.Height));
            Point end = new Point(DotRound(points[last].X, gridPitch, size.Width),
                                   DotRound(points[last].Y, gridPitch, size.Height));
            Console.WriteLine(start.ToString()+" "+end.ToString());
            //ReSizingCurve(start, end);

            List<Point> box = GetRectangle();
            double scaleCoeffX = (end.X - start.X) / (double)(points[last].X - points[0].X);
            double scaleCoeffY = (end.Y - start.Y) / (double)(points[last].Y - points[0].Y);

            scaleCoeffX = scaleCoeffX < 0.001 ? 1 : scaleCoeffX;
            scaleCoeffY = scaleCoeffY < 0.001 ? 1 : scaleCoeffY;

            List<Point> _points = new List<Point>();
            _points.AddRange(points);

            for (int i = 0; i < _points.Count; ++i)
            {
                _points[i] = new Point(
                    start.X + (int)Math.Round(scaleCoeffX * (_points[i].X - points[0].X)),
                    start.Y + (int)Math.Round(scaleCoeffY * (_points[i].Y - points[0].Y))
                );
            }

            points.Clear();
            points.AddRange(_points);
        }

        //private void ReSizingCurve(Point start, Point end) // TODO NEED FIX
        //{
        //    int last = points.Count - 1;
        //    if (points.Count() == 2)
        //    {
        //        points = new List<Point> { start, end };
        //    }
        //    else
        //    {
        //        float shiftX = (float)(points[last].X - points[0].X) / (end.X - start.X);
        //        float shiftY = (float)(points[last].Y - points[0].Y) / (end.Y - start.Y);
        //        shiftX = shiftX < 0 ? 1 : shiftX;
        //        shiftY = shiftY < 0 ? 1 : shiftY;
        //        Point p1 = points[0];
        //        for (int i = 0; i < points.Count; i++)
        //        {
        //            points[i] = new Point(
        //                start.X + (int)Math.Round((points[i].X - p1.X) * shiftX),
        //                start.Y + (int)Math.Round((points[i].Y - p1.Y) * shiftY));
        //        }
        //    }
        //}

        public override void UpdatePoints(List<Point> curPoints, Point _mouse, String x, String y)
        {
            List<Point> box = new List<Point> {
                new Point(prevPoints.Min(point => point.X), prevPoints.Min(point => point.Y)),
                new Point(prevPoints.Max(point => point.X), prevPoints.Max(point => point.Y))
            };
            List<Point> boxNew = GetRectangle();

            if (x == "max")
            {
                boxNew[1] = new Point(_mouse.X, boxNew[1].Y);
            }
            else if (x == "min")
            {
                boxNew[0] = new Point(_mouse.X, boxNew[0].Y);
            }
            if (y == "max")
            {
                boxNew[1] = new Point(boxNew[1].X, _mouse.Y);
            }
            else if (y == "min")
            {
                boxNew[0] = new Point(boxNew[0].X, _mouse.Y);
            }
            Console.WriteLine(box[0].ToString() + " " + box[1].ToString());
            Console.WriteLine(boxNew[0].ToString() + " " + boxNew[1].ToString());
            double scaleCoeffX = (boxNew[1].X - boxNew[0].X) / (double)(box[1].X - box[0].X);
            double scaleCoeffY = (boxNew[1].Y - boxNew[0].Y) / (double)(box[1].Y - box[0].Y);
            scaleCoeffX = scaleCoeffX > 0.00001 ? scaleCoeffX : 1;
            scaleCoeffY = scaleCoeffY > 0.00001 ? scaleCoeffY : 1;

            Console.WriteLine(scaleCoeffX.ToString() + " " + scaleCoeffY.ToString());
            Console.WriteLine();

            List<Point> _points = new List<Point>();
            _points.AddRange(prevPoints);


            for (int i = 0; i < _points.Count; i++)
            {
                _points[i] = new Point(boxNew[0].X + (int)Math.Round(scaleCoeffX * (_points[i].X - box[0].X)), 
                                       boxNew[0].Y + (int)Math.Round(scaleCoeffY * (_points[i].Y - box[0].Y)));
            }

            points.Clear();
            points.AddRange(_points);


        }

        public override FigureType getType() => FigureType.Curve;
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
