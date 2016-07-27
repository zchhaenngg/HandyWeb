﻿using HandyWork.DAL.Repository;
using HandyWork.Model.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL.Cache
{
    public class SysColumnsCache
    {
        private static List<SysColumns> _sysColumns;
        public static bool IsExist(string tableName, string columnName)
        {
            return _sysColumns.Exists(o => o.ColumnName == columnName && o.TableName == tableName);
        }
        private static string GetDescription(string tableName, string columnName)
        {
            return _sysColumns.First(o => o.ColumnName == columnName && o.TableName == tableName).Description;
        }
        public static void LoadData()
        {
            using (UserEntities context = new UserEntities())
            {
                SqlRepository sqlRepository = new SqlRepository(context);
                List<SysColumns> sysColumns = sqlRepository.GetList<SysColumns>(SQL.ColumnDescription);
                _sysColumns = sysColumns.Select(o => new SysColumns()
                {
                    TableName = o.TableName,
                    ColumnName = o.ColumnName,
                    Description = (o.Description ?? string.Empty).Split(new char[] { '*' })[0]
                }).ToList();
            }

        }

        public static string CompareObject(string tableName, object newObj, Func<string, Tuple<string, string>> propertyOrginCurrentValue)
        {
            if (newObj == null)
            {
                throw new ArgumentNullException(nameof(newObj));
            }
            StringBuilder sBuilder = new StringBuilder();
            PropertyInfo[] properties = newObj.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (!IsExist(tableName, prop.Name))
                {
                    continue;
                }
                if (prop.CanRead && prop.CanWrite)
                {
                    Tuple<string, string> orginCurrentValue = propertyOrginCurrentValue(prop.Name);
                    if (orginCurrentValue != null)
                    {
                        if (orginCurrentValue.Item1 != orginCurrentValue.Item2)
                        {
                            sBuilder.Append(string.Format("{0}:{1} -> {2};<br/>", GetDescription(tableName, prop.Name), orginCurrentValue.Item1, orginCurrentValue.Item2));
                        }
                    }
                }
            }
            return sBuilder.ToString();
        }
    }
}