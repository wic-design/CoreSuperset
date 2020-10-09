using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;


namespace DCSP {
    /// <summary>
    /// 验证码工具
    /// </summary>
    public class Captcha {
        static private Random random = new Random();

        /// <summary>
        /// 生成字符串验证码，可包含字母(不分大小写)和数字
        /// </summary>
        /// <param name="Count">生成的验证码位数</param>
        /// <returns></returns>
        static public String CreateCode(int Count) {
            string[] ch = {"0","1","2","3","4","5","6","7","8","9","A","B",
                           "C","D","E","F","G","H","I","J","K","L","M","N",
                           "O","P","Q","R","S","T", "U","V","W","X","Y","Z"};  //个数36
            string code = "";
            for (int i = 0; i < Count; i++) {
                code += ch[random.Next(0, 36)];
            }
            return code;
        }

        /// <summary>
        /// 生成数字验证码
        /// </summary>
        /// <param name="Count">生成的验证码位数</param>
        /// <returns></returns>
        static public String CreatNumberCode(int Count) {
            string code = "";
            for (int i = 0; i < Count; i++) {
                code += random.Next(0, 9).ToString();
            }
            return code;
        }

        /// <summary>
        /// 生成不重复的短代码(不区分大小写)
        /// </summary>
        /// <returns></returns>
        static public String CreatShortKey() {
            // 转36进制字符串
            long i = DateTime.Now.Ticks;
            string s = "";
            long j = 0;
            while (i > 36) {
                j = i % 36;
                if (j <= 9)
                    s += j.ToString();
                else
                    s += Convert.ToChar(j - 10 + 'a');
                i = i / 36;
            }
            if (i <= 9)
                s += i.ToString();
            else
                s += Convert.ToChar(i - 10 + 'a');
            Char[] c = s.ToCharArray();
            Array.Reverse(c);
            char[] sE = new char[c.Length];

            // 交叉排序
            int kc = c.Length / 2;
            for (int k = 0; k < kc; k++) {
                sE[k * 2] = c[k];
                sE[k * 2 + 1] = c[kc * 2 - k - 1];
            }
            return new string(sE);
        }

        /// <summary>
        /// 生成不重复的短代码(区分大小写,比CreatShortKey更短)
        /// </summary>
        /// <returns></returns>
        static public String CreatSShortKey() {
            // 转36进制字符串
            long i = DateTime.Now.Ticks;
            string s = "";
            long j = 0;
            while (i > 62) {
                j = i % 62;
                if (j <= 9)
                    s += j.ToString();
                else if (j <= 35)
                    s += Convert.ToChar(j - 10 + 'a');
                else
                    s += Convert.ToChar(j - 36 + 'A');

                i = i / 62;
            }
            if (i <= 9)
                s += i.ToString();
            else if (i <= 35)
                s += Convert.ToChar(i - 10 + 'a');
            else
                s += Convert.ToChar(i - 36 + 'A');
            Char[] c = s.ToCharArray();
            Array.Reverse(c);
            char[] sE = new char[c.Length];


            // 交叉排序
            int kc = c.Length / 2;
            for (int k = 0; k < kc; k++) {
                sE[k * 2] = c[k];
                sE[k * 2 + 1] = c[kc * 2 - k - 1];
            }

            return new string(sE);
        }
      
        /// <summary>
        /// 绘制base64的验证码的图片
        /// </summary>
        /// <param name="code">验证码</param>
        public static string CodeToImg(string code) {
            string[] fontFamily = { "Impact", "Arial", "Courier", "MS Serif", "Comic Sans", "Roman", "Palatino Linotype" };
            FontStyle[] fontStyles = { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, (FontStyle.Bold | FontStyle.Italic) };
            Color[] fontColors = { Color.LightSlateGray, Color.CadetBlue, Color.Blue, Color.BlueViolet, Color.MidnightBlue, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkRed, Color.Indigo };

            Bitmap image = new Bitmap((int)Math.Ceiling(code.Length * 18.0), 33);
            Color[] colors = { Color.Aquamarine, Color.Silver, Color.NavajoWhite, Color.LightPink, Color.Cyan };
            Graphics g = Graphics.FromImage(image);
            try {
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++) {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(colors[random.Next(colors.Length)]), x1, y1, x2, y2);
                }
                //画文字
                for (int i = 0; i < code.Length; i++) {
                    SolidBrush brush = new SolidBrush(fontColors[random.Next(0, fontColors.Length)]);
                    Font font = new Font(fontFamily[random.Next(0, fontFamily.Length)], random.Next(15, 20), fontStyles[random.Next(0, fontStyles.Length)]);
                    g.DrawString(code[i].ToString(), font, brush, i * 15 + random.Next(-3, 3), 2 + random.Next(-5, 5));
                }

                //画图片的前景干扰点
                for (int i = 0; i < 100; i++) {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                byte[] arr = stream.ToArray();
                return Convert.ToBase64String(arr);
            } finally {
                g.Dispose();
                image.Dispose();
            }
        }

    }
}
