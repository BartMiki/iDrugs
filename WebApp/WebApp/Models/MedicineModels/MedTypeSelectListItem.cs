using Common.Utils;
using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.MedicineModels
{
    public class MedTypeSelectListItem
    {
        public MedType EnumValue { get; set; }
        public string DisplayName { get => EnumValue.GetDisplayName(); }

        public static IEnumerable<MedTypeSelectListItem> GetSelectList()
        {
            foreach (var e in Enum.GetNames(typeof(MedType)).Select(x => x.AsEnum<MedType>()))
            {
                yield return new MedTypeSelectListItem { EnumValue = e };
            }
        }
    }
}
