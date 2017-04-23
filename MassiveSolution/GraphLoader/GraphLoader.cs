using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphLoader
{
    public class GraphLoader
    {
        private string _folder;

        public GraphLoader(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentException(nameof(folder));
            }

            if (!Directory.Exists(folder))
            {
                throw new DirectoryNotFoundException($"Could not find folder ('{Path.GetFullPath(folder)}')");
            }

            _folder = folder;
        }

        public void Load()
        {
            foreach (string file in Directory.EnumerateFiles(_folder, "*.xml"))
            {
                string contents = File.ReadAllText(file);
            }

            throw new NotImplementedException("Not mapping to entities yet");
        }
    }
}
