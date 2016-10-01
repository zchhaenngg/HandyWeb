using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IAuthPermissionRepository: IBaseRepository<AuthPermission>
    {
        AuthPermission Find(string id);
        AuthPermission FindByCode(string code);
        AuthPermission FindByName(string name);
        List<AuthPermission> GetAll();
    }
}
