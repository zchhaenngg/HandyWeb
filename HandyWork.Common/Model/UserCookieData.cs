using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Model
{
    public class UserCookieData
    {
        public string Name { set; get; }
        public string Id { set; get; }
        public string RealName { get; set; }
        public string[] Roles { set; get; }
        public string Encoder()
        {
            string returnValue = JsonConvert.SerializeObject(this);
            return returnValue;
        }

        public static UserCookieData Decoder(string userDataStr)
        {
            UserCookieData ud = JsonConvert.DeserializeObject<UserCookieData>(userDataStr);
            return ud;
        }
    }
}
