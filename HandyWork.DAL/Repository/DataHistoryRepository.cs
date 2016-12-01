using HandyWork.Common.Extensions;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using HandyWork.Model.Entity;
using HandyWork.ViewModel.PCWeb.Query;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HandyWork.DAL.Repository
{
    public sealed class DataHistoryRepository: IDataHistoryRepository
    {
        private UnitOfWork _unitOfWork;
        public DbSet<hy_data_history> Source { get; set; }

        public DataHistoryRepository(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Source = unitOfWork.AsTracking<hy_data_history>();
        }
        
        public hy_data_history Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }
            return Source.Find(id);
        }

        hy_data_history IDataHistoryRepository.Find(string id)
        {
            throw new NotImplementedException();
        }
    }
}
