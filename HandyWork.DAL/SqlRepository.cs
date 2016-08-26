using HandyWork.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    public class SqlRepository
    {
        private DbContext _dbContext;

        public SqlRepository(DbContext context)
        {
            _dbContext = context;
        }

        public List<T> GetList<T>(string sql)
        {
            List<T> list = _dbContext.Database.SqlQuery<T>(sql).ToList();
            return list;
        }

        public List<T> GetList<T>(string sql, params object[] parameters)
        {
            List<T> list = _dbContext.Database.SqlQuery<T>(sql, parameters).ToList();
            return list;
        }
    }
}
