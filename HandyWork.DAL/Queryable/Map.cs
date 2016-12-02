using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Queryable
{
    public class Map
    {
        public Type Souce { get; protected set; }
        public Type Destination { get; protected set; }
    }
    /// <summary>
    /// TEntity,TQuery和实际传入的query对象构建表达式
    /// </summary>
    public class Map<TEntity, TQuery> : Map
    {
        public delegate Expression<Func<TEntity, bool>> Expression_TEntity_Bool(TQuery query);
        public event Expression_TEntity_Bool Handler;

        public Map()
        {
            Souce = typeof(TEntity);
            Destination = typeof(TQuery);
        }

        public static Map<TEntity, TQuery> CreateMap(Expression_TEntity_Bool handler)
        {
            var map = new Map<TEntity, TQuery>();
            map.Handler += handler;
            return map;
        }

        public Expression<Func<TEntity, bool>> Invoke(TQuery query)
        {
            return Handler(query);
        }
    }
}
