using HandyWork.DAL;
using HandyWork.Model.Entity;
using HandyWork.Services.Service.Interfaces;
using HandyWork.ViewModel.Common;
using HandyWork.ViewModel.Web;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HandyWork.Common.Extensions;

namespace HandyWork.Services.Service
{
    public static class MyOwinConfig
    {
        public static string CookieName
        {
            get
            {
                var str = ConfigurationManager.AppSettings["Cookie"];
                return string.IsNullOrWhiteSpace(str) ? "HandyWork.Cookie" : str;
            }
        }

        public static string IdentityProvider
        {
            get
            {
                var str = ConfigurationManager.AppSettings["IdentityProvider"];
                return string.IsNullOrWhiteSpace(str) ? "HandyWork.IdentityProvider" : str;
            }
        }

        public static string AuthenticationType
        {
            get
            {
                var str = ConfigurationManager.AppSettings["AuthenticationType"];
                return string.IsNullOrWhiteSpace(str) ? "HandyWork.AuthenticationType" : str;
            }
        }

        public static string ApplicationVersion
        {
            get
            {
                var str = ConfigurationManager.AppSettings["ApplicationVersion"];
                return string.IsNullOrWhiteSpace(str) ? "1.0" : str;
            }
        }

        /// <summary>
        /// 用户密码输错账号锁定后，下次可以尝试登陆时间，默认30分钟
        /// </summary>
        public static TimeSpan LockoutTimeSpan
        {
            get
            {
                return TimeSpan.FromMinutes(30);
            }
        }
    }
    public partial class OWinService: IOWinService
    {
        protected HttpRequest Request => HttpContext.Current.Request;
        //public void Register(OwinViewModel user, string password)
        //{
        //    var entity = new hy_user
        //    {
        //        id = Guid.NewGuid().ToString(),
        //        access_failed_times = 0,
        //        is_locked = false,
        //       // password_hash = user.PasswordHash,
        //       // security_stamp = user.SecurityStamp,
        //        phone_number = user.PhoneNumber,
        //        phone_number_confirmed = false,
        //        two_factor_enabled = true,
        //        email_confirmed = false,
        //        user_name = user.UserName,
        //        nick_name = user.NickName,
        //        email = user.Email,
        //        is_valid = true
        //    };
        //    entity.security_stamp = Guid.NewGuid().ToString();
        //    entity.password_hash = new PasswordHasher().HashPassword(password);
        //    using (var context = new HyContext(LoginId))
        //    {
        //        context.Add(entity);
        //        context.SaveChanges();
        //    }
        //}

        /// <summary>
        /// Attempts to sign in the specified <paramref name="userName"/> and <paramref name="password"/>
        /// </summary>
        /// <param name="usernameOrEmail">The email to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// /// <param name="GreaterThanUTCInMinute">utc time to browser time's interval time</param>
        /// <param name="shouldLockout">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>The task object representing  the <see name="SignInResult"/>
        /// for the sign-in attempt.</returns>
        public SignInResult SignIn(string usernameOrEmail, string password, bool isPersistent, int GreaterThanUTCInMinute, bool shouldLockout = true)
        {
            using (var context = new HyContext(null))
            {
                var entity = context.hy_users.FirstOrDefault(o => o.user_name == usernameOrEmail)
                    ?? context.hy_users.FirstOrDefault(o => o.email == usernameOrEmail);
                if (entity == null)
                {
                    return SignInResult.UserNameError;
                }
                if (!entity.is_valid)
                {
                    return SignInResult.Invalid;
                }
                if (entity.is_locked)
                {
                    return SignInResult.LockedOut;
                }
                if (entity.user_name != usernameOrEmail && !entity.two_factor_enabled)
                {
                    return SignInResult.Invalid;
                }
                else
                {
                    var result = VerifyHashedPassword(entity, password);
                    switch (result)
                    {
                        case PasswordVerificationResult.Failed:
                            if (shouldLockout)
                            {
                                entity.access_failed_times++;
                                if (entity.access_failed_times > 3)
                                {
                                    entity.unlock_time = DateTime.UtcNow.Add(MyOwinConfig.LockoutTimeSpan);
                                }
                                context.SaveChanges();
                            }
                            return SignInResult.PasswordError;
                        case PasswordVerificationResult.Success:
                            entity.unlock_time = null;
                            entity.access_failed_times = 0;
                            SignIn(entity, isPersistent, GreaterThanUTCInMinute);
                            context.SaveChanges();
                            return SignInResult.Success;
                        case PasswordVerificationResult.SuccessRehashNeeded:
                            entity.unlock_time = null;
                            entity.access_failed_times = 0;
                            context.SaveChanges();
                            return SignInResult.SuccessRehashNeeded;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
        
        public void SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut(MyOwinConfig.AuthenticationType);
        }
    }
    public partial class OWinService
    {
        //私有方法
        private PasswordVerificationResult VerifyHashedPassword(hy_user entity, string password)
        {
            if (entity.password_hash == null)
            {
                return PasswordVerificationResult.Failed;
            }
            else
            {
                return new PasswordHasher().VerifyHashedPassword(entity.password_hash, password);
            }
        }
        private void SignIn(hy_user entity, bool isPersistent, int GreaterThanUTCInMinute)
        {
            var claims = GetClaims(entity, GreaterThanUTCInMinute);
            var identity = new ClaimsIdentity(claims, MyOwinConfig.AuthenticationType);
            var authenticationProperties = new AuthenticationProperties { IsPersistent = isPersistent };
            Request.GetOwinContext().Authentication.SignIn(authenticationProperties, identity);
        }
        private IList<Claim> GetClaims(hy_user entity, int GreaterThanUTCInMinute)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, entity.id));
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", MyOwinConfig.IdentityProvider));
            claims.Add(new Claim(ClaimTypes.Name, entity.user_name));
            //登陆状态需要检查Claim版本信息，如果版本号不正确则强制用户重新登陆
            claims.Add(new Claim(ClaimTypes.Version, MyOwinConfig.ApplicationVersion));
            claims.Add(new Claim("NickName", entity.nick_name));
            claims.Add(new Claim("EmailAddress", entity.email));
            claims.Add(new Claim("GreaterThanUTCInMinute", GreaterThanUTCInMinute.ToString(), "int"));
            return claims;
        }
    }
}
