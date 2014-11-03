using LicenseManager.Common.Enums;
using System;
using System.Configuration;

namespace LicenseManager.Common.Configuration
{
    public class Config
    {
        public virtual DataSourse GetDataSource()
        {
            var source = ConfigurationManager.AppSettings["DataSource"];

            if (source == null)
            {
                var msg = "Missed configuration. DataSource is not set in appConfig";
                throw new ConfigurationErrorsException(msg);
            }

            try
            {
                return (DataSourse)Enum.Parse(typeof(DataSourse), source);
            }
            catch (Exception e)
            {
                var msg = "Wrong configuration. DataSource value is invalid. Valid values: 'DataBase, Xml'";
                throw new ConfigurationErrorsException(msg, e);
            }
        }

        public virtual string GetXmlSourcePath()
        {
            var source = ConfigurationManager.AppSettings["XmlSourcePath"];

            if (source == null)
            {
                var msg = "Missed configuration. XmlSourcePath is not set in appConfig";
                throw new ConfigurationErrorsException(msg);
            }

            return source;
        }
    }
}