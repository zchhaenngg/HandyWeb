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

namespace HandyWork.DAL.Queryable
{
    public class Mapping
    {
        private static List<Map> Maps = new List<Map>();
        static Mapping()
        {
            Maps.Add(Map<AuthUser, UserQuery>.CreateMap(Build4AuthUser));
            Maps.Add(Map<AuthRole, AuthRoleQuery>.CreateMap(Build4AuthRole));
            Maps.Add(Map<AuthPermission, AuthPermissionQuery>.CreateMap(Build4AuthPermission));
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

        private static Expression<Func<AuthUser, bool>> Build4AuthUser(UserQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(string.Format("{0},不能为空", nameof(query)));
            }
            var expression =  ExpressionUtility.True<AuthUser>()
                .And(IsNotEmpty.For(query.UserNameLike), LikeLambda<AuthUser>.For(o => o.UserName, query.UserNameLike))
                .And(IsNotEmpty.For(query.UserNameEqual), EqualLambda<AuthUser, string>.For(o => o.UserName, query.UserNameEqual))
                .And(IsNotEmpty.For(query.RealNameLike), LikeLambda<AuthUser>.For(o => o.RealName, query.RealNameLike))
                .And(IsNotNull.For(query.IsValid), EqualLambda<AuthUser, bool>.For(o => o.IsValid, query.IsValid))
                .And(IsNotNull.For(query.IsLocked), EqualLambda<AuthUser, bool>.For(o => o.IsLocked, query.IsLocked));
            return expression;
        }

        private static Expression<Func<AuthRole, bool>> Build4AuthRole(AuthRoleQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(string.Format("{0},不能为空", nameof(query)));
            }
            var expression = ExpressionUtility.True<AuthRole>()
                .And(IsNotEmpty.For(query.NameLike), LikeLambda<AuthRole>.For(o => o.Name, query.NameLike));
            return expression;
        }

        private static Expression<Func<AuthPermission, bool>> Build4AuthPermission(AuthPermissionQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(string.Format("{0},不能为空", nameof(query)));
            }
            var expression = ExpressionUtility.True<AuthPermission>()
                .And(IsNotEmpty.For(query.NameLike), LikeLambda<AuthPermission>.For(o => o.Name, query.NameLike))
                .And(IsNotEmpty.For(query.CodeLike), EqualLambda<AuthPermission, string>.For(o => o.Code, query.CodeLike));
            return expression;
        }
    }
}
