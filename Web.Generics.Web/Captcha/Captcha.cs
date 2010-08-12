using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Web.Generics.Web.Captcha
{
    // Raphael Cruzeiro 2010-08-12
    public class Captcha
    {
        public string Text { get; set; }
        public Bitmap Image { get; set; }

        private Random random = new Random(DateTime.Now.Second);

        private int width, height, length;

        public Captcha(int width, int height, int length)
        {
            this.width = width;
            this.height = height;
            this.length = length;

            if (Text == null)
                Text = Randomize(length);
        }

        public Captcha(String text, int width, int height)
            : this(width, height, text.Length)
        {
            Text = text;
        }

        public Captcha(int length) : this(240, 120, length) { }

        public Captcha() : this(5) { }

        private string Randomize(int length)
        {
            string result = String.Empty;
            char[] arrChars = new char[] {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 
                's', 't', 'u', 'v', 'x', 'y', 'z', 'w', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'X', 'Y', 'Z', 'W', '0', '1',
                '2', '3', '4', '5', '6', '7', '8', '9' 
            };
            for (int i = length; i > -1; i--)
            {
                int ix = random.Next(0, arrChars.Length);
                result += arrChars[ix];
            }
            return result;
        }

        public void Draw()
        {

            // Creates a 32bits bitmap
            Bitmap bitmap = new Bitmap(
                width,
                height,
                PixelFormat.Format32bppArgb);

            // A Graphic object in which to draw
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // A rectangle to fill the image
            Rectangle rect = new Rectangle(0, 0, width, height);
            HatchBrush hatchBrush = new HatchBrush(
                HatchStyle.SmallConfetti,
                Color.LightGray,
                Color.White);
            g.FillRectangle(hatchBrush, rect);

            // Prepares the font
            SizeF textSize;
            float fontSize = rect.Height + 1;
            Font font;

            // Ajusts the size of the font
            do
            {
                fontSize--;
                font = new Font(
                    "Arial",
                    fontSize,
                    FontStyle.Bold);
                textSize = g.MeasureString(Text, font);
            } while (textSize.Width > rect.Width);

            // Text formating
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Random transform
            GraphicsPath path = new GraphicsPath();
            path.AddString(
                Text,
                font.FontFamily,
                (int)font.Style,
                font.Size,
                rect,
                format);
            float v = 4F;
            PointF[] points = new PointF[] {
                new PointF(
                    random.Next(rect.Width) / v,
                    random.Next(rect.Height) / v),
                new PointF(
                    rect.Width - random.Next(rect.Width) / v,
                    random.Next(rect.Height) / v),
                new PointF(
                    random.Next(rect.Width) / v,
                    rect.Height - random.Next(rect.Height) / v),
                new PointF(
                    rect.Width - random.Next(rect.Width) / v,
                    rect.Height - random.Next(rect.Height) / v)
            };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Draws text
            hatchBrush = new HatchBrush(
                HatchStyle.LargeConfetti,
                Color.LightGray,
                Color.DarkGray);
            g.FillPath(hatchBrush, path);
            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = random.Next(rect.Width);
                int y = random.Next(rect.Height);
                int w = random.Next(m / 50);
                int h = random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Adds random lines
            Color[] colors = new Color[] { 
                Color.DarkGray, Color.Cyan, Color.Violet, Color.LightGreen, Color.Gold, Color.Gray, Color.HotPink, Color.LightPink };
            for (int i = 0; i < colors.Length; i++)
            {
                hatchBrush = new HatchBrush(
                HatchStyle.LargeConfetti,
                Color.LightGray,
                colors[i]);
                Pen pen = new Pen(hatchBrush);
                g.DrawLine(pen, new Point(random.Next(0, rect.Width), random.Next(0, rect.Width)), new Point(random.Next(0, rect.Height), random.Next(0, rect.Height)));
            }

            // Resources cleanup
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            // Sets the image
            Image = bitmap;
        }
    }
}
