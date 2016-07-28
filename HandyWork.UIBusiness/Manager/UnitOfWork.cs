using Autofac;
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
    /// 访问入口，保证使用统一的上下文
    /// </summary>
    internal class UnitOfWork : IUnitOfWork
    {
        IContainer Container { get; }//todo

        public UnitOfWork(IContainer container)
        {
            Container = container;
        }

        public List<ErrorInfo> ErrorInfos { get; } = new List<ErrorInfo>();
        internal UserEntities UserEntities { get; } = new UserEntities();
        internal HistoryEntities HistoryEntities { get; } = new HistoryEntities();

        #region Repository 用户及权限
        public IUserRepository UserRepository
        {
            get
            {
                return Container.Resolve<IUserRepository>();
            }
        }
        public IAuthPermissionRepository AuthPermissionRepository
        {
            get
            {
                return Container.Resolve<IAuthPermissionRepository>();
            }
        }
        public IAuthRoleRepository AuthRoleRepository
        {
            get
            {
                return Container.Resolve<IAuthRoleRepository>();
            }
        }

        
        #endregion

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
