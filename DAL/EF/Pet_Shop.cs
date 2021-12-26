using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.EF
{
    public partial class Pet_Shop : DbContext
    {
        public Pet_Shop()
            : base("name=Pet_Shop")
        {
        }

        public virtual DbSet<Animals> Animals { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Order_line> Order_line { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Product_Type> Product_Type { get; set; }
        public virtual DbSet<Puck_Up_Point> Puck_Up_Point { get; set; }
        public virtual DbSet<Shopping_Basket> Shopping_Basket { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animals>()
                .Property(e => e.animal_name)
                .IsUnicode(false);

            modelBuilder.Entity<Animals>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Animals)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Animals>()
                .HasMany(e => e.Type)
                .WithRequired(e => e.Animals)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_name)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.customer_login)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Shopping_Basket)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.total_cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Order_line)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order_line>()
                .Property(e => e.order_line_cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.product_name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.cost)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.weight)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.picture)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_line)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Shopping_Basket)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Type>()
                .Property(e => e.type_name)
                .IsUnicode(false);

            modelBuilder.Entity<Product_Type>()
                .HasMany(e => e.Product)
                .WithRequired(e => e.Product_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Type>()
                .HasMany(e => e.Type)
                .WithRequired(e => e.Product_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Puck_Up_Point>()
                .Property(e => e.pick_up_point_name)
                .IsUnicode(false);

            modelBuilder.Entity<Status>()
                .Property(e => e.status_name)
                .IsUnicode(false);
        }
    }
}
