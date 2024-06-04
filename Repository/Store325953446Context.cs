using Microsoft.EntityFrameworkCore;

using Entities.Models;
using Microsoft.Extensions.Configuration;
namespace Repositories;

public partial class Store325953446Context : DbContext
{
    public IConfiguration _configuration;
    public Store325953446Context()
    {
    }

    public Store325953446Context(DbContextOptions<Store325953446Context> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder.UseSqlServer("Data Source=SRV2\\PUPILS;Initial Catalog = store_325953446;Integrated Security = True;Encrypt = False");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07C534E7DE");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC07F3A9A8BD");

            entity.Property(e => e.OrderDate).HasColumnType("date");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UserId__4F47C5E3");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderIte__3214EC07BF3801C9");

            entity.ToTable("OrderItem");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__531856C7");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__5224328E");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07D84F95A6");

            entity.Property(e => e.ProdDescription)
                .HasMaxLength(100)
                .HasColumnName("prodDescription");
            entity.Property(e => e.ProdImage)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.ProdName)
                .HasMaxLength(20)
                .IsFixedLength();

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__4BAC3F29");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07514739AD");

            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
