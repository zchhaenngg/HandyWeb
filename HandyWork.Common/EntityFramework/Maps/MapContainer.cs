using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Common.EntityFramework.Maps
{
    public class MapContainer
    {
        private List<Map> _maps = new List<Map>();
        public Map FindMap<TEntity>(object obj)
        {
            var queryType = obj.GetType();
            return _maps.FirstOrDefault(o => o.Souce == typeof(TEntity) && o.Destination == queryType);
        }
        public void Register<TEntity, TQuery>(Map<TEntity, TQuery> map)
        {
            _maps.Add(map);
        }

        public Expression<Func<TEntity, bool>> GetExpression<TEntity>(object query)
        {
            var map = FindMap<TEntity>(query);
            if (map == null)
            {
                throw new Exception(string.Format("不存在的Map,TEntity：{0}， TQuery：{1}", typeof(TEntity).Name, map.Destination.Name));
            }

            var expression = map.GetType().GetMethod("Invoke").Invoke(map, new object[] { query }) as Expression<Func<TEntity, bool>>;
            return expression;
        }

        public Expression<Func<TEntity, bool>> GetExpression<TEntity, TQuery>(TQuery query)
        {
            var map = _maps.FirstOrDefault(o => o.Souce == typeof(TEntity) && o.Destination == typeof(TQuery)) as Map<TEntity, TQuery>;
            if (map == null)
            {
                throw new Exception(string.Format("不存在的Map,TEntity：{0}， TQuery：{1}", typeof(TEntity).Name, typeof(TQuery).Name));
            }
            var expression = map.Invoke(query);
            return expression;
        }
    }
}
