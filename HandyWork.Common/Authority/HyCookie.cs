using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Authority
{
    public class HyCookie
    {
        public int GreaterThanUTCInMinute { set; get; }
        public string Name { set; get; }
        public string Id { set; get; }
        public string NickName { get; set; }
        public string[] Roles { get; set; }
        public string Encoder() => JsonConvert.SerializeObject(this);
        public static HyCookie Decoder(string userDataStr)
        {
            if (string.IsNullOrWhiteSpace(userDataStr))
            {
                return null;
            }
            HyCookie ud = JsonConvert.DeserializeObject<HyCookie>(userDataStr);
            return ud;
        }
    }
}
