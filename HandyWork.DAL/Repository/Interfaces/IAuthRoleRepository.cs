using HandyWork.Model;
using HandyWork.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IAuthRoleRepository : IBaseRepository<hy_auth_role>
    {
        hy_auth_role Find(string id);
        hy_auth_role FindByName(string name);
    }
}
