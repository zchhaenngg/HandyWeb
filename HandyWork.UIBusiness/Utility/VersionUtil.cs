using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Utility
{
    public class VersionUtil
    {
        public static string GetVersionNumber()
        {
            string returnValue = string.Empty;
            try
            {
                string versionUrl = System.Web.HttpContext.Current.Server.MapPath("/") + "\\VersionNumber.txt";
                using (StreamReader sr = new StreamReader(versionUrl, Encoding.UTF8))
                {
                    returnValue = sr.ReadToEnd();
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                //LogHelper._ErrorLog.Error(ex.Message, ex);
                return "0.0.0.1";
            }
        }
    }
}
