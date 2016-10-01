using HandyWork.DAL.Cache;
using HandyWork.DAL.Repository.Interfaces;
using HandyWork.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Repository.Abstracts
{
    public class BaseRecordDataHistory<T>
        where T : class
    {
        private bool _isRecordDataChange;
        protected UnitOfWork UnitOfWork { get; set; }

        public BaseRecordDataHistory(UnitOfWork unitOfWork, bool isRecordDataChange)
        {
            UnitOfWork = unitOfWork;
            _isRecordDataChange = isRecordDataChange;
        }

        protected virtual void RecordData(T entity, string operatorId)
        {

        }

        /// <summary>
        /// 返回值为不需要记录在历史变更中的字段。同时需要对history的foreign key id和统计字段Keep1,Keep2等赋值
        /// </summary>
        protected virtual string[] OnBeforeRecordHistory(T entity, DataHistory history) => null;
    }
}
