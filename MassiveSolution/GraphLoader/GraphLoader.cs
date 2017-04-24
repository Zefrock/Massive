using GraphLib.Models;
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

        public void LoadGraphFromFolder()
        {
            foreach (string file in Directory.EnumerateFiles(_folder, "*.xml"))
            {
                string contents = File.ReadAllText(file);
                //missing code to deserialize xml
            }
        }

        public void SaveGraphToDB()
        {
            using (var context = new GraphDBDataEntities())
            {
                //missing code to map to db entities
                context.SaveChanges();
            }
        }
    }
}
