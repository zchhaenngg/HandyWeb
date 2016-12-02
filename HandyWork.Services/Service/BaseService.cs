using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Services.Service
{
    public abstract class BaseService
    {
       protected virtual string LoginId { get; }
        public BaseService(string loginId)
        {
            LoginId = loginId;
        }
    }
}
