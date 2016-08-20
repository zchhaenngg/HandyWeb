using HandyWork.Common.Model;
using HandyWork.DAL;
using HandyWork.DAL.Repository;
using HandyWork.UIBusiness.IManager;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness.Manager
{
    /// <summary>
    /// 访问入口，保证使用统一的上下文.应对外提供，本层由外部调用，
    /// UnitOfWork因此也应当由外部统一创建以保证唯一性，必要时由外部通过UnitOfWork做事务处理
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        internal UserEntities UserEntities { get; } = new UserEntities();
        internal HistoryEntities HistoryEntities { get; } = new HistoryEntities();

        public IUserRepository UserRepository { get; }
        public IAuthPermissionRepository AuthPermissionRepository { get; }
        public IAuthRoleRepository AuthRoleRepository { get; }

        public List<ErrorInfo> ErrorInfos { get; } = new List<ErrorInfo>();

        public UnitOfWork()
        {
            UserRepository = new UserRepository(UserEntities, HistoryEntities);
            AuthPermissionRepository = new AuthPermissionRepository(UserEntities);
            AuthRoleRepository = new AuthRoleRepository(UserEntities);
        }
        
        /// <summary>
        /// -1 表明ErrorInfos中有错误记录因此不会提交变更。
        /// </summary>
        public void SaveChanges()
        {
            if (ErrorInfos.Count > 0)
            {
                return;
            }
            try
            {
                this.UserEntities.SaveChanges();
                this.HistoryEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                ErrorInfos.Add(new ErrorInfo("", ex.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage));
            }
        }

        public void Dispose()
        {
            this.UserEntities.Dispose();
            this.HistoryEntities.Dispose();
        }
    }
}
