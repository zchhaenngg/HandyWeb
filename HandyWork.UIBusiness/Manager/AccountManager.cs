using HandyWork.Model;
using HandyWork.Common.Ext;
using HandyWork.UIBusiness.Enums;
using HandyWork.UIBusiness.Query;
using HandyWork.UIBusiness.Utility;
using HandyWork.UIBusiness.ViewModel.PCWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common;
using HandyWork.Common.Model;
using System.Web.Security;
using System.Web;
using HandyWork.DAL;

namespace HandyWork.UIBusiness.Manager
{
    public class AccountManager : BaseManager
    {
        public AccountManager(ManagerStore store)
            : base(store)
        {
        }


        #region 业务-用户
        public SignInResult SignIn(string userName, string password)
        {
            User user = Store.UserRepository.FindByUserName(userName);
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
                    string encryptPassword = Algorithm.EncryptPassword(password);
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
                        Store.UserRepository.Update(user);
                        Store.SaveChanges();//此处需要将由密码输入错误记录到数据库。
                        return SignInResult.PasswordError;
                    }
                    else
                    {
                        #region 写入Cookie
                        UserCookieData cookieData = new UserCookieData()
                        {
                            Id = user.Id,
                            Name = user.UserName,
                            RealName = user.RealName,
                            Roles = null
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
                        Store.UserRepository.Update(user);
                        Store.SaveChanges();//密码正确，清空错误次数.
                        return SignInResult.Success;
                    }
                }
            }
        }

        public void Register(RegisterViewModel model)
        {
            User user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Password = Algorithm.EncryptPassword(model.Password),
                RealName = model.RealName,
                Phone = model.Phone,
                Email = model.Email,
                IsDomain = model.IsDomain,
                IsValid = true,
                IsLocked = false,
            };
            Store.UserRepository.Add(user);
            Store.SaveChanges();
        }

        public void UpdateUser(UpdateUserViewModel model)
        {
            User user = Store.UserRepository.Find(model.Id);
            user.UserName = model.UserName;
            user.RealName = model.RealName;
            user.Phone = model.Phone;
            user.Email = model.Email;
            user.IsDomain = model.IsDomain;
            Store.UserRepository.Update(user);
            Store.SaveChanges();
        }

        public void ResetPassword(ResetPasswordViewModel model)
        {
            User user = Store.UserRepository.FindByUserName(model.UserName);
            if (user == null)
            {
                Store.ErrorInfos.Add(Errors.InvalidUserName);
            }
            else
            {
                user.Password = Algorithm.EncryptPassword(model.Password);
                Store.UserRepository.Update(user);
                Store.SaveChanges();
            }
        }

        public UpdateUserViewModel GetUpdateUserViewModel(string userId)
        {
            User user = Store.UserRepository.Find(userId);
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
            User user = Store.UserRepository.Find(userId);
            user.IsValid = !user.IsValid;
            Store.UserRepository.Update(user);
            Store.SaveChanges();
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
            User user = Store.UserRepository.Find(userId);
            if (!user.IsLocked)
            {
                throw new Exception("该用户已处于非锁定状态");
            }
            user.IsLocked = false;
            Store.UserRepository.Update(user);
            Store.SaveChanges();
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
            Store.AuthPermissionRepository.Add(permission);
            Store.SaveChanges();
        }
        public void EditPermission(PermissionViewModel model)
        {
            AuthPermission permission = Store.AuthPermissionRepository.Find(model.Id);
            permission.Code = model.Code.Trim();
            permission.Name = model.Name.Trim();
            permission.Description = model.Description.Trim();
            Store.AuthPermissionRepository.Update(permission);
            Store.SaveChanges();
        }
        public PermissionViewModel GetPermissionViewModel(string id)
        {
            AuthPermission permission = Store.AuthPermissionRepository.Find(id);
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
            Store.AuthRoleRepository.Add(role);
            Store.SaveChanges();
        }
        public void EditRole(RoleViewModel model)
        {
            AuthRole role = Store.AuthRoleRepository.Find(model.Id);
            role.Name = model.Name.Trim();
            role.Description = model.Description.Trim();
            Store.AuthRoleRepository.Update(role);
            Store.SaveChanges();
        }
        public Tuple<bool, string> DeleteRole(string id)
        {
            Store.AuthRoleRepository.Remove(id);
            return new Tuple<bool, string>(true, "角色删除成功");
        }
        public RoleViewModel GetRoleViewModel(string id)
        {
            AuthRole role = Store.AuthRoleRepository.Find(id);
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
            EasyuiUtil.FillPageQueryFromRequest(Request, query);
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
        internal Expression<Func<User, bool>> GetExpression4User(UserQuery query)
        {
            Expression<Func<User, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(query.UserNameLike))
            {
                expression = expression.And(o => o.UserName.Contains(query.UserNameLike));
            }
            if (!string.IsNullOrWhiteSpace(query.RealNameLike))
            {
                expression = expression.And(o => o.RealName.Contains(query.RealNameLike));
            }
            if (query.IsValid != null)
            {
                expression = expression.And(o => o.IsValid == query.IsValid);
            }
            if (query.IsLocked != null)
            {
                expression = expression.And(o => o.IsLocked == query.IsLocked);
            }
            return expression;
        }
        internal Tuple<List<User>, int> GetPage4User()
        {
            UserQuery query = GetUserQuery();
            Expression<Func<User, bool>> expression = GetExpression4User(query);
            return Store.UserRepository.GetPage(query, expression);
        }
        public Tuple<List<UserViewModel>, int> GetPage4UserViewModel()
        {
            Tuple<List<User>, int> tuple = GetPage4User();

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
            User user = Store.UserRepository.Find(userId);
            var whereResult = string.IsNullOrWhiteSpace(permissionNameLike) ? user.AuthPermission :
                user.AuthPermission.Where(o => o.Name.Contains(permissionNameLike));
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
            var allPermissions = Store.AuthPermissionRepository.Source.ToList();
            var userPermissions = Store.UserRepository.Find(userId).AuthPermission.ToList();
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
            User user = Store.UserRepository.Find(userId);
            var permission = Store.AuthPermissionRepository.Find(permissionId);
            user.AuthPermission.Add(permission);
            Store.SaveChanges();
        }
        public void RemoveUserPermission(string userId, string permissionId)
        {
            User user = Store.UserRepository.Find(userId);
            var permission = Store.AuthPermissionRepository.Find(permissionId);
            user.AuthPermission.Remove(permission);
            Store.SaveChanges();
        }

        public List<RoleViewModel> GetRoleViewModelsByUserId(string userId)
        {
            User user = Store.UserRepository.Find(userId);

            List<RoleViewModel> list = user.AuthRole.ToList().Select(o => new RoleViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description
            }).ToList();

            return list;
        }
        public List<RoleViewModel> GetRoleViewModels4AddByUserId(string userId)
        {
            var allRoles = Store.AuthRoleRepository.Source.ToList();
            var userRoles = Store.UserRepository.Find(userId).AuthRole.ToList();
            List<RoleViewModel> list = allRoles.Where(o => !userRoles.Contains(o)).Select(o => new RoleViewModel
            {
                Id = o.Id,
                Name = o.Name,
                Description = o.Description
            }).ToList();
            return list;
        }
        public void AddUserRole(string userId, string roleId)
        {
            User user = Store.UserRepository.Find(userId);
            var role = Store.AuthRoleRepository.Find(roleId);
            user.AuthRole.Add(role);
            Store.SaveChanges();
        }
        public void RemoveUserRole(string userId, string roleId)
        {
            User user = Store.UserRepository.Find(userId);
            var role = Store.AuthRoleRepository.Find(roleId);
            user.AuthRole.Remove(role);
            Store.SaveChanges();
        }
        #endregion

        #region Permission
        internal AuthPermissionQuery GetAuthPermissionQuery()
        {
            AuthPermissionQuery query = new AuthPermissionQuery();
            EasyuiUtil.FillPageQueryFromRequest(Request, query);
            query.NameLike = string.IsNullOrWhiteSpace(Request["NameLike"]) ? null : Request["NameLike"].Trim();
            query.CodeLike = string.IsNullOrWhiteSpace(Request["CodeLike"]) ? null : Request["CodeLike"].Trim();
            return query;
        }
        internal Expression<Func<AuthPermission, bool>> GetExpression4User(AuthPermissionQuery query)
        {
            Expression<Func<AuthPermission, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(query.NameLike))
            {
                expression = expression.And(o => o.Name.Contains(query.NameLike));
            }
            if (!string.IsNullOrWhiteSpace(query.CodeLike))
            {
                expression = expression.And(o => o.Code.Contains(query.CodeLike));
            }
            return expression;
        }
        internal Tuple<List<AuthPermission>, int> GetPage4AuthPermission()
        {
            AuthPermissionQuery query = GetAuthPermissionQuery();
            Expression<Func<AuthPermission, bool>> expression = GetExpression4User(query);
            return Store.AuthPermissionRepository.GetPage(query, expression);
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
        public AuthRoleQuery GetAuthRoleQuery()
        {
            AuthRoleQuery query = new AuthRoleQuery();
            EasyuiUtil.FillPageQueryFromRequest(Request, query);
            query.NameLike = string.IsNullOrWhiteSpace(Request["NameLike"]) ? null : Request["NameLike"].Trim();
            return query;
        }
        public Expression<Func<AuthRole, bool>> GetExpression4AuthRole(AuthRoleQuery query)
        {
            Expression<Func<AuthRole, bool>> expression = null;
            if (!string.IsNullOrWhiteSpace(query.NameLike))
            {
                expression = expression.And(o => o.Name.Contains(query.NameLike));
            }
            return expression;
        }
        public Tuple<List<AuthRole>, int> GetPage4AuthRole()
        {
            AuthRoleQuery query = GetAuthRoleQuery();
            Expression<Func<AuthRole, bool>> expression = GetExpression4AuthRole(query);
            return Store.AuthRoleRepository.GetPage(query, expression);
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
            AuthRole role = Store.AuthRoleRepository.Find(roleId);
            var whereResult = string.IsNullOrWhiteSpace(permissionNameLike) ? role.AuthPermission
                : role.AuthPermission.Where(o => o.Name.Contains(permissionNameLike));

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
            var allPermissions = Store.AuthPermissionRepository.Source.ToList();
            var rolePermissions = Store.AuthRoleRepository.Find(roleId).AuthPermission.ToList();
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
            var role = Store.AuthRoleRepository.Find(roleId);
            var permission = Store.AuthPermissionRepository.Find(permissionId);
            role.AuthPermission.Add(permission);
            Store.SaveChanges();
        }
        public void RemoveRolePermission(string roleId, string permissionId)
        {
            var role = Store.AuthRoleRepository.Find(roleId);
            var permission = Store.AuthPermissionRepository.Find(permissionId);
            role.AuthPermission.Remove(permission);
            Store.SaveChanges();
        }
        #endregion
        #endregion
    }
}
