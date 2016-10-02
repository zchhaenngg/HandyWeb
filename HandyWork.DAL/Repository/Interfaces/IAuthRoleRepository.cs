using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IAuthRoleRepository : IBaseRepository<AuthRole>
    {
        AuthRole Find(string id);
        AuthRole FindByName(string name);
    }
}
