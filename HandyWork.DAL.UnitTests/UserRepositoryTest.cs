﻿using HandyWork.Common.Extensions;
using HandyWork.DAL;
using HandyWork.DAL.Queryable;
using HandyWork.Model;
using HandyWork.ViewModel.PCWeb.Query;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Profiling;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace HandyWork.UnitTests
{
    [TestClass]
    public class UserRepositoryTest: BaseTest
    {
        [TestMethod]
        public void test()
        {
            var query = new UserQuery();
            query.UserNameLike = "cheng";
            query.RealNameLike = "成";
            using (ReportOuput output = new ReportOuput())
            {
                int iTotal;
                UnitOfWork.AsNoTracking<AuthUser>().GetPage(query, out iTotal).Select(o => new { o.RealName, o.UserName }).ToList();
                UnitOfWork.AsTracking<AuthUser>().GetPage(query, out iTotal).Select(o => new { o.RealName, o.UserName }).ToList();
                //UnitOfWork.AsNoTracking<AuthUser>().Where(o => true).ToList();//执行的sql语句中没有where查询
                //UnitOfWork.AsNoTracking<AuthUser>().Where(o => false).ToList();// WHERE 1 = 0
            }   
        }
    }
}
