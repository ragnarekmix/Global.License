using System;

namespace LicenseManager.Common
{
    public class TimeContext
    {
        public virtual DateTime Now()
        {
            return DateTime.Now;
        }
    }
}