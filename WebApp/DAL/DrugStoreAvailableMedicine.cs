//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DrugStoreAvailableMedicine
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    
        public virtual Medicine Medicine { get; set; }
    }
}
