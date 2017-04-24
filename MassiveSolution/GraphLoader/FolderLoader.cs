using System;
using System.Collections.Generic;
using System.IO;

namespace GraphLoader
{
    public class FolderLoader
    {
        private string _folder;

        public FolderLoader(string folder)
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

        public IEnumerable<string> GetNodeFiles()
        {
            foreach (var file in Directory.EnumerateFiles(_folder, "*.xml"))
            {
                yield return File.ReadAllText(file);
            }
        }
    }
}
