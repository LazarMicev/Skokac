using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace Training.Controls
{
    public class MyButton : Button
    {

        //Properties
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color borderColor = Color.Black;
        private Color backgroundColor = Color.Transparent;
        private Color backgroundColorOnFocus = Color.LightGray;
        private Color backgroundColorOnMouseEnter = Color.LightGray;
        private bool isFocsed = false;
        private bool isOnMouse = false;

        [Category("A")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("A")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }
        [Category("A")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        [Category("A")]
        public Color BackgroundColor
        {
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = value;
                this.Invalidate();
            }
        }
        [Category("A")]
        public Color BackgroundColorOnFocus
        {
            get
            {
                return backgroundColorOnFocus;
            }
            set
            {
                backgroundColorOnFocus = value;
                this.Invalidate();
            }
        }
        protected override bool ShowFocusCues
        {
            get { return false; }
        }

        [Category("A")]
        public Color BackgroundColorOnMouseEnter
        {
            get
            {
                return backgroundColorOnMouseEnter;
            }
            set
            {
                backgroundColorOnMouseEnter = value;
            }
        }








        //Constructor
        public MyButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BorderSize = 1;
            this.Size = new Size(150, 40);
            this.BackColor = Color.Transparent;

            this.ForeColor = Color.Black;

        }

        //Methods
        private GraphicsPath GetFigurePath(RectangleF react, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(react.X, react.Y, radius, radius, 180, 90);
            path.AddArc(react.Width - radius, react.Y, radius, radius, 270, 90);
            path.AddArc(react.Width - radius, react.Height - radius, radius, radius, 0, 90);
            path.AddArc(react.X, react.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectBoarder = new RectangleF(1, 1, Width - 0.8F, Height - 1);

            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBoarder, borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    Region = new Region(pathSurface);

                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    //Draw control border
                    if (borderSize >= 1)
                    {
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                    }

                }
            }
            else
            {
                this.Region = new Region(rectSurface);
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChange);
        }

        private void Container_BackColorChange(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                this.Invalidate();
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            isFocsed = true;
            this.BackColor = backgroundColorOnFocus;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isFocsed = false;
            if (isOnMouse)
            {
                this.BackColor = backgroundColorOnMouseEnter;
            }
            else
            {
                this.BackColor = backgroundColor;
            }
        }
        
    }
}
