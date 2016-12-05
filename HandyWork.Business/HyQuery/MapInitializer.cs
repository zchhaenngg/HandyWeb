using HandyWork.Common.EntityFramework.Elements;
using HandyWork.Common.EntityFramework.Lambdas;
using HandyWork.Common.EntityFramework.Maps;
using HandyWork.Model.Entity;
using HandyWork.ViewModel.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.Business.HyQuery
{
    public class MapInitializer
    {
        private static MapContainer _container;
        public static MapContainer Container => _container ?? (_container = new MapContainer());
        static MapInitializer()
        {
            Container.Register(Map<hy_user, UserQuery>.CreateMap(Build4AuthUser));
            Container.Register(Map<hy_auth_role, AuthRoleQuery>.CreateMap(Build4AuthRole));
            Container.Register(Map<hy_auth_permission, AuthPermissionQuery>.CreateMap(Build4AuthPermission));
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
                .IfEqual(IsNotEmpty.For(query.IsLocked), o => o.is_locked, query.IsLocked);
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
