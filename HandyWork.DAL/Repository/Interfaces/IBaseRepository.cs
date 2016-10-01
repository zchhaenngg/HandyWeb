﻿using HandyWork.ViewModel.PCWeb.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;

namespace HandyWork.DAL.Repository.Interfaces
{
    public interface IBaseRepository<T> : IRepository
        where T : class
    {
        DbSet<T> Source { get; }
        T Add(T entity, string operatorId);
        T Update(T entity, string operatorId);
        T Remove(T entity);
        T Find(T entity);
        List<T> FindAllByQuery(BaseQuery query);
        Tuple<List<T>, int> GetPage(BaseQuery query);
        Expression<Func<T, bool>> GetExpression(BaseQuery baseQuery);
    }
}
