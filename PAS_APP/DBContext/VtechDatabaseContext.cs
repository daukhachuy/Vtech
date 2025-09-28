using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PAS_APP.Models;

namespace PAS_APP.DBContext;

public partial class VtechDatabaseContext : DbContext
{
    public VtechDatabaseContext()
    {
    }

    public VtechDatabaseContext(DbContextOptions<VtechDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Filter> Filters { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=MSI;Database=VTech_DATABASE;User Id=sa;Password=huy123;TrustServerCertificate=True");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {

        IConfiguration config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", true, true)
              .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];
        return strConn;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Filter>(entity =>
        {
            entity.HasKey(e => e.FilterId).HasName("PK__Filter__3159DF6EDE9F5E9D");

            entity.ToTable("Filter");

            entity.Property(e => e.FilterId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CreateAT");
            entity.Property(e => e.FileName).HasMaxLength(1000);

            entity.HasOne(d => d.Service).WithMany(p => p.Filters)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Filter_Service");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__Form__FB05B7DD38A8F1EA");

            entity.ToTable("Form");

            entity.Property(e => e.FormId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CreateAT");
            entity.Property(e => e.Due).HasColumnType("datetime");
            entity.Property(e => e.Infor).HasMaxLength(1000);

            entity.HasOne(d => d.Student).WithMany(p => p.Forms)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK_FORM_STUDENT");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Notifica__1788CC4C86594D5F");

            entity.ToTable("Notification");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CreateAT");
            entity.Property(e => e.Detail).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.User).WithOne(p => p.Notification)
                .HasForeignKey<Notification>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notification_User");
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Package__322035CCFEBC99C6");

            entity.ToTable("Package");

            entity.Property(e => e.Detail).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(6, 3)");
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Service__C51BB00A5841B9F5");

            entity.ToTable("Service");

            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CreateAT");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B996B26C121");

            entity.ToTable("Student");

            entity.Property(e => e.CurrentAddress).HasMaxLength(200);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ethnicity).HasMaxLength(50);
            entity.Property(e => e.FatherCurrentAddress).HasMaxLength(200);
            entity.Property(e => e.FatherName).HasMaxLength(100);
            entity.Property(e => e.FatherOccupation).HasMaxLength(100);
            entity.Property(e => e.FatherPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.GpaThpt)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("GPA_THPT");
            entity.Property(e => e.Ielts)
                .HasMaxLength(20)
                .HasColumnName("IELTS");
            entity.Property(e => e.MotherCurrentAddress).HasMaxLength(200);
            entity.Property(e => e.MotherName).HasMaxLength(100);
            entity.Property(e => e.MotherOccupation).HasMaxLength(100);
            entity.Property(e => e.MotherPhone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.OtherCertificates).HasMaxLength(255);
            entity.Property(e => e.PermanentAddress).HasMaxLength(200);
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Toeic)
                .HasMaxLength(20)
                .HasColumnName("TOEIC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB9FD49BC");

            entity.Property(e => e.CompanyAddress).HasMaxLength(200);
            entity.Property(e => e.CompanyCode)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName).HasMaxLength(200);
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("CreateAT");
            entity.Property(e => e.DateBuyPackage).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FilterId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FormId)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PassWord)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName).HasMaxLength(100);

            entity.HasOne(d => d.Filter).WithMany(p => p.Users)
                .HasForeignKey(d => d.FilterId)
                .HasConstraintName("FK_User_Filter");

            entity.HasOne(d => d.Form).WithMany(p => p.Users)
                .HasForeignKey(d => d.FormId)
                .HasConstraintName("FK_User_Form");

            entity.HasOne(d => d.Package).WithMany(p => p.Users)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK_User_Package");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
