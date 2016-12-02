﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.Authority
{
    public class Cookie
    {
        public int GreaterThanUTCInMinute { set; get; }
        public string Name { set; get; }
        public string Id { set; get; }
        public string NickName { get; set; }
        public string[] Roles { get; set; }
        public string Encoder() => JsonConvert.SerializeObject(this);
        public static Cookie Decoder(string userDataStr)
        {
            if (string.IsNullOrWhiteSpace(userDataStr))
            {
                return null;
            }
            Cookie ud = JsonConvert.DeserializeObject<Cookie>(userDataStr);
            return ud;
        }
    }
}
