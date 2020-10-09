using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

    /// <summary>
    /// String字符串扩展方法
    /// </summary>
    public static class StringHelper {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMD5(this String value) {
            MD5 md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Sha1加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSha1(this String value) {
            var strRes = Encoding.Default.GetBytes(value);
            HashAlgorithm iSha = new SHA1CryptoServiceProvider();
            strRes = iSha.ComputeHash(strRes);
            var enText = new StringBuilder();
            foreach (byte iByte in strRes) {
                enText.AppendFormat("{0:x2}", iByte);
            }
            return enText.ToString();
        }

        /// <summary>
        /// 将base64字符串转换为图像
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Bitmap Base64ToBitmap(this String base64) {
            //去掉前面的标识   //data:image/png;base64,
            int index = base64.IndexOf("base64,");
            if (index >= 0) {
                base64 = base64.Substring(index + 7);
            }

            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            Bitmap bmp = new Bitmap(memStream);
            memStream.Close();
            return bmp;
        }

        /// <summary>
        /// 将base64字符串转换为图像字节流
        /// </summary>
        /// <param name="base64"></param>
        /// <param name="qualityLevel">压缩率，默认80</param>
        /// <returns></returns>
        public static byte[] Base64ToBytes(this String base64, ImageFormat format = null, byte qualityLevel = 80) {
        if (format == null) format = ImageFormat.Jpeg;
            var bmp = base64.Base64ToBitmap();
            //压缩参数
            ImageCodecInfo imageEncoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(v => v.FormatID == format.Guid);
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qualityLevel);

            MemoryStream mstream = new MemoryStream();
            bmp.Save(mstream, imageEncoder, encoderParameters);
            byte[] byData = new Byte[mstream.Length];
            mstream.Position = 0;
            mstream.Read(byData, 0, byData.Length); mstream.Close();
            return byData;
        }

    }
