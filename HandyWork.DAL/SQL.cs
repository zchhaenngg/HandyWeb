using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyWork.DAL
{
    internal static class SQL
    {
        public const string ColumnDescription = @"SELECT d.name as TableName,a.name as ColumnName,g.[value] as Description FROM dbo.syscolumns a 
        left join dbo.systypes b on a.xusertype=b.xusertype
        inner join dbo.sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'
        left join dbo.syscomments e on a.cdefault=e.id
        left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id
        left join sys.extended_properties f on d.id=f.major_id and f.minor_id=0";

        public const string Permission4RoleUser = @"SELECT distinct permission.*
  FROM dbo.AuthPermission permission
  INNER JOIN dbo.AuthRolePermission rolePermission on permission.Id=rolePermission.PermissionId
  Left OUTER JOIN dbo.AuthUserRole userRole on rolePermission.RoleId=userRole.RoleId
  where userRole.UserId=@p0";
    }
}
