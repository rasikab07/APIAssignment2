using System;
using System.Linq;
using APIAssignment2.Models.Entities;
using APIAssignment2.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace APIAssignment2.Data
{
    public class ManageEmployeesContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public ManageEmployeesContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Employee>().Property(s => s.DepartmentId).IsRequired();
            modelBuilder.Entity<Employee>().Property(s => s.Emp_Name).IsRequired().HasMaxLength(30);
            modelBuilder.Entity<Employee>().Property(s => s.Emp_Age);
            modelBuilder.Entity<Employee>().Property(s => s.Emp_Salary);

            modelBuilder.Entity<Employee>().HasOne(s => s.Department).WithMany(s => s.Employees).HasForeignKey(d=>d.DepartmentId);

            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Department>().Property(s => s.Dept_Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Department>().HasMany(s => s.Employees);
        }
    }
}
