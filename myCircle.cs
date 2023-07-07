using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace Skocko
{
    public class myCircle : Button
    {
        private Color borderColor = Color.Black;
        private Color colorInside = Color.White;

        [Category("A")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Refresh();
            }
        }

        [Category("A")]
        [DefaultValue(typeof(Color), "White")]
        public Color ColorInside
        {
            get { return colorInside; }
            set
            {
                colorInside = value;
                Refresh();
            }
        }

        [Browsable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, Width, Height);
            RectangleF rectBorder = new RectangleF(1, 1, Width - 2, Height - 2);

            pevent.Graphics.FillEllipse(new SolidBrush(colorInside), rectSurface);
            using (Pen borderPen = new Pen(borderColor, 1))
            {
                pevent.Graphics.DrawEllipse(borderPen, rectBorder);
            }
        }

       
    }
}
