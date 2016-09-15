using HandyWork.Common.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using HandyWork.Common.Extensions;


namespace HandyWork.Common.Utility
{
    // <select id = "Action_FindByQuery" parameterClass="UIH.ServiceCenter.Models.Query.ActionQuery" resultMap="Action_ListResult">
    //  select* from sc_action where id > 0
    //  <isNotEmpty prepend = " and " property="NameEqual" >name = #NameEqual#</isNotEmpty>
    //  <isNotEmpty prepend = " and " property="NameNotEqual" >name != #NameNotEqual#</isNotEmpty>
    //  <isNotEmpty prepend = " and " property="NameLike" >name like '%$NameLike$%'</isNotEmpty>
    //  <isNotEmpty prepend = " and " property="NameNotLike" >name not like '%$NameNotLike$%'</isNotEmpty>

    //  <isNotEmpty prepend = " and " property="DescriptionEqual" >description = #DescriptionEqual#</isNotEmpty>
    //  <isNotEmpty prepend = " and " property="DescriptionNotEqual" >description != #DescriptionNotEqual#</isNotEmpty>
    //  <isNotEmpty prepend = " and " property="DescriptionLike" >description like '%$DescriptionLike$%'</isNotEmpty>
    //  <isNotEmpty prepend = " and " property="DescriptionNotLike" >description not like '%$DescriptionNotLike$%'</isNotEmpty>

    //  <isNotEmpty property = "OrderByKey" > order by $OrderByKey$ $OrderByValue$</isNotEmpty>
    //  <isGreaterThan property = "Limit" compareValue="-1" prepend=" limit ">
    //    <isGreaterThan property = "Offset" compareValue="-1" prepend=" ">
    //      #Offset#,
    //    </isGreaterThan>
    //    #Limit#
    //  </isGreaterThan>
    //</select>
    public static class ExpressionUtility
    {
        public static Expression<Func<TEntity,bool>> Build<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property, object value, QueryCondition queryCondition, QueryMethod queryMethod)
        {
            switch (queryMethod)
            {
                case QueryMethod.Equal:
                    return ExpressionUtility.Equal(property, value, queryCondition);
                case QueryMethod.GreaterThan:
                    break;
                case QueryMethod.GreaterThanOrEqual:
                    break;
                case QueryMethod.In:
                    break;
                case QueryMethod.LessThan:
                    break;
                case QueryMethod.LessThanOrEqual:
                    break;
                case QueryMethod.Like:
                    break;
                case QueryMethod.NotEqual:
                    break;
                default:
                    break;
            }
            throw new Exception();
        }
        
        public static Expression<Func<TEntity, bool>> Equal<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property, object value, QueryCondition queryCondition)
        {
            bool isPassed;
            switch (queryCondition)
            {
                case QueryCondition.IsNull:
                    isPassed = value == null;
                    break;
                case QueryCondition.IsNotNull:
                    isPassed = value != null;
                    break;
                case QueryCondition.IsNullOrWhiteSpace:
                    isPassed = value == null || string.IsNullOrWhiteSpace(value as string);
                    break;
                case QueryCondition.IsNotNullOrWhiteSpace:
                    isPassed = value != null && !string.IsNullOrWhiteSpace(value as string);
                    break;
                default:
                    throw new ArgumentException(string.Format("不支持对QueryCondition为{0}的表达式构建", queryCondition.ToString()));
            }
            if (isPassed)
            {
                var parameter = Expression.Parameter(typeof(TEntity), "o");
                var propertyName = (property.Body as MemberExpression).Member.Name;
                var member = Expression.Property(parameter, propertyName);
                var binary = Expression.MakeBinary(ExpressionType.Equal, member, Expression.Constant(value));
                return Expression.Lambda<Func<TEntity, bool>>(binary, parameter);
            }
            else
            {
                return null;
            }
        }
    }
}
