﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kerry.K3.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class K3DEVEntities : DbContext
    {
        public K3DEVEntities()
            : base("name=K3DEVEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ACTION_JOB> ACTION_JOB { get; set; }
        public DbSet<JOB> JOB { get; set; }
        public DbSet<JOBLINK> JOBLINK { get; set; }
    }
}
