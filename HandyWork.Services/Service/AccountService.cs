﻿using HandyWork.Services.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.EntityFramework.Query;
using HandyWork.ViewModel.Web;
using HandyWork.DAL;
using HandyWork.Model.Entity;
using Microsoft.AspNet.Identity;

namespace HandyWork.Services.Service
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(string loginId) : base(loginId)
        {
        }

        public void AddRolePermission(string roleId, string permissionId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_auth_roles.First(o => o.id == roleId);
                var permission = context.hy_auth_permissions.First(o => o.id == permissionId);
                if (!entity.hy_auth_permissions.Contains(permission))
                {
                    entity.hy_auth_permissions.Add(permission);
                    context.SaveChanges();
                }
            }
        }

        public void AddUserPermission(string userId, string permissionId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.First(o => o.id == userId);
                var permission = context.hy_auth_permissions.First(o => o.id == permissionId);
                if (!entity.hy_auth_permissions.Contains(permission))
                {
                    entity.hy_auth_permissions.Add(permission);
                    context.SaveChanges();
                }
            }
        }

        public void AddUserRole(string userId, string roleId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.First(o => o.id == userId);
                var role = context.hy_auth_roles.First(o => o.id == roleId);
                if (!entity.hy_auth_roles.Contains(role))
                {
                    entity.hy_auth_roles.Add(role);
                    context.SaveChanges();
                }
            }
        }

        public void CreatePermission(PermissionViewModel model)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = new hy_auth_permission
                {
                    id = Guid.NewGuid().ToString(),
                    name = model.Name,
                    code = model.Code,
                    description = model.Description
                };
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void CreateRole(RoleViewModel model)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = new hy_auth_role
                {
                    id = Guid.NewGuid().ToString(),
                    name = model.Name,
                    description = model.Description
                };
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void DeleteRole(string roleId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_auth_roles.Find(roleId);
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public void EditPermission(PermissionViewModel model)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_auth_permissions.Find(model.Id);
                entity.name = model.Name;
                entity.description = model.Description;
                entity.code = model.Code;
                context.SaveChanges();
            }
        }

        public void EditRole(RoleViewModel model)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_auth_roles.Find(model.Id);
                entity.name = model.Name;
                entity.description = model.Description;
                context.SaveChanges();
            }
        }

        public string[] GetAllPermissions4Code(string userId)
        {
            throw new NotImplementedException();
        }

        public Tuple<List<PermissionViewModel>, int> GetPage4PermissionViewModel()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<RoleViewModel>, int> GetPage4RoleViewModel()
        {
            throw new NotImplementedException();
        }

        public Tuple<List<AuthUserViewModel>, int> GetPage4UserViewModel(QueryModel model)
        {
            throw new NotImplementedException();
        }

        public PermissionViewModel GetPermissionViewModel(string id)
        {
            throw new NotImplementedException();
        }

        public List<PermissionViewModel> GetPermissionViewModels4AddByRoleId(string roleId, string permissionNameLike)
        {
            throw new NotImplementedException();
        }

        public List<PermissionViewModel> GetPermissionViewModels4AddByUserId(string userId, string permissionNameLike)
        {
            throw new NotImplementedException();
        }

        public List<PermissionViewModel> GetPermissionViewModelsByRoleId(string roleId, string permissionNameLike)
        {
            throw new NotImplementedException();
        }

        public List<PermissionViewModel> GetPermissionViewModelsByUserId(string userId, string permissionNameLike)
        {
            throw new NotImplementedException();
        }

        public RoleViewModel GetRoleViewModel(string id)
        {
            throw new NotImplementedException();
        }

        public List<RoleViewModel> GetRoleViewModels4AddByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public List<RoleViewModel> GetRoleViewModelsByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public UpdateUserViewModel GetUpdateUserViewModel(string userId)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterViewModel model)
        {
            var entity = new hy_user
            {
                id = Guid.NewGuid().ToString(),
                access_failed_times = 0,
                is_locked = false,
                two_factor_enabled = false,
                email_confirmed = false,
                user_name = model.UserName,
                nick_name = model.NickName,
                email = model.Email,
                is_valid = true
            };
            entity.security_stamp = Guid.NewGuid().ToString();
            entity.password_hash = new PasswordHasher().HashPassword(model.Password);
            using (var context = new HyContext(LoginId))
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void RemoveRolePermission(string roleId, string permissionId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_auth_roles.Find(roleId);
                var remove = entity.hy_auth_permissions.First(o => o.id == permissionId);
                entity.hy_auth_permissions.Remove(remove);
                context.SaveChanges();
            }
        }

        public void RemoveUserPermission(string userId, string permissionId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.Find(userId);
                var remove = entity.hy_auth_permissions.First(o => o.id == permissionId);
                entity.hy_auth_permissions.Remove(remove);
                context.SaveChanges();
            }
        }

        public void RemoveUserRole(string userId, string roleId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.Find(userId);
                var remove = entity.hy_auth_roles.First(o => o.id == roleId);
                entity.hy_auth_roles.Remove(remove);
                context.SaveChanges();
            }
        }

        public void ResetPassword(ResetPasswordViewModel model)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.First(o => o.user_name == model.UserName);
                entity.security_stamp = Guid.NewGuid().ToString();
                entity.password_hash = new PasswordHasher().HashPassword(model.Password);
                context.SaveChanges();
            }
        }

        public void SetUnlocked4User(string userId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.Find(userId);
                entity.is_locked = false;
                context.SaveChanges();
            }
        }

        public void SetUserValid(string userId)
        {
            using (var context = new HyContext(LoginId))
            {
                var entity = context.hy_users.Find(userId);
                entity.is_valid = true;
                context.SaveChanges();
            }
        }

        public void UpdateUser(UpdateUserViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
