﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Puya.Captcha
{
    // source: https://github.com/art264400/SimpleCaptcha
    public class SimpleCaptcha
    {
        private SimpleCaptchaOptions options;
        public SimpleCaptchaOptions Options
        {
            get
            {
                if (options == null)
                {
                    options = new SimpleCaptchaOptions();
                }

                return options;
            }
            set
            {
                options = value;
            }
        }
        public SimpleCaptcha()
        { }
        public SimpleCaptcha(SimpleCaptchaOptions options)
        {
            Options = options;
        }
        public byte[] Generate(out string captcha)
        {
            captcha = GenerateRandomString();

            using (var ms = new MemoryStream())
            {
                using (var bmp = new Bitmap(Options.Width, Options.Height))
                {
                    var rnd = new Random();
                    var rectf = new RectangleF(rnd.Next(10, 20), rnd.Next(4, 10), 0, 0);

                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Bezier(g, rnd);
                        Lines(g, rnd);
                        g.DrawString(captcha, options.Font, options.FontColor, rectf);
                        Noise(bmp, rnd);
                        g.Flush();

                        bmp.Save(ms, ImageFormat.Jpeg);
                    }
                }

                return ms.ToArray();
            }
        }
        private string GenerateRandomString()
        {
            var rnd = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < Options.Length; i++)
            {
                sb.Append(Options.Characters[rnd.Next(Options.Characters.Length)]);
            }

            return sb.ToString();
        }
        private Color RandomColor()
        {
            int r, g, b;
            var bytes1 = new byte[3];
            var rnd1 = new Random();

            rnd1.NextBytes(bytes1);

            r = Convert.ToInt16(bytes1[0]);
            g = Convert.ToInt16(bytes1[1]);
            b = Convert.ToInt16(bytes1[2]);

            return Color.FromArgb(r, g, b);
        }
        private void Bezier(Graphics g, Random rnd)
        {
            var start = new Point(rnd.Next(10, 55), rnd.Next(1, 45));
            var control1 = new Point(20, 20);
            var control2 = new Point(25, 80);
            var finish = new Point(rnd.Next(70, 130), 8);

            g.DrawBezier(new Pen(Options.LinesColor, 2), start, control1, control2, finish);
        }
        private void Lines(Graphics g, Random rnd)
        {
            var pen = new Pen(Options.LinesColor, 1.7F);
            var flag = rnd.Next(0, 1);

            if (flag == 0)
            {
                var width1 = 15 + rnd.Next(-20, 20);
                var height1 = 0;
                var width2 = 30 + rnd.Next(-20, 20);
                var height2 = 45;

                g.DrawLine(pen, width1, height1, width2, height2);
                g.DrawLine(pen, width1 + 30, height1, width2 + 30, height2);
                g.DrawLine(pen, width1 + 60, height1, width2 + 60, height2);
                g.DrawLine(pen, width1 + 90, height1, width2 + 90, height2);
            }
        }
        private void Noise(Bitmap bmp, Random rnd)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (rnd.Next(100) <= 22) bmp.SetPixel(x, y, Options.NoiseColor);
                }
            }
        }
    }
}
