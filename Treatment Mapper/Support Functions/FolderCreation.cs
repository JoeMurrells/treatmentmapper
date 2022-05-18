using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Treatment_Mapper.Support_Functions
{
    public static class FolderCreation
    {
        public static void CreateFolders(string exePath, string pRef)
        {
            Directory.CreateDirectory($@"{exePath}\output\{pRef}");

            if (!Directory.Exists($@"{exePath}\MasterCSV"))
            {
                Directory.CreateDirectory($@"{exePath}\MasterCSV");
            }

            if (!Directory.Exists($@"{exePath}\output"))
            {
                Directory.CreateDirectory($@"{exePath}\output");
            }

            if (!Directory.Exists($@"{exePath}\backup"))
            {
                Directory.CreateDirectory($@"{exePath}\backup");
            }
        }
    }
}
