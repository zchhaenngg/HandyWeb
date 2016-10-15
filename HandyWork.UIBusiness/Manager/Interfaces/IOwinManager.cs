using HandyWork.Model;
using HandyWork.UIBusiness.Enums;
using HandyWork.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Manager.Interfaces
{
    public interface IOwinManager
    {
        /// <summary>
        /// 获取用户所有声明
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IList<Claim> GetClaims(AuthUser entity);
        
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isPersistent"></param>
        void SignIn(AuthUser entity, bool isPersistent);

        /// <summary>
        /// 用户登出
        /// </summary>
        void SignOut();

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="usernameOrEmail"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="shouldLockout"></param>
        /// <returns></returns>
        SignInResult SignIn(string usernameOrEmail, string password, bool isPersistent, bool shouldLockout = true);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        void Register(OwinViewModel user, string password);
    }
}
