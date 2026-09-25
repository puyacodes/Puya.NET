using Puya.Extensions;
using System;
using System.IO;

namespace Puya.Base
{
    public class AppPath
    {
        public static string Root
        {
            get
            {
                var result = AppDomain.CurrentDomain.BaseDirectory;

                if (result.EndzWith("\\debug"))
                {
                    result = result.Substring(0, result.Length - 6);
                }
                if (result.EndzWith("\\release"))
                {
                    result = result.Substring(0, result.Length - 8);
                }
                if (result.EndzWith("\\bin"))
                {
                    result = result.Substring(0, result.Length - 4);
                }

                return result;
            }
        }
        public static string GetPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = AppPath.Root;
            }
            else
            {
                if (!Path.IsPathRooted(path))
                {
                    path = AppPath.Root + "\\" + path;
                }
            }

            return path;
        }
        public static string GetFilePath(string path, string filename, string defualtFileName = "")
        {
            var _filename = string.IsNullOrEmpty(filename) ? defualtFileName : filename;

            return Path.Combine(GetPath(path), _filename);
        }
    }
}
