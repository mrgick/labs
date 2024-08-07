using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint
{
    [Serializable()]
    class Text : Figure
    {
        public Font font;
        public string text = System.String.Empty;
        [NonSerialized()]
        private Form parent;


        public Text(Point pointOne, Point pointTwo, Point offset, int lineSize, Color lineColor, Font font, Form parent) :
            base(pointOne, pointTwo, offset, lineSize, lineColor)
        {
            this.font = font;
            this.parent = parent;
        }
        public override void DrawHash(Graphics g, Point offset) { }
        private Point[] normPoints(Graphics g, Point offset)
        {
            Point normPointOne = new Point(points[0].X + offset.X, points[0].Y + offset.Y);
            Point normPointTwo = new Point(points[1].X + offset.X, points[1].Y + offset.Y);
            Normalization(ref normPointOne, ref normPointTwo);
            Point[] res = { normPointOne, normPointTwo };
            return res;
        }
        public override void BaseDraw(Graphics g, Point offset, Pen pen, SolidBrush solidBrush) 
        {
            Point[] points = normPoints(g, offset);
            System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.FromLTRB(points[0].X, points[0].Y, points[1].X, points[1].Y);

            if (solidBrush != null)
            {
                g.FillRectangle(solidBrush, rectangle);
            }

            g.DrawRectangle(pen, rectangle);
        }

        public override void Draw(Graphics g, Point offset)
        {
            if (selected)
            {
                Pen pen = new Pen(lineColor, lineSize)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                };
                System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.FromLTRB(points[0].X, points[0].Y, points[1].X, points[1].Y);
                g.DrawString(text, font, new SolidBrush(lineColor), rectangle);
                g.DrawRectangle(pen, rectangle);
            }
            else if (text == System.String.Empty)
            {
                Pen pen = new Pen(Color.Black, 1);
                BaseDraw(g, offset, pen, null);
                pen.Dispose();
            }
            else
            {
                Point[] points = normPoints(g, offset);
                System.Drawing.Rectangle rectangle = System.Drawing.Rectangle.FromLTRB(points[0].X, points[0].Y, points[1].X, points[1].Y);
                g.DrawString(text, font, new SolidBrush(lineColor), rectangle);
            }
            if (showBox)
            {
                DrawBox(g, offset);
            }
        }

        public override void Hide(Graphics g, Point offset)
        {
            Pen pen = new Pen(Color.White, lineSize);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            BaseDraw(g, offset, pen, solidBrush);
            pen.Dispose();
            solidBrush.Dispose();
        }
        public void Click(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                parent.Invalidate();
                text = textBox.Text;
                Falidate();
                textBox.Dispose();

                MainForm main = (MainForm)parent.ParentForm;
                main.drawFontType(font, true);
            }
        }

        public override void FinishDraw(Graphics g, Point offset)
        {
            TextBox textBox = new TextBox();
            textBox.Parent = parent;

            Point[] points = normPoints(g, offset);

            textBox.Location = points[0];
            textBox.Size = new Size(points[1].X - points[0].X, points[1].Y - points[0].Y);
            textBox.Multiline = true;
            textBox.Font = font;
            textBox.ForeColor = lineColor;
            textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Click);
            
            MainForm main = (MainForm)parent.ParentForm;
            main.drawFontType(font);
        }

        public override void Falidate()
        {
            if (points[0] == points[1])
            {
                isCorrect = StatusCheck.Bad;
                return;
            }
            if (text == System.String.Empty)
            {
                isCorrect = StatusCheck.Bad;
                return;
            }
            isCorrect = StatusCheck.Good;
        }

        public override bool isText() => true;
        public override FigureType getType() => FigureType.Text;

        public override void setFont(Font font)
        {
            this.font = font;
        }

        public override List<Control> getControls()
        {
            List<Control> control = new List<Control>
            {
                getControlLineColor(),
                getLineSizeControl(),
                getFontControl(),
                getTextControl(),
                getPointsControl()
            };
            return control;
        }
        public override void setParameters(Control.ControlCollection controls)
        {
            updateLineColor(((FlowLayoutPanel)controls[0]).Controls);
            updateLineSize(((FlowLayoutPanel)controls[1]).Controls);
            updateFont(((FlowLayoutPanel)controls[2]).Controls);
            updateText(((FlowLayoutPanel)controls[3]).Controls);
            updatePoints(((FlowLayoutPanel)controls[4]).Controls);
        }

        public virtual Control getFontControl()
        {
            var panel = getPanel();
            var label = getLabel("Шрифт", 70);
            var textBox = new TextBox()
            {
                BackColor = Color.White,
                Text = font.Name.ToString()+" "+font.Size.ToString(),
                Font = new Font("Times New Roman", 12),
                Tag = font,
                Size = new Size(160, 50),
                ReadOnly = true
            };
            textBox.MouseClick += (object sender, MouseEventArgs e) =>
            {
                var fontDialog = new FontDialog()
                {
                    Font = (Font)textBox.Tag
                };
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = fontDialog.Font.Name.ToString() + " " + fontDialog.Font.Size.ToString();
                    textBox.Tag = fontDialog.Font;
                }
            };
            panel.Controls.AddRange(new Control[] { label, textBox });
            return panel;
        }

        public void updateFont(Control.ControlCollection controls)
        {
            font = (Font)controls[1].Tag;
        }

        public virtual Control getTextControl()
        {
            var panel = getPanel(50);
            var label = getLabel("Текст", 70);
            var textBox = new TextBox()
            {
                BackColor = Color.White,
                Text = text,
                Font = new Font("Times New Roman", 12),
                Size = new Size(160, 50),
                Multiline = true
            };
            panel.Controls.AddRange(new Control[] { label, textBox });
            return panel;
        }

        public void updateText(Control.ControlCollection controls)
        {
            text = (String)controls[1].Text;
        }
    }
}
