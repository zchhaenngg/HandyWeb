using HandyWork.Model;
using HandyWork.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        T Add(T entity, string operatorId);
        T Update(T entity, string operatorId);
        T Remove(T entity);
        T Find(T entity);
        Tuple<List<T>, int> GetPage(BaseQuery query, Expression<Func<T, bool>> where = null);
    }
}
