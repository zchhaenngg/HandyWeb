using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HandyWork.Common.Extensions;

namespace HandyWork.Common.EntityFramework.Lambdas
{
    /// <summary>
    /// 数组、集合。  如果是关于string的请使用LikeLambda
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    public class ContainLambda<TEntity, TProperty> : BaseLambda<TEntity, TProperty>
    {
        public ContainLambda(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue) : base(entityProperty, entityValue)
        {
            if ((entityValue as List<TProperty>) == null)
            {//类型自动转换
                List<TProperty> list = new List<TProperty>();
                #region 设置list
                ICollection coll = Value as ICollection;
                var entityType = typeof(TProperty);
                foreach (var item in coll)
                {
                    if (item == null)
                    {
                        list.Add(default(TProperty));
                    }
                    else
                    {
                        var valueType = item.GetType();
                        if (valueType == entityType)
                        {
                            list.Add((TProperty)item);
                        }
                        else
                        {
                            if (entityType.IsShortOrNullable())
                            {
                                list.Add((TProperty)(Convert.ToInt16(item) as object));
                            }
                            else if (entityType.IsFloatOrNullable())
                            {
                                list.Add((TProperty)(Convert.ToSingle(item) as object));
                            }
                            else if (entityType.IsDoubleOrNullable())
                            {
                                list.Add((TProperty)(Convert.ToDouble(item) as object));
                            }
                            else if (entityType.IsString())
                            {
                                list.Add((TProperty)(item.ToString() as object));
                            }
                            else
                            {
                                throw new NotSupportedException("ContainLambda 构建表达式，不支持转换成类型" + entityType.Name);
                            }
                        }
                    }
                }
                #endregion
                Value = list;
            }
        }

        public static ContainLambda<TEntity, TProperty> For(Expression<Func<TEntity, TProperty>> entityProperty, object entityValue)
        {
            return new ContainLambda<TEntity, TProperty>(entityProperty, entityValue);
        }

        public override Expression<Func<TEntity, bool>> Build()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "o");
            var propertyName = (PropertyExpression.Body as MemberExpression).Member.Name;
            var member = Expression.Property(parameter, propertyName);

            var body = Expression.Call(Expression.Constant(Value), GetContainMethodInfo(), member);
            return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
        }

        public virtual MethodInfo GetContainMethodInfo()
        {
            return Value.GetType().GetMethod("Contains");
        }
    }
}
