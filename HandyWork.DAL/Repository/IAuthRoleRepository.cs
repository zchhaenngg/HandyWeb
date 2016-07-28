using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository
{
    public interface IAuthRoleRepository : IBaseRepository<AuthRole>
    {
        void Remove(string id);
        AuthRole Find(string id);
        AuthRole FindByName(string name);
        List<AuthRole> GetAll();
    }
}
