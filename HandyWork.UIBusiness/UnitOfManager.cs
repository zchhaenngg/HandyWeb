using HandyWork.Common.Model;
using HandyWork.DAL;
using HandyWork.UIBusiness.IManager;
using HandyWork.UIBusiness.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.UIBusiness
{
    public class UnitOfManager : IDisposable
    {
        internal UnitOfWork UnitOfWork { get; } = new UnitOfWork();

        private IAccountManager _accountManager;
        public IAccountManager AccountManager
        {
            get
            {
                if (_accountManager == null)
                {
                    _accountManager = new AccountManager(this);
                }
                return _accountManager;
            }
        }

        //直接只返回表达式结果的属性或方法使用 => 来定义
        public List<ErrorInfo> ErrorInfos => UnitOfWork.ErrorInfos;

        public void Dispose()
        {
            UnitOfWork.Dispose();
        }
    }
}
