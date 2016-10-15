using System;
using System.Collections.Generic;
using HandyWork.Model;
using System.Security.Claims;
using HandyWork.ViewModel.Web;
using Microsoft.AspNet.Identity;
using HandyWork.UIBusiness.Enums;
using System.Web;
using Microsoft.Owin.Security;

namespace HandyWork.UIBusiness.Manager
{
    public partial class OwinManager : BaseManager
    {
        public OwinManager(UnitOfManager unitOfManager) : base(unitOfManager)
        {
        }

        public void Create(OwnViewModel user, string password)
        {
            var entity = GetAuthUserFromOwnViewModel(user);
            entity.SecurityStamp = Guid.NewGuid().ToString();
            entity.PasswordHash = new PasswordHasher().HashPassword(password);
            UnitOfWork.Add(entity);
            UnitOfWork.SaveChanges();
        }

        /// <summary>
        /// Attempts to sign in the specified <paramref name="userName"/> and <paramref name="password"/>
        /// </summary>
        /// <param name="email">The user name to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>The task object representing  the <see name="SignInResult"/>
        /// for the sign-in attempt.</returns>
        public SignInResult PasswordSignIn(string email, string password,
            bool isPersistent, bool shouldLockout=true)
        {
            var entity = UnitOfWork.UserRepository.FindByEmail(email);
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
                            entity.LockoutEndDateUtc = DateTime.UtcNow.AddMinutes(5);
                            entity.AccessFailedCount++;
                            UnitOfWork.SaveChanges();
                        }
                        return SignInResult.PasswordError;
                    case PasswordVerificationResult.Success:
                        entity.LockoutEndDateUtc = null;
                        entity.AccessFailedCount = 0;
                        UnitOfWork.SaveChanges();
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, "-1"));
                        claims.Add(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "HandyWork"));
                        claims.Add(new Claim(ClaimTypes.Name, "cheng.zhang"));
                        //claims.Add(new Claim(ClaimTypes.Role, "op"));
                        //claims.Add(new Claim(ClaimTypes.Role, "pse"));
                        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                        var authenticationProperties = new AuthenticationProperties { IsPersistent = isPersistent };
                        Request.GetOwinContext().Authentication.SignIn(authenticationProperties, identity);
                        
                        var u = HttpContext.Current.User;
                        var s = u.Identity.Name;
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

        public void SignOut()
        {
            Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
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

        private AuthUser GetAuthUserFromOwnViewModel(OwnViewModel user)
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

        private OwnViewModel GetOwnViewModelFromAuthUser(AuthUser entity)
        {
            return new OwnViewModel
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
