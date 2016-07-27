using HandyWork.DAL.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Manager
{
    public class CacheManager
    {
        public static void LoadCache()
        {
            SysColumnsCache.LoadData();
        }
    }
}
