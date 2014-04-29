using Global.LicenseManager.Common.Configuration;
using Global.LicenseManager.Common.Log;
using System;
using System.IO;
using System.Xml.Linq;

namespace Global.LicenseManager.Data
{
    public class FileSystem
    {
        ILogger log;
        Config config;

        public FileSystem(ILogger log, Config config)
        {
            this.log = log;
            this.config = config;
        }

        public virtual string ReadXmlFile()
        {
            var source = config.GetXmlSourcePath();
            try
            {
                return File.ReadAllText(source);
            }
            catch (Exception e)
            {
                var msg = String.Format("ERROR: Cannot read file {0}", source);
                log.Error(msg);
                throw new ApplicationException(msg, e);
            }
        }

        public virtual void SaveXmlFile(string text)
        {
            var source = config.GetXmlSourcePath();
            try
            {
                var document = XDocument.Parse(text);
                document.Save(source);
            }
            catch (Exception e)
            {
                var msg = String.Format("ERROR: Cannot save file {0}", source);
                log.Error(msg);
                throw new ApplicationException(msg, e);
            }
        }
    }
}