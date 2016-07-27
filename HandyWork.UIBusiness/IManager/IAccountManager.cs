using HandyWork.UIBusiness.Enums;
using HandyWork.UIBusiness.ViewModel.PCWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.IManager
{
    public interface IAccountManager
    {
        SignInResult SignIn(string userName, string password);
        void Register(RegisterViewModel model);
        void UpdateUser(UpdateUserViewModel model);
        void ResetPassword(ResetPasswordViewModel model);
        UpdateUserViewModel GetUpdateUserViewModel(string userId);
        string SetUserValid(string userId);
        void SetUnlocked4User(string userId);
        void CreatePermission(PermissionViewModel model);
        void EditPermission(PermissionViewModel model);
        PermissionViewModel GetPermissionViewModel(string id);
        void CreateRole(RoleViewModel model);
        void EditRole(RoleViewModel model);
        Tuple<bool, string> DeleteRole(string id);
        RoleViewModel GetRoleViewModel(string id);
        Tuple<List<UserViewModel>, int> GetPage4UserViewModel();
        List<PermissionViewModel> GetPermissionViewModelsByUserId(string userId, string permissionNameLike);
        List<PermissionViewModel> GetPermissionViewModels4AddByUserId(string userId, string permissionNameLike);
        void AddUserPermission(string userId, string permissionId);
        void RemoveUserPermission(string userId, string permissionId);
        List<RoleViewModel> GetRoleViewModelsByUserId(string userId);
        List<RoleViewModel> GetRoleViewModels4AddByUserId(string userId);
        void AddUserRole(string userId, string roleId);
        void RemoveUserRole(string userId, string roleId);
        Tuple<List<PermissionViewModel>, int> GetPage4PermissionViewModel();
        Tuple<List<RoleViewModel>, int> GetPage4RoleViewModel();
        List<PermissionViewModel> GetPermissionViewModelsByRoleId(string roleId, string permissionNameLike);
        List<PermissionViewModel> GetPermissionViewModels4AddByRoleId(string roleId, string permissionNameLike);
        void AddRolePermission(string roleId, string permissionId);
        void RemoveRolePermission(string roleId, string permissionId);
    }
}
