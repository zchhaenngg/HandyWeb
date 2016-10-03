using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using HandyWork.Common.Extensions;
using HandyWork.ViewModel.PCWeb.Query;

namespace HandyWork.DAL.Repository.Abstracts
{
    public abstract class BaseRepository<T>
        where T : class
    {
        protected UnitOfWork UnitOfWork { get; }
        public DbSet<T> Source { get; }

        public BaseRepository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Source = UnitOfWork.AsTracking<T>();
        }
        
    }
}
