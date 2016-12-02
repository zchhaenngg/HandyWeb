using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;
using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Common.Utility;
using HandyWork.Model.Entity;

namespace HandyWork.DAL.Queryable
{
    public class Mapping
    {
        private static List<Map> Maps = new List<Map>();
        static Mapping()
        {
            Maps.Add(Map<hy_user, UserQuery>.CreateMap(Build4AuthUser));
            Maps.Add(Map<hy_auth_role, AuthRoleQuery>.CreateMap(Build4AuthRole));
            Maps.Add(Map<hy_auth_permission, AuthPermissionQuery>.CreateMap(Build4AuthPermission));
        }

        public static Map FindMap<TEntity>(object obj)
        {
            var queryType = obj.GetType();
            return Maps.FirstOrDefault(o => o.Souce == typeof(TEntity) && o.Destination == queryType);
        }
        public static Expression<Func<TEntity, bool>> GetExpression<TEntity>(object query)
        {
            var map = FindMap<TEntity>(query);
            if (map == null)
            {
                throw new Exception(string.Format("不存在的Map,TEntity：{0}， TQuery：{1}", typeof(TEntity).Name, map.Destination.Name));
            }

            var expression = map.GetType().GetMethod("Invoke").Invoke(map, new object[] { query }) as Expression<Func<TEntity, bool>>;
            return expression;
        }

        public static Expression<Func<TEntity, bool>> GetExpression<TEntity, TQuery>(TQuery query)
        {
            var map = Maps.FirstOrDefault(o => o.Souce == typeof(TEntity) && o.Destination == typeof(TQuery)) as Map<TEntity, TQuery>;
            if (map == null)
            {
                throw new Exception(string.Format("不存在的Map,TEntity：{0}， TQuery：{1}", typeof(TEntity).Name,typeof(TQuery).Name));
            }
            var expression = map.Invoke(query);
            return expression;
        }

        private static Expression<Func<hy_user, bool>> Build4AuthUser(UserQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(string.Format("{0},不能为空", nameof(query)));
            }

            var factory = new LambdaFactory<hy_user>().IfLike(IsNotEmpty.For(query.UserNameLike), o => o.user_name, query.UserNameLike)
                .IfEqual(IsNotEmpty.For(query.UserNameEqual), o => o.user_name, query.UserNameEqual)
                .IfLike(IsNotEmpty.For(query.RealNameLike), o => o.nick_name, query.RealNameLike)
                .IfEqual(IsNotEmpty.For(query.IsValid), o => o.is_valid, query.IsValid)
                .IfEqual(IsNotEmpty.For(query.IsLocked), o => o.is_lockout_enable, query.IsLocked);
            return factory.ToExpression();
        }

        private static Expression<Func<hy_auth_role, bool>> Build4AuthRole(AuthRoleQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(string.Format("{0},不能为空", nameof(query)));
            }
            var factory = new LambdaFactory<hy_auth_role>().IfLike(IsNotEmpty.For(query.NameLike), o => o.name, query.NameLike);
            return factory.ToExpression();
        }

        private static Expression<Func<hy_auth_permission, bool>> Build4AuthPermission(AuthPermissionQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(string.Format("{0},不能为空", nameof(query)));
            }
            var factory = new LambdaFactory<hy_auth_permission>()
                .IfLike(IsNotEmpty.For(query.NameLike), o => o.name, query.NameLike)
                .IfLike(IsNotEmpty.For(query.CodeLike), o => o.code, query.CodeLike);
            return factory.ToExpression();
        }
    }
}
