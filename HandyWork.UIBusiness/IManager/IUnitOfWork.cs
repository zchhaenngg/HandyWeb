using HandyWork.Common.Model;
using HandyWork.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.IManager
{
    public interface IUnitOfWork : IDisposable
    {
        List<ErrorInfo> ErrorInfos { get; }
        IUserRepository UserRepository { get;}
        IAuthPermissionRepository AuthPermissionRepository { get;}
        IAuthRoleRepository AuthRoleRepository { get; }
        void SaveChanges();
    }
}
