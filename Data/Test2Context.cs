using Test2.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Test2.Data
{
    public partial class Test2Context : DbContext
    {
        public Test2Context()
        {
        }

        public Test2Context(DbContextOptions<Test2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<Test> Tests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(App.Config.GetConnectionString("DataBase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Parameter>(entity =>
            {
                entity.Property(e => e.MeasuredValue).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ParameterName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.RequiredValue).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.Parameters)
                    .HasForeignKey(d => d.TestId)
                    .HasConstraintName("FK_Parameters_To_Tests");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.BlockName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.TestDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
