using HandyWork.ViewModel.Common;
using HandyWork.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Services.Service.Interfaces
{
    /// <summary>
    /// 提供注册、登陆等认证
    /// </summary>
    public interface IOWinService
    {
        void Register(OwinViewModel user, string password);
        SignInResult SignIn(string usernameOrEmail, string password, bool isPersistent, bool shouldLockout = true);
        void SignOut();
    }
}
