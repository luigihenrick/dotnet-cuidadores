using System;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Cuidadores.Web.Infrastructure
{
    public class DateTimeModelBinder:IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CultureInfo ci = Thread.CurrentThread.CurrentUICulture;
            ValueProviderResult value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null)
            {
                return (DateTime?)null;
            }
            try
            {
                object date = value.ConvertTo(typeof(DateTime), ci);
                return date;
            }
            catch (Exception)
            {
                return (DateTime?)null;
            }
        }
    }
}