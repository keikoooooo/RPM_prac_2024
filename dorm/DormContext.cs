using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dorm;

public partial class DormContext : DbContext
{
    public DormContext()
    {
    }

    public DormContext(DbContextOptions<DormContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<ResponsiblePerson> ResponsiblePersons { get; set; }

    public virtual DbSet<SarcCampus> SarcCampuses { get; set; }

    public virtual DbSet<SarcRoom> SarcRooms { get; set; }

    public virtual DbSet<SarcStudent> SarcStudents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=calculator\\mssqlserver02;Database=dorm;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Floor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__floor__3213E83F1088239E");

            entity.ToTable("floor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FloorNumber).HasColumnName("floor_number");
            entity.Property(e => e.IdCampus).HasColumnName("id_campus");

            entity.HasOne(d => d.IdCampusNavigation).WithMany(p => p.Floors)
                .HasForeignKey(d => d.IdCampus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__floor__id_campus__45F365D3");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__post__3213E83FD576D1C5");

            entity.ToTable("post");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PostName)
                .HasMaxLength(50)
                .HasColumnName("post_name");
        });

        modelBuilder.Entity<ResponsiblePerson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__responsi__3213E83FAC67E5ED");

            entity.ToTable("responsible_persons");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.IdCampus).HasColumnName("id_campus");
            entity.Property(e => e.IdPost).HasColumnName("id_post");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");

            entity.HasOne(d => d.IdCampusNavigation).WithMany(p => p.ResponsiblePeople)
                .HasForeignKey(d => d.IdCampus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__responsib__id_ca__5165187F");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.ResponsiblePeople)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__responsib__id_po__52593CB8");
        });

        modelBuilder.Entity<SarcCampus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sarc_cam__3213E83FF94C460C");

            entity.ToTable("sarc_campus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.FloorsQuantity).HasColumnName("floors_quantity");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SarcRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sarc_roo__3213E83F43B2816E");

            entity.ToTable("sarc_room");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.IdCampus).HasColumnName("id_campus");
            entity.Property(e => e.IdFloor).HasColumnName("id_floor");
            entity.Property(e => e.NumRoom).HasColumnName("num_room");

            entity.HasOne(d => d.IdCampusNavigation).WithMany(p => p.SarcRooms)
                .HasForeignKey(d => d.IdCampus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sarc_room__id_ca__49C3F6B7");

            entity.HasOne(d => d.IdFloorNavigation).WithMany(p => p.SarcRooms)
                .HasForeignKey(d => d.IdFloor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sarc_room__id_fl__48CFD27E");
        });

        modelBuilder.Entity<SarcStudent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sarc_stu__3213E83FF391B2B9");

            entity.ToTable("sarc_student");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EarnedPoints)
                .HasDefaultValue(100)
                .HasColumnName("earned_points");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.NumCampus).HasColumnName("num_campus");
            entity.Property(e => e.NumContract).HasColumnName("num_contract");
            entity.Property(e => e.NumGroup).HasColumnName("num_group");
            entity.Property(e => e.NumRoom).HasColumnName("num_room");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
            entity.Property(e => e.StudLogin)
                .HasMaxLength(30)
                .HasColumnName("stud_login");
            entity.Property(e => e.StudPassword)
                .HasMaxLength(30)
                .HasColumnName("stud_password");
            entity.Property(e => e.StudentCard)
                .HasMaxLength(30)
                .HasColumnName("student_card");

            entity.HasOne(d => d.NumCampusNavigation).WithMany(p => p.SarcStudents)
                .HasForeignKey(d => d.NumCampus)
                .HasConstraintName("FK__sarc_stud__num_c__4D94879B");

            entity.HasOne(d => d.NumRoomNavigation).WithMany(p => p.SarcStudents)
                .HasForeignKey(d => d.NumRoom)
                .HasConstraintName("FK__sarc_stud__num_r__4E88ABD4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
