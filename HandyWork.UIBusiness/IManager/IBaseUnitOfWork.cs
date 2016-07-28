using HandyWork.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.IManager
{
    internal interface IBaseUnitOfWork : IDisposable
    {
        UserRepository UserRepository { get; }
        AuthPermissionRepository AuthPermissionRepository { get; }
        AuthRoleRepository AuthRoleRepository { get; }
    }
}
