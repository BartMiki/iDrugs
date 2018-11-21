using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.CustomAttributeExtensions;
using static System.Enum;

namespace Common.Utils
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            return value.GetType()
                .GetMember(value.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName(this Enum value)
        {
            return value.GetAttribute<DisplayAttribute>().Name;
        }

        public static string AsString(this Enum value)
        {
            return value.ToString();
        }

        public static string AsDatabaseType(this Enum value)
        {
            return value.ToString().ToUpper();
        }

        public static TEnum AsEnum<TEnum>(this string value)
        {
            return (TEnum) Parse(typeof(TEnum), value.ToTitleCase());
        }
    }
}
