using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Enums
{
    public static class DALEnumExtensions
    {
        public static string AsPlural(this Unit unit, int count)
        {
            var str = unit.GetDisplayName();

            var remainder = count % 10;

            switch(str)
            {
                case "sztuk" when(count == 1):
                    return str + "a";
                case "sztuk" when (remainder > 1 && remainder < 5):
                    return str + "i";
                case "sztuk":
                    return str;
                case "gram" when (count == 1):
                    return str;
                case "gram" when (remainder > 1 && remainder < 5):
                    return str + "y";
                case "gram":
                    return str+"ów";
                case "mililitr" when (count == 1):
                    return str;
                case "mililitr" when (remainder > 1 && remainder < 5):
                    return str + "y";
                case "mililitr":
                    return str + "ów";
                default:
                    return str;
            }
        }
    }
}
