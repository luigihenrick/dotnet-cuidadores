using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Cuidadores.Util.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            DisplayAttribute attr = GetDisplayAttribute(value);

            if (attr != null)
            {
                return attr.Name;
            }

            return value.ToString();
        }

        private static DisplayAttribute GetDisplayAttribute(Enum value)
        {
            Type type = value.GetType();

            MemberInfo memInfo = type.GetMember(value.ToString()).FirstOrDefault();

            if (memInfo != null)
            {
                var attrs = memInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                return attrs;
            }

            return null;
        }

        public static List<SelectListItem> ToSelectList(this Enum value)
        {
            return ToSelectList(value, true);
        }

        public static List<SelectListItem> ToSelectList(this Enum value, bool select)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            foreach (Enum item in Enum.GetValues(value.GetType()))
            {
                result.Add(new SelectListItem()
                {
                    Value = item.ToString(),
                    Text = item.GetDisplayName(),
                    Selected = (select && item.Equals(value))
                });
            }

            return result;
        }

        public static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
    }
}
