using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Model
{
    public class UserCookie
    {
        public string Name { set; get; }
        public string Id { set; get; }
        public string RealName { get; set; }
        public string[] Roles { set; get; }
        public string Encoder() => JsonConvert.SerializeObject(this);
        public static UserCookie Decoder(string userDataStr)
        {
            if (string.IsNullOrWhiteSpace(userDataStr))
            {
                return null;
            }
            UserCookie ud = JsonConvert.DeserializeObject<UserCookie>(userDataStr);
            return ud;
        }
    }
}
