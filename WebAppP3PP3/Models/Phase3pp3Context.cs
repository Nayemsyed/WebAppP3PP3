using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppP3PP3.Models
{
    public partial class Phase3pp3Context : DbContext
    {
        public Phase3pp3Context()
        {
        }

        public Phase3pp3Context(DbContextOptions<Phase3pp3Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=DESKTOP-6COE67H;database=Phase3pp3;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__Classes__C1F8DC391B1E144A");

                entity.Property(e => e.Cid)
                    .ValueGeneratedNever()
                    .HasColumnName("CId");

                entity.Property(e => e.Cname)
                    .HasMaxLength(50)
                    .HasColumnName("CName");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.StuRollNo)
                    .HasName("PK__Student__7F54A86B07D210B6");

                entity.ToTable("Student");

                entity.Property(e => e.StuRollNo).ValueGeneratedNever();

                entity.Property(e => e.Cid).HasColumnName("CId");

                entity.Property(e => e.StuName).HasMaxLength(50);

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("FK__Student__CId__4D94879B");

                entity.HasOne(d => d.Subj)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.SubjId)
                    .HasConstraintName("FK__Student__SubjId__4E88ABD4");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubId)
                    .HasName("PK__Subjects__4D9BB84A9F356613");

                entity.Property(e => e.SubId).ValueGeneratedNever();

                entity.Property(e => e.SubName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
