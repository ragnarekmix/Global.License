using System;
using System.IO;
using System.Xml.Linq;
using Global.LicenseManager.Common.Log;

namespace Global.LicenseManager.Data
{
    public class FileSystem
    {
        private ILogger log;

        public FileSystem(ILogger log)
        {
            this.log = log;
        }

        public virtual string ReadFile(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                var msg = String.Format("ERROR: Cannot read file {0}", fileName);
                log.Error(msg);
                throw new ApplicationException(msg, e);
            }
        }

        public virtual void SaveFile(XDocument document, string fileName)
        {
            try
            {
                document.Save(fileName);
            }
            catch (Exception e)
            {
                var msg = String.Format("ERROR: Cannot save file {0}", fileName);
                log.Error(msg);
                throw new ApplicationException(msg, e);
            }
        }
    }
}