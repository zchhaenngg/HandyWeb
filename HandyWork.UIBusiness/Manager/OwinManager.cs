using System;
using System.Collections.Generic;
using HandyWork.Model;
using System.Security.Claims;
using HandyWork.ViewModel.Web;
using Microsoft.AspNet.Identity;
using HandyWork.UIBusiness.Enums;
using System.Web;
using Microsoft.Owin.Security;
using System.Configuration;
using HandyWork.UIBusiness.Manager.Interfaces;

namespace HandyWork.UIBusiness.Manager
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
        /// 用户密码输错账号锁定后，下次可以尝试登陆时间，默认5分钟
        /// </summary>
        public static TimeSpan LockoutTimeSpan
        {
            get
            {
                return TimeSpan.FromMinutes(5);
            }
        }
    }
    public partial class OwinManager : BaseManager, IOwinManager
    {
        public OwinManager(UnitOfManager unitOfManager) : base(unitOfManager)
        {
        }

        public void Register(OwinViewModel user, string password)
        {
            var entity = GetAuthUserFromOwnViewModel(user);
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.PasswordHash = new PasswordHasher().HashPassword(password);
            UnitOfWork.Add(entity);
            UnitOfWork.SaveChanges();
        }

        public IList<Claim> GetClaims(AuthUser entity)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, entity.Id));
            claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", MyOwinConfig.IdentityProvider));
            claims.Add(new Claim(ClaimTypes.Name, entity.UserName));
            //登陆状态需要检查Claim版本信息，如果版本号不正确则强制用户重新登陆
            claims.Add(new Claim(ClaimTypes.Version, MyOwinConfig.ApplicationVersion));
            return claims;
        }

        /// <summary>
        /// Attempts to sign in the specified <paramref name="userName"/> and <paramref name="password"/>
        /// </summary>
        /// <param name="usernameOrEmail">The email to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>The task object representing  the <see name="SignInResult"/>
        /// for the sign-in attempt.</returns>
        public SignInResult SignIn(string usernameOrEmail, string password, bool isPersistent, bool shouldLockout=true)
        {
            var entity = UnitOfWork.UserRepository.FindByUserName(usernameOrEmail) ??
                UnitOfWork.UserRepository.FindByEmail(usernameOrEmail);
            if (entity == null)
            {
                return SignInResult.UserNameError;
            }
            if (!entity.IsValid)
            {
                return SignInResult.Invalid;
            }
            if (IsLockout(entity))
            {
                return SignInResult.LockedOut;
            }
            else
            {
                var result = VerifyHashedPassword(entity, password);
                switch (result)
                {
                    case PasswordVerificationResult.Failed:
                        if (shouldLockout)
                        {
                            entity.LockoutEndDateUtc = DateTime.UtcNow.Add(MyOwinConfig.LockoutTimeSpan);
                            entity.AccessFailedCount++;
                            UnitOfWork.SaveChanges();
                        }
                        return SignInResult.PasswordError;
                    case PasswordVerificationResult.Success:
                        entity.LockoutEndDateUtc = null;
                        entity.AccessFailedCount = 0;
                        UnitOfWork.SaveChanges();
                        SignIn(entity, isPersistent);
                        return SignInResult.Success;
                    case PasswordVerificationResult.SuccessRehashNeeded:
                        entity.LockoutEndDateUtc = null;
                        entity.AccessFailedCount = 0;
                        UnitOfWork.SaveChanges();
                        return SignInResult.SuccessRehashNeeded;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public void SignIn(AuthUser entity, bool isPersistent)
        {
            var claims = GetClaims(entity);
            var identity = new ClaimsIdentity(claims, MyOwinConfig.AuthenticationType);
            var authenticationProperties = new AuthenticationProperties { IsPersistent = isPersistent };
            Request.GetOwinContext().Authentication.SignIn(authenticationProperties, identity);
        }

        public void SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut(MyOwinConfig.AuthenticationType);
        }
    }

    public partial class OwinManager
    {
        //私有方法
        private bool IsLockout(AuthUser entity)
        {
            return entity.LockoutEnabled
                     && entity.LockoutEndDateUtc != null
                     && DateTime.UtcNow < entity.LockoutEndDateUtc.Value;
        }
        private PasswordVerificationResult VerifyHashedPassword(AuthUser entity, string password)
        {
            if (entity.PasswordHash == null)
            {
                return PasswordVerificationResult.Failed;
            }
            else
            {
                return new PasswordHasher().VerifyHashedPassword(entity.PasswordHash, password);
            }
        }

        private AuthUser GetAuthUserFromOwnViewModel(OwinViewModel user)
        {
            var entity = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                AccessFailedCount = 0,
                LockoutEnabled = true,
                PasswordHash = user.PasswordHash,
                SecurityStamp = user.SecurityStamp,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = true,
                EmailConfirmed = false,
                UserName = user.UserName,
                RealName = user.RealName,
                Email = user.Email,
                IsValid = true
            };
            return entity;
        }

        private OwinViewModel GetOwnViewModelFromAuthUser(AuthUser entity)
        {
            return new OwinViewModel
            {
                Id = entity.Id,
                AccessFailedCount = entity.AccessFailedCount,
                LockoutEnabled = entity.LockoutEnabled,
                LockoutEndDateUtc = entity.LockoutEndDateUtc,
                PasswordHash = entity.PasswordHash,
                SecurityStamp = entity.SecurityStamp,
                PhoneNumber = entity.PhoneNumber,
                PhoneNumberConfirmed = entity.PhoneNumberConfirmed,
                TwoFactorEnabled = entity.TwoFactorEnabled,
                EmailConfirmed = entity.EmailConfirmed,
                UserName = entity.UserName,
                RealName = entity.RealName,
                Email = entity.Email,
                IsValid = entity.IsValid,
                CreatedById = entity.CreatedById,
                LastModifiedById = entity.LastModifiedById,
                CreatedTime = entity.CreatedTime,
                LastModifiedTime = entity.LastModifiedTime
            };
        }
    }
}
