using System;
using Microsoft.EntityFrameworkCore;
using Payroll.Data.Entities;

namespace Payroll.Data
{
    public class PayrollContext : DbContext
    {
        public PayrollContext(DbContextOptions<PayrollContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<SalaryPayRate> SalaryPayRates { get; set; }
        public DbSet<CommissionPayRate> CommissionPayRates { get; set; }
        public DbSet<HourlyPayRate> HourlyPayRates { get; set; }
        public DbSet<EmployeeSale> EmployeeSales { get; set; }
        public DbSet<EmployeeHour> EmployeeHours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees").HasAlternateKey(x => x.SocialSecurityNumber);            
            modelBuilder.Entity<SalaryPayRate>().ToTable("SalaryPayRates");
            modelBuilder.Entity<CommissionPayRate>().ToTable("CommissionPayRates");
            modelBuilder.Entity<HourlyPayRate>().ToTable("HourlyPayRates");
            modelBuilder.Entity<EmployeeSale>().ToTable("EmployeeSales");
            modelBuilder.Entity<EmployeeHour>().ToTable("EmployeeHours");
        }
    }
}
