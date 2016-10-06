using HandyWork.UIBusiness.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Model;
using System.Security.Claims;
using HandyWork.ViewModel.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using HandyWork.UIBusiness.Enums;

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
        /// Attempts to sign in the specified <paramref name="userName"/> and <paramref name="password"/> combination
        /// as an asynchronous operation.
        /// </summary>
        /// <param name="userName">The user name to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>The task object representing the asynchronous operation containing the <see name="SignInResult"/>
        /// for the sign-in attempt.</returns>
        public SignInResult PasswordSignInAsync(string userName, string password,
            bool isPersistent, bool shouldLockout=true)
        {
            var entity = UnitOfWork.UserRepository.FindByUserName(userName);
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
