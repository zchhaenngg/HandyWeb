using HandyWork.Common.EntityFramework.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace HandyWork.Web.App_Start
{
    public class QueryModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var methods = Enum.GetNames(typeof(QueryMethod)).Select(o => string.Format("[{0}]", o)).ToArray();
            var keys = controllerContext.HttpContext.Request.Params.AllKeys.Where(k => methods.Any(m=>k.StartsWith(m))).ToList();
            if (keys.Count != 0)
            {
                var model = new QueryModel { Items = new List<QueryItem>() };
                foreach (var key in keys)
                {
                    var keywords = key.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var queryItem = new QueryItem();
                    queryItem.Field = keywords[1].Trim();
                    queryItem.Value = bindingContext.ValueProvider.GetValue(key).AttemptedValue;
                    queryItem.Method = (QueryMethod)Enum.Parse(typeof(QueryMethod), keywords[0]);
                    model.Items.Add(queryItem);
                }
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}