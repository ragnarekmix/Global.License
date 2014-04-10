using Global.LicenseManager.Common.Enums;
using Global.LicenseManager.Common.Log;
using System;
using System.Configuration;

namespace Global.LicenseManager.Common.Configuration
{
    public class Config
    {
        ILogger log;

        public Config(ILogger log)
        {
            this.log = log;
        }

        public DataSourse GetDataSource()
        {
            var source = ConfigurationManager.AppSettings["DataSource"];

            if (source == null)
            {
                var msg = "Missed configuration. DataSource is not set in appConfig";
                log.Error(msg);
                throw new ConfigurationErrorsException(msg);
            }

            try
            {
                return (DataSourse)Enum.Parse(typeof(DataSourse), source);
            }
            catch (Exception e)
            {
                var msg = "Wrong configuration. DataSource value is invalid. Valid values: 'DataBase, Xml'";
                log.Error(msg, e);
                throw new ConfigurationErrorsException(msg, e);
            }
        }

        public string GetXmlSourcePath()
        {
            var source = ConfigurationManager.AppSettings["XmlSourcePath"];

            if (source == null)
            {
                var msg = "Missed configuration. XmlSourcePath is not set in appConfig";
                log.Error(msg);
                throw new ConfigurationErrorsException(msg);
            }

            return source;
        }
    }
}