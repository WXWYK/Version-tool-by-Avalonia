using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

namespace PlatesAvaloniaProject.Helpers
{
    public class FileInfoHelper : IEquatable<FileInfo>
    {
        private readonly FileInfo _fileInfo;   

        public FileInfoHelper(FileInfo fileInfo) 
        {
            _fileInfo = fileInfo;
            Name = _fileInfo.Name;
            FullName = _fileInfo.FullName;
        }

        public string Name { get; internal set; }
        public string FullName { get; internal set; }

        public void MoveTo(string destFileName)
        {
            _fileInfo.MoveTo(destFileName);
        }

        public bool Equals(FileInfo? other)
        {
           return CompareByMD5(_fileInfo.FullName, other.FullName);
        }

        /// <summary>
        ///  利用hash值比较是否相等
        /// </summary>
        /// <param name="file1">文档1</param>
        /// <param name="file2">文档2</param>
        /// <returns></returns>
        private bool CompareByMD5(string file1, string file2)
        {
            // 使用.NET内置的MD5库
            using (var md5 = MD5.Create())
            {
                byte[] one, two;
                using (var fs1 = File.Open(file1, FileMode.Open))
                {
                    // 以FileStream读取文件内容,计算HASH值
                    one = md5.ComputeHash(fs1);
                }
                using (var fs2 = File.Open(file2, FileMode.Open))
                {
                    // 以FileStream读取文件内容,计算HASH值
                    two = md5.ComputeHash(fs2);
                }
                // 将MD5结果(字节数组)转换成字符串进行比较
                return BitConverter.ToString(one) == BitConverter.ToString(two);
            }
        }

        /// <summary>
        /// 根据快捷方式，获取程序所在路径
        /// </summary>
        /// <param name="shortCutName"></param>
        /// <returns></returns>
        public static IWshShortcut GetPathByShortcut(string shortCutName)
        {
            if (File.Exists(shortCutName))
            {
                WshShell shell = new WshShell();
                IWshShortcut link = (IWshShortcut)shell.CreateShortcut(shortCutName);             
                return link;
            }
            return default;
        }
    }
}
