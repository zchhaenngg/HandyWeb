using HandyWork.Model;
using HandyWork.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IDataHistoryRepository : IBaseRepository<hy_data_history>
    {
        hy_data_history Find(string id);
    }
}
