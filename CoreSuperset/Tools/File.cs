using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.IO {
    /// <summary>
    /// 文件储存的简单封装
    /// </summary>
    public class EasyFile {

        /// <summary>
        /// 字符串保存文件
        /// </summary>
        /// <param name="text"></param>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        public static void SaveFile(string text, string fileName, Encoding encoder = null) {
            if (encoder == null) encoder = Encoding.UTF8;

            fileName = fileName.ToLower();
            string path = Path.GetDirectoryName(fileName); //获取文件路径
            Directory.CreateDirectory(path);  //创建文件夹(如果不存在)

            File.WriteAllText(fileName, text, encoder);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileData">文件数据的byte[]</param>
        /// <param name="objectName">路径及文件名</param>
        /// <returns></returns>
        public static void SaveFile(byte[] fileData, string fileName) {
            MemoryStream stream = new MemoryStream(fileData);
            SaveFile(stream, fileName);
            stream.Close();
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="fileName">完整路径(路径+文件名)</param>
        private static void SaveFile(Stream fileStream, string fileName) {
            fileStream.Seek(0, SeekOrigin.Begin); //这一条非常重要，极其重要！！！不能删除也不要修改位置，否则的话保存的文件将会损坏
            fileName = fileName.ToLower();

            string path = Path.GetDirectoryName(fileName); //获取文件路径
            string name = System.IO.Path.GetFileName(fileName); //获取文件名
            Directory.CreateDirectory(path);  //创建文件夹(如果不存在)

            //构建写文件流对象
            using (FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
                byte[] srcBuf = new Byte[fileStream.Length];
                fileStream.Read(srcBuf, 0, srcBuf.Length);
                file.Write(srcBuf, 0, srcBuf.Length);
                file.Close();
            }
        }



    }
}
