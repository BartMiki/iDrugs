﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class iDrugsEntities : DbContext
    {
        public iDrugsEntities()
            : base("name=iDrugsEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Apothecary> Apothecaries { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DrugStoreAvailableMedicine> DrugStoreAvailableMedicines { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineWarehouse> MedicineWarehouses { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionItem> PrescriptionItems { get; set; }
    
        public virtual int InsertApothecary(string firstName, string lastName, Nullable<decimal> monthlySalary)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("FirstName", firstName) :
                new ObjectParameter("FirstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("LastName", lastName) :
                new ObjectParameter("LastName", typeof(string));
    
            var monthlySalaryParameter = monthlySalary.HasValue ?
                new ObjectParameter("MonthlySalary", monthlySalary) :
                new ObjectParameter("MonthlySalary", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertApothecary", firstNameParameter, lastNameParameter, monthlySalaryParameter);
        }
    
        public virtual int FireApothecary(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("FireApothecary", idParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> CreateOrder(Nullable<int> apothecaryId)
        {
            var apothecaryIdParameter = apothecaryId.HasValue ?
                new ObjectParameter("ApothecaryId", apothecaryId) :
                new ObjectParameter("ApothecaryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("CreateOrder", apothecaryIdParameter);
        }
    
        public virtual int DeleteMedicine(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteMedicine", idParameter);
        }
    
        public virtual int DeleteOrder(Nullable<int> orderId)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("orderId", orderId) :
                new ObjectParameter("orderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteOrder", orderIdParameter);
        }
    }
}
