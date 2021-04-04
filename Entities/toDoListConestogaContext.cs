using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CondorHacks.Entities
{
    public partial class toDoListConestogaContext : DbContext
    {
        public toDoListConestogaContext()
        {
        }

        public toDoListConestogaContext(DbContextOptions<toDoListConestogaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessCode> AccessCode { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Assignment> Assignment { get; set; }
        public virtual DbSet<Difficulty> Difficulty { get; set; }
        public virtual DbSet<Length> Length { get; set; }
        public virtual DbSet<Levels> Levels { get; set; }
        public virtual DbSet<ProgramName> ProgramName { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SubjectProgram> SubjectProgram { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<UserAssignment> UserAssignment { get; set; }
        public virtual DbSet<UserElective> UserElective { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=34.68.241.90;Database=toDoListConestoga;User ID=sqlserver;Password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessCode>(entity =>
            {
                entity.HasKey(e => e.IdCode);

                entity.Property(e => e.IdCode)
                    .HasColumnName("idCode")
                    .ValueGeneratedNever();

                entity.Property(e => e.AccessCode1)
                    .IsRequired()
                    .HasColumnName("accessCode")
                    .HasMaxLength(10);

                entity.Property(e => e.NumberOfUsers).HasColumnName("numberOfUsers");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin);

                entity.Property(e => e.IdAdmin)
                    .HasColumnName("idAdmin")
                    .ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(70);

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(48);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Admin)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Admin_Roles");
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.IdAssignment);

                entity.Property(e => e.IdAssignment)
                    .HasColumnName("idAssignment")
                    .ValueGeneratedNever();

                entity.Property(e => e.Deadline).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.IdSubject).HasColumnName("idSubject");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.PercentOfFinalGrade).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.DifficultyNavigation)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.Difficulty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Difficulty");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Subjects");

                entity.HasOne(d => d.LengthNavigation)
                    .WithMany(p => p.Assignment)
                    .HasForeignKey(d => d.Length)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Assignment_Length");
            });

            modelBuilder.Entity<Difficulty>(entity =>
            {
                entity.HasKey(e => e.IdLevelOfDifficulty);

                entity.Property(e => e.IdLevelOfDifficulty)
                    .HasColumnName("idLevelOfDifficulty")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Length>(entity =>
            {
                entity.HasKey(e => e.IdLength);

                entity.Property(e => e.IdLength)
                    .HasColumnName("idLength")
                    .ValueGeneratedNever();

                entity.Property(e => e.Length1)
                    .IsRequired()
                    .HasColumnName("Length")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Levels>(entity =>
            {
                entity.HasKey(e => e.IdLevel);

                entity.Property(e => e.IdLevel)
                    .HasColumnName("idLevel")
                    .ValueGeneratedNever();

                entity.Property(e => e.LevelValue).HasColumnName("levelValue");
            });

            modelBuilder.Entity<ProgramName>(entity =>
            {
                entity.HasKey(e => e.IdProgram);

                entity.Property(e => e.IdProgram)
                    .HasColumnName("idProgram")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProgramName1)
                    .IsRequired()
                    .HasColumnName("programName")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.Property(e => e.IdRole)
                    .HasColumnName("idRole")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("roleName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SubjectProgram>(entity =>
            {
                entity.HasKey(e => e.IdSubjectProgram);

                entity.Property(e => e.IdSubjectProgram)
                    .HasColumnName("idSubjectProgram")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdProgram).HasColumnName("idProgram");

                entity.Property(e => e.IdSubject).HasColumnName("idSubject");

                entity.HasOne(d => d.IdProgramNavigation)
                    .WithMany(p => p.SubjectProgram)
                    .HasForeignKey(d => d.IdProgram)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectProgram_ProgramName");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.SubjectProgram)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubjectProgram_Subjects");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.IdSubject);

                entity.Property(e => e.IdSubject)
                    .HasColumnName("idSubject")
                    .ValueGeneratedNever();

                entity.Property(e => e.LevelSubject).HasColumnName("levelSubject");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.LevelSubjectNavigation)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.LevelSubject)
                    .HasConstraintName("FK_Subjects_Levels");
            });

            modelBuilder.Entity<UserAssignment>(entity =>
            {
                entity.HasKey(e => e.IdUserAssignment);

                entity.Property(e => e.IdUserAssignment)
                    .HasColumnName("idUserAssignment")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdAssignmentNavigation)
                    .WithMany(p => p.UserAssignment)
                    .HasForeignKey(d => d.IdAssignment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAssignment_Assignment");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserAssignment)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAssignment_Users");
            });

            modelBuilder.Entity<UserElective>(entity =>
            {
                entity.HasKey(e => e.IdUserElective);

                entity.Property(e => e.IdUserElective)
                    .HasColumnName("idUserElective")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.UserElective)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserElective_Subjects");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserElective)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserElective_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser)
                    .HasColumnName("idUser")
                    .ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("dateOfBirth")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.DateOfRegistration)
                    .HasColumnName("dateOfRegistration")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasColumnName("fullName")
                    .HasMaxLength(70);

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.LevelProg).HasColumnName("levelProg");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(48);

                entity.Property(e => e.Token)
                    .HasColumnName("token")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");

                entity.HasOne(d => d.LevelProgNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LevelProg)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Levels");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_ProgramName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
