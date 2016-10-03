using HandyWork.Model;
using HandyWork.Common.Extensions;
using HandyWork.UIBusiness.Enums;
using HandyWork.UIBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using HandyWork.Common.Authority;
using System.Web.Security;
using System.Web;
using HandyWork.DAL;
using HandyWork.UIBusiness.Manager.Interfaces;
using HandyWork.ViewModel.PCWeb;
using HandyWork.ViewModel.PCWeb.Query;
using HandyWork.Common.Exceptions;
using HandyWork.Localization;

namespace HandyWork.UIBusiness.Manager
{
    public class AccountManager : BaseManager, IAccountManager
    {
        public AccountManager(UnitOfManager unitOfManager) 
            : base(unitOfManager)
        {
        }

        #region 业务-用户
        public SignInResult SignIn(string userName, string password, int GreaterThanUTCInMinute)
        {
            AuthUser user = UnitOfWork.UserRepository.FindByUserName(userName);
            if (user == null)
            {
                return SignInResult.UserNameError;
            }
            else
            {
                if (user.IsLocked)
                {
                    return SignInResult.LockedOut;
                }
                else if (!user.IsValid)
                {
                    return SignInResult.Invalid;
                }
                //else if (user.IsDomain)
                //{

                //}
                else
                {
                    string encryptPassword = AlgorithmUtility.EncryptPassword(password);
                    if (!encryptPassword.Equals(user.Password))
                    {
                        if (user.LastLoginFailedTime.IsToday())
                        {
                            user.LoginFailedCount++;
                            user.LastLoginFailedTime = DateTime.Now;
                            if (user.LoginFailedCount == 3)
                            {
                                user.IsLocked = true;
                            }
                        }
                        else
                        {
                            user.LoginFailedCount = 1;
                            user.LastLoginFailedTime = DateTime.Now;
                        }
                        UnitOfWork.SaveChanges();//此处需要将由密码输入错误记录到数据库。
                        return SignInResult.PasswordError;
                    }
                    else
                    {
                        #region 写入Cookie
                        Cookie cookieData = new Cookie()
                        {
                            Id = user.Id,
                            Name = user.UserName,
                            RealName = user.RealName,
                            GreaterThanUTCInMinute = GreaterThanUTCInMinute
                        };
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, cookieData.Name, DateTime.Now, DateTime.Now.AddDays(365), false, cookieData.Encoder());
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName)
                        {
                            Value = FormsAuthentication.Encrypt(ticket),
                            //Expires = DateTime.Now.AddDays(365)
                        };
                        HttpContext.Current.Response.Cookies.Add(cookie);
                        #endregion

                        user.LoginFailedCount = 0;
                        UnitOfWork.SaveChanges();//密码正确，清空错误次数.
                        return SignInResult.Success;
                    }
                }
            }
        }

        public void Register(RegisterViewModel model)
        {
            AuthUser user = new AuthUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Password = AlgorithmUtility.EncryptPassword(model.Password),
                RealName = model.RealName,
                Phone = model.Phone,
                Email = model.Email,
                IsDomain = model.IsDomain,
                IsValid = true,
                IsLocked = false,
            };
            UnitOfWork.Add(user);
            UnitOfWork.SaveChanges();
        }

        public void UpdateUser(UpdateUserViewModel model)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(model.Id);
            user.UserName = model.UserName;
            user.RealName = model.RealName;
            user.Phone = model.Phone;
            user.Email = model.Email;
            user.IsDomain = model.IsDomain;
            UnitOfWork.SaveChanges();
        }

        public void ResetPassword(ResetPasswordViewModel model)
        {
            AuthUser user = UnitOfWork.UserRepository.FindByUserName(model.UserName);
            if (user == null)
            {
                throw new ErrorException(LocalizedResource.NOTEXIST_USERNAME);
            }
            else
            {
                user.Password = AlgorithmUtility.EncryptPassword(model.Password);
                UnitOfWork.SaveChanges();
            }
        }

        public UpdateUserViewModel GetUpdateUserViewModel(string userId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            UpdateUserViewModel model = new UpdateUserViewModel
            {
                UserName = user.UserName,
                RealName = user.RealName,
                Phone = user.Phone,
                Email = user.Email,
                IsDomain = user.IsDomain,
                Id = userId
            };
            return model;
        }

        public string SetUserValid(string userId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            user.IsValid = !user.IsValid;
            UnitOfWork.SaveChanges();
            if (user.IsValid)
            {
                return "用户已成功被启用";
            }
            else
            {
                return "用户已成功被设置为失效状态";
            }
        }

        public void SetUnlocked4User(string userId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            if (!user.IsLocked)
            {
                throw new Exception("该用户已处于非锁定状态");
            }
            user.IsLocked = false;
            UnitOfWork.SaveChanges();
        }

        public string[] GetAllPermissions4Code(string userId)
        {
            return UnitOfWork.UserRepository.GetAllPermissions(userId).Select(o=>o.Code).ToArray();
        }
        #endregion

        #region 业务-权限
        public void CreatePermission(PermissionViewModel model)
        {
            AuthPermission permission = new AuthPermission
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name.Trim(),
                Code = model.Code.Trim(),
                Description = model.Description,
            };
            UnitOfWork.Add(permission);
            UnitOfWork.SaveChanges();
        }
        public void EditPermission(PermissionViewModel model)
        {
            AuthPermission permission = UnitOfWork.PermissionRepository.Find(model.Id);
            permission.Code = model.Code.Trim();
            permission.Name = model.Name.Trim();
            permission.Description = model.Description.Trim();
            UnitOfWork.SaveChanges();
        }
        public PermissionViewModel GetPermissionViewModel(string id)
        {
            AuthPermission permission = UnitOfWork.PermissionRepository.Find(id);
            PermissionViewModel vm = new PermissionViewModel
            {
                Id = permission.Id,
                Name = permission.Name,
                Code = permission.Code,
                Description = permission.Description,
            };
            return vm;
        }
        #endregion

        #region 业务-角色
        public void CreateRole(RoleViewModel model)
        {
            AuthRole role = new AuthRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = model.Name.Trim(),
                Description = model.Description,
            };
            UnitOfWork.Add(role);
            UnitOfWork.SaveChanges();
        }
        public void EditRole(RoleViewModel model)
        {
            AuthRole role = UnitOfWork.RoleRepository.Find(model.Id);
            role.Name = model.Name.Trim();
            role.Description = model.Description.Trim();
            UnitOfWork.SaveChanges();
        }
        public Tuple<bool, string> DeleteRole(string id)
        {
            var entity = UnitOfWork.RoleRepository.Find(id);
            UnitOfWork.Remove(entity);
            return new Tuple<bool, string>(true, "角色删除成功");
        }
        public RoleViewModel GetRoleViewModel(string id)
        {
            AuthRole role = UnitOfWork.RoleRepository.Find(id);
            RoleViewModel vm = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
            };
            return vm;
        }
        #endregion

        #region 分页查询
        #region User
        internal UserQuery GetUserQuery()
        {
            UserQuery query = new UserQuery();
            EasyuiUtility.FillPageQueryFromRequest(Request, query);
            query.UserNameLike = string.IsNullOrWhiteSpace(Request["UserNameLike"]) ? null : Request["UserNameLike"].Trim();
            query.RealNameLike = string.IsNullOrWhiteSpace(Request["RealNameLike"]) ? null : Request["RealNameLike"].Trim();
            if (!string.IsNullOrWhiteSpace(Request["IsValid"]))
            {
                query.IsValid = bool.Parse(Request["IsValid"]);
            }
            if (!string.IsNullOrWhiteSpace(Request["IsLocked"]))
            {
                query.IsLocked = bool.Parse(Request["IsLocked"]);
            }
            return query;
        }
        internal Tuple<List<AuthUser>, int> GetPage4User()
        {
            int iTotal;
            var query = GetUserQuery();
            var list = UnitOfWork.AsNoTracking<AuthUser>().GetPage(query, out iTotal).ToList();
            return new Tuple<List<AuthUser>, int>(list, iTotal);

        }
        public Tuple<List<UserViewModel>, int> GetPage4UserViewModel()
        {
            Tuple<List<AuthUser>, int> tuple = GetPage4User();

            var list = tuple.Item1.Select(o => new UserViewModel
            {
                Id = o.Id,
                RealName = o.RealName,
                UserName = o.UserName,
                Email = o.Email,
                IsDomain = o.IsDomain,
                IsValid = o.IsValid,
                IsLocked = o.IsLocked
            }).ToList();

            return new Tuple<List<UserViewModel>, int>(list, tuple.Item2);
        }

        public List<PermissionViewModel> GetPermissionViewModelsByUserId(string userId, string permissionNameLike)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            var whereResult = string.IsNullOrWhiteSpace(permissionNameLike) ? user.Permissions :
                user.Permissions.Where(o => o.Name.Contains(permissionNameLike));
            return whereResult.ToList().Select(o => new PermissionViewModel
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Description = o.Description
            }).ToList();
        }
        public List<PermissionViewModel> GetPermissionViewModels4AddByUserId(string userId, string permissionNameLike)
        {
            var allPermissions = UnitOfWork.PermissionRepository.Source.ToList();
            var userPermissions = UnitOfWork.UserRepository.Find(userId).Permissions.ToList();
            var whereResult = string.IsNullOrWhiteSpace(permissionNameLike) ? allPermissions.Where(o => !userPermissions.Contains(o)) :
               allPermissions.Where(o => !userPermissions.Contains(o) && o.Name.Contains(permissionNameLike));
            var list = whereResult.Select(o => new PermissionViewModel
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Description = o.Description
            }).ToList();
            return list;
        }
        public void AddUserPermission(string userId, string permissionId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            var permission = UnitOfWork.PermissionRepository.Find(permissionId);
            user.Permissions.Add(permission);
            UnitOfWork.SaveChanges();
        }
        public void RemoveUserPermission(string userId, string permissionId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            var permission = UnitOfWork.PermissionRepository.Find(permissionId);
            user.Permissions.Remove(permission);
            UnitOfWork.SaveChanges();
        }

        public List<RoleViewModel> GetRoleViewModelsByUserId(string userId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);

            List<RoleViewModel> list = user.Roles.ToList().Select(o => new RoleViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description
            }).ToList();

            return list;
        }
        public List<RoleViewModel> GetRoleViewModels4AddByUserId(string userId)
        {
            var userRoles = UnitOfWork.UserRepository.Find(userId).Roles.ToList();
            var list = UnitOfWork.RoleRepository.Source.Where(o => !userRoles.Contains(o)).Select(o => new RoleViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description
            }).ToList();
            return list;
        }
        public void AddUserRole(string userId, string roleId)
        {
            var user = UnitOfWork.UserRepository.Find(userId);
            var role = UnitOfWork.RoleRepository.Find(roleId);
            user.Roles.Add(role);
            UnitOfWork.SaveChanges();
        }
        public void RemoveUserRole(string userId, string roleId)
        {
            AuthUser user = UnitOfWork.UserRepository.Find(userId);
            var role = UnitOfWork.RoleRepository.Find(roleId);
            user.Roles.Remove(role);
            UnitOfWork.SaveChanges();
        }
        #endregion

        #region Permission
        internal AuthPermissionQuery GetAuthPermissionQuery()
        {
            var query = new AuthPermissionQuery();
            EasyuiUtility.FillPageQueryFromRequest(Request, query);
            query.NameLike = string.IsNullOrWhiteSpace(Request["NameLike"]) ? null : Request["NameLike"].Trim();
            query.CodeLike = string.IsNullOrWhiteSpace(Request["CodeLike"]) ? null : Request["CodeLike"].Trim();
            return query;
        }
        internal Tuple<List<AuthPermission>, int> GetPage4AuthPermission()
        {
            int iTotal;
            var query = GetAuthPermissionQuery();
            var list = UnitOfWork.AsNoTracking<AuthPermission>().GetPage(query, out iTotal).ToList();
            return new Tuple<List<AuthPermission>, int>(list, iTotal);
        }

        public Tuple<List<PermissionViewModel>, int> GetPage4PermissionViewModel()
        {
            Tuple<List<AuthPermission>, int> tuple = GetPage4AuthPermission();
            var list = tuple.Item1.Select(o => new PermissionViewModel
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Description = o.Description
            }).ToList();

            return new Tuple<List<PermissionViewModel>, int>(list, tuple.Item2);
        }
        #endregion 

        #region Role
        internal AuthRoleQuery GetAuthRoleQuery()
        {
            AuthRoleQuery query = new AuthRoleQuery();
            EasyuiUtility.FillPageQueryFromRequest(Request, query);
            query.NameLike = string.IsNullOrWhiteSpace(Request["NameLike"]) ? null : Request["NameLike"].Trim();
            return query;
        }

        internal Tuple<List<AuthRole>, int> GetPage4AuthRole()
        {
            int iTotal;
            var query = GetAuthRoleQuery();
            var list = UnitOfWork.AsNoTracking<AuthRole>().GetPage(query, out iTotal).ToList();
            return new Tuple<List<AuthRole>, int>(list, iTotal);
        }
        public Tuple<List<RoleViewModel>, int> GetPage4RoleViewModel()
        {
            Tuple<List<AuthRole>, int> tuple = GetPage4AuthRole();
            var list = tuple.Item1.Select(o => new RoleViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description
            }).ToList();

            return new Tuple<List<RoleViewModel>, int>(list, tuple.Item2);
        }

        public List<PermissionViewModel> GetPermissionViewModelsByRoleId(string roleId, string permissionNameLike)
        {
            AuthRole role = UnitOfWork.RoleRepository.Find(roleId);
            var whereResult = string.IsNullOrWhiteSpace(permissionNameLike) ? role.AuthPermissions
                : role.AuthPermissions.Where(o => o.Name.Contains(permissionNameLike));

            return whereResult.ToList().Select(o => new PermissionViewModel
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Description = o.Description
            }).ToList();
        }
        public List<PermissionViewModel> GetPermissionViewModels4AddByRoleId(string roleId, string permissionNameLike)
        {
            var allPermissions = UnitOfWork.PermissionRepository.Source.ToList();
            var rolePermissions = UnitOfWork.RoleRepository.Find(roleId).AuthPermissions.ToList();
            var whereResult = string.IsNullOrWhiteSpace(permissionNameLike) ? allPermissions.Where(o => !rolePermissions.Contains(o)) :
                allPermissions.Where(o => !rolePermissions.Contains(o) && o.Name.Contains(permissionNameLike));

            var list = whereResult.Select(o => new PermissionViewModel
            {
                Id = o.Id,
                Code = o.Code,
                Name = o.Name,
                Description = o.Description
            }).ToList();
            return list;
        }
        public void AddRolePermission(string roleId, string permissionId)
        {
            var role = UnitOfWork.RoleRepository.Find(roleId);
            var permission = UnitOfWork.PermissionRepository.Find(permissionId);
            role.AuthPermissions.Add(permission);
            UnitOfWork.SaveChanges();
        }
        public void RemoveRolePermission(string roleId, string permissionId)
        {
            var role = UnitOfWork.RoleRepository.Find(roleId);
            var permission = UnitOfWork.PermissionRepository.Find(permissionId);
            role.AuthPermissions.Remove(permission);
            UnitOfWork.SaveChanges();
        }
        #endregion
        #endregion
    }
}
