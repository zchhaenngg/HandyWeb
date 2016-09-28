﻿using HandyWork.Common.Authority;
using HandyWork.DAL;
using HandyWork.UIBusiness.Manager;
using HandyWork.UIBusiness.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness
{
    public class UnitOfManager : IDisposable
    {
        private UnitOfWork _unitOfWork;
        internal UnitOfWork UnitOfWork => _unitOfWork ?? (_unitOfWork =  new UnitOfWork());

        private IAccountManager _accountManager;
        public IAccountManager AccountManager => _accountManager ?? (_accountManager = new AccountManager(this));
        
        //直接只返回表达式结果的属性或方法使用 => 来定义
        public List<Error> Errors => _unitOfWork == null ? null : _unitOfWork.Errors;

        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
        } 
    }
}
