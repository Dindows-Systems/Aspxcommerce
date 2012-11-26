using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SageFrame.Common
{
    public class IOHelper
    {
        public static bool DeleteDirectory(string target_dir)
        {
            bool result = false;

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);

            return result;
        }
        public static bool DeleteDirectoryFiles(string target_dir,string ext_todelete)
        {
            bool result = false;

            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            string[] ext_arr_todelete = ext_todelete.Split(',');

            foreach (string file in files)
            {
                if (ext_arr_todelete.Contains(Path.GetExtension(file)))
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }
            }          

            return result;
        }
    }
}
