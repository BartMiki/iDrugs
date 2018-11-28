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

        /// <summary>
        /// Use when you want to displya value of Enum in View or Model
        /// </summary>
        public static string GetDisplayName(this Enum value)
        {
            return value.GetAttribute<DisplayAttribute>().Name;
        }

        public static string AsString(this Enum value)
        {
            return value.ToString();
        }

        /// <summary>
        /// Use when you want to convert enum to database constant
        /// </summary>
        public static string AsDatabaseType(this Enum value)
        {
            return value.ToString().ToUpper();
        }

        /// <summary>
        /// Use when you want to convert value from database into enum counterpart
        /// </summary>
        public static TEnum AsEnum<TEnum>(this string value)
        {
            return (TEnum) Parse(typeof(TEnum), value.ToTitleCase());
        }
    }
}
