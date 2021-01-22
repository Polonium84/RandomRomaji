using System;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.Reflection;

namespace INISupport
{
    public static class INI
    {
        #region DLL引用
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
             string section,
             string key,
             string val,
             string filePath
            );
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath
            );
        #endregion
        /// <summary>
        /// 读配置数据，自定义文件路径
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static string ReadINI(string section, string key, string filepath)
        {
            string value;
            if (File.Exists(filepath))
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    GetPrivateProfileString(section, key, "VOID_KEY", sb, 10240, filepath);
                    value = sb.ToString();
                }
                catch
                {
                    value = "UKNOWN_ERROR";
                }
            }
            else
            {
                value = "INVALID_FILEPATH";
            }
            return value;
        }
        /// <summary>
        /// 读配置数据，自定义文件路径和数据大小
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="filepath">文件路径</param>
        /// <param name="size">数据大小</param>
        /// <returns></returns>
        public static string ReadINI(string section, string key, string filepath, int size)
        {
            string value;
            if (File.Exists(filepath))
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    GetPrivateProfileString(section, key, "VOID_KEY", sb, size, filepath);
                    value = sb.ToString();
                }
                catch
                {
                    value = "UKNOWN_ERROR";
                }
            }
            else
            {
                value = "INVALID_FILEPATH";
            }
            return value;
        }
        /// <summary>
        /// 读取配置数据，使用默认文件路径和数据大小
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static string ReadINI(string section, string key)
        {
            string value;
            FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            string dicpath = fileInfo.DirectoryName + '\\';
            string filepath = dicpath + Assembly.GetEntryAssembly().GetName().Name + ".ini";
            if (File.Exists(filepath))
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    GetPrivateProfileString(section, key, "VOID_KEY", sb, 10240, filepath);
                    value = sb.ToString();
                }
                catch
                {
                    value = "UKNOWN_ERROR";
                }
            }
            else
            {
                value = "INVALID_FILEPATH";
            }
            return value;
        }
        /// <summary>
        /// 写配置数据
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="value">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static bool WriteINI(string section, string key, string value, string filepath)
        {
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
            long status = WritePrivateProfileString(section, key, value, filepath);
            if (status != 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 写配置数据，使用默认文件路径
        /// </summary>
        /// <param name="section">节名称</param>
        /// <param name="key">键名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool WriteINI(string section, string key, string value)
        {
            FileInfo fileInfo = new FileInfo(Assembly.GetEntryAssembly().Location);
            string dicpath = fileInfo.DirectoryName + '\\';
            string filepath = dicpath + Assembly.GetEntryAssembly().GetName().Name + ".ini";
            if (!File.Exists(filepath))
            {
                File.Create(filepath);
            }
            long status = WritePrivateProfileString(section, key, value, filepath);
            if (status != 0)
                return true;
            else
                return false;
        }
    }
}
