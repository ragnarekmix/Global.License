using LicenseManager.Common.Configuration;
using System.IO;
using System.Xml.Linq;

namespace LicenseManager.DataAccess
{
    public class FileSystem
    {
        Config config;

        public FileSystem(Config config)
        {
            this.config = config;
        }

        public virtual string ReadXmlFile()
        {
            var source = config.GetXmlSourcePath();
            return File.ReadAllText(source);
        }

        public virtual void SaveXmlFile(string text)
        {
            var source = config.GetXmlSourcePath();
            var document = XDocument.Parse(text);
            document.Save(source);
        }
    }
}