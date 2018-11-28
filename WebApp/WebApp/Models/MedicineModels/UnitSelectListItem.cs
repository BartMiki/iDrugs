using Common.Utils;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.MedicineModels
{
    public class UnitSelectListItem
    {
        public Unit EnumValue { get; set; }
        public string DisplayName { get => EnumValue.AsPlural(2).ToTitleCase(); }

        public static IEnumerable<UnitSelectListItem> GetSelectList()
        {
            foreach (var e in Enum.GetNames(typeof(Unit)).Select(x => x.AsEnum<Unit>()))
            {
                yield return new UnitSelectListItem { EnumValue = e };
            }
        }
    }
}
