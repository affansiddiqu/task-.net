using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud_Operations_in_Asp.Net_Core.Models;

public partial class AssignmentDbContext : DbContext
{
    public AssignmentDbContext()
    {
    }

    public AssignmentDbContext(DbContextOptions<AssignmentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__tbl_Prod__C57059387705F878");

            entity.ToTable("tbl_Products");

            entity.Property(e => e.Pname).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_User__3214EC072696F8A2");

            entity.ToTable("tbl_Users");

            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasDefaultValue((byte)2);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
