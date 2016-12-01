using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IAuthPermissionRepository: IBaseRepository<hy_auth_permission>
    {
        hy_auth_permission Find(string id);
        hy_auth_permission FindByCode(string code);
        hy_auth_permission FindByName(string name);
    }
}
