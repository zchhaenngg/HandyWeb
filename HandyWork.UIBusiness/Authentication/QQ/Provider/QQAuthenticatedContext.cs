﻿using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace HandyWork.UIBusiness.Authentication.QQ.Provider
{
    public class QQAuthenticatedContext : BaseContext
    {
        public QQAuthenticatedContext(IOwinContext context, string openId, JObject user, string accessToken) : base(context)
        {
            IDictionary<string, JToken> userAsDictionary = user;
            User = user;
            AccessToken = accessToken;
            Id = openId;
            Name = PropertyValueIfExists("nickname", userAsDictionary);
        }

        public JObject User { get; private set; }

        public string AccessToken { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public ClaimsIdentity Identity { get; set; }

        public AuthenticationProperties Properties { get; set; }

        private static string PropertyValueIfExists(string property, IDictionary<string, JToken> dictionary)
        {
            return dictionary.ContainsKey(property) ? dictionary[property].ToString() : null;
        }
    }
}
