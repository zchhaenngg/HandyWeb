using HandyWork.Common.EntityFramework.Query;
using HandyWork.ViewModel.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Services.Service.Interfaces
{
    public interface IAccountService
    {
        UpdateUserViewModel GetUpdateUserViewModel(string userId);
        PermissionViewModel GetPermissionViewModel(string id);
        RoleViewModel GetRoleViewModel(string id);
        void Register(RegisterViewModel model);
        void UpdateUser(UpdateUserViewModel model);
        void ResetPassword(ResetPasswordViewModel model);
        
        void ReverseUserValid(string userId);
        void SetUnlocked4User(string userId);
        void CreatePermission(PermissionViewModel model);
        void EditPermission(PermissionViewModel model);
        
        void CreateRole(RoleViewModel model);
        void EditRole(RoleViewModel model);
        void DeleteRole(string id);
        void AddUserPermission(string userId, string permissionId);
        void RemoveUserPermission(string userId, string permissionId);
        void AddUserRole(string userId, string roleId);
        void RemoveUserRole(string userId, string roleId);
        void AddRolePermission(string roleId, string permissionId);
        void RemoveRolePermission(string roleId, string permissionId);
        string[] GetAllPermissions4Code(string userId);

        IList<AuthUserViewModel> GetPage4UserViewModel(QueryModel model, out int total);
        IList<PermissionViewModel> GetPage4PermissionViewModel(out int total);
        IList<RoleViewModel> GetPage4RoleViewModel(out int total);
        IList<PermissionViewModel> GetPermissionViewModelsByUserId(string userId, string permissionNameLike);
        IList<PermissionViewModel> GetPermissionViewModels4AddByUserId(string userId, string permissionNameLike);
        IList<RoleViewModel> GetRoleViewModelsByUserId(string userId);
        IList<RoleViewModel> GetRoleViewModels4AddByUserId(string userId);
        IList<PermissionViewModel> GetPermissionViewModelsByRoleId(string roleId, string permissionNameLike);
        IList<PermissionViewModel> GetPermissionViewModels4AddByRoleId(string roleId, string permissionNameLike);
    }
}
