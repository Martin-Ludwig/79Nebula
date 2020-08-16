using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Godot.File;

namespace Nebula._79Nebula.Utils
{
    public class FileHandler
    {

        private const string _prefix = "res://";
        private const string _root = "79Nebula";

        public static string ReadFile(string filepath)
        {
            File file = new File();

            if (!file.FileExists(_prefix + _root + filepath))
            {
                throw new FileNotFoundException(_prefix + _root + filepath);
            }

            file.Open(_prefix + _root + filepath, File.ModeFlags.Read);
            return file.GetAsText();
        }

    }
}
