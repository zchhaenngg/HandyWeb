using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Authentication.QQ.Provider
{
    public interface IQQAuthenticationProvider
    {
        Task Authenticated(QQAuthenticatedContext context);

        Task ReturnEndpoint(QQReturnEndpointContext context);
    }
}
