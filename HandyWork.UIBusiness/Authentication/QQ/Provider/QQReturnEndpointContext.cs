using Microsoft.Owin.Security.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace HandyWork.UIBusiness.Authentication.QQ.Provider
{
    public class QQReturnEndpointContext : ReturnEndpointContext
    {
        public QQReturnEndpointContext(IOwinContext context, AuthenticationTicket ticket) : base(context, ticket)
        {
        }
    }
}
