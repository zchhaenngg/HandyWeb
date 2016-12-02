using HandyWork.Services.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.EntityFramework.Query;
using HandyWork.ViewModel.Web;

namespace HandyWork.Services.Service
{
    public class AccountService : BaseService, IAccountService
    {
        public AccountService(string loginId) : base(loginId)
        {
        }

        public void AddRolePermission(string roleId, string permissionId)
        {
            throw new NotImplementedException();
        }

        public void AddUserPermission(string userId, string permissionId)
        {
            throw new NotImplementedException();
        }

        public void AddUserRole(string userId, string roleId)
        {
            throw new NotImplementedException();
        }

        public void CreatePermission(PermissionViewModel model)
        {
            throw new NotImplementedException();
        }

        public void CreateRole(RoleViewModel model)
        {
            throw new NotImplementedException();
        }

        public Tuple<bool, string> DeleteRole(string id)
        {
            throw new NotImplementedException();
        }

        public void EditPermission(PermissionViewModel model)
        {
            throw new NotImplementedException();
        }

        public void EditRole(RoleViewModel model)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void RemoveRolePermission(string roleId, string permissionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserPermission(string userId, string permissionId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserRole(string userId, string roleId)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public void SetUnlocked4User(string userId)
        {
            throw new NotImplementedException();
        }

        public string SetUserValid(string userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(UpdateUserViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
