using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using RSACore;

#nullable disable

namespace Web_API_Prueba.Models
{
    public partial class DB_PRUEBA_BLAZORContext : DbContext
    {
        public DB_PRUEBA_BLAZORContext()
        {
        }

        public DB_PRUEBA_BLAZORContext(DbContextOptions<DB_PRUEBA_BLAZORContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblRusuario> TblRusuarios { get; set; }

        public static IConfiguration Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

                var rsa = new RSAImpl(builder);

                Configuration = builder.Build();

                optionsBuilder.UseSqlServer(rsa.LoadAndDecrypt(Configuration.GetConnectionString("DefaultConnection"), @"C:\Temp\crypt.key"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblRusuario>(entity =>
            {
                entity.HasKey(e => e.UsuNid)
                    .HasName("PK__TBL_RUSU__BD23EBCA8D844C76");

                entity.ToTable("TBL_RUSUARIO");

                entity.Property(e => e.UsuNid).HasColumnName("USU_NID");

                entity.Property(e => e.UsuCapellido)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USU_CAPELLIDO");

                entity.Property(e => e.UsuCdireccion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USU_CDIRECCION");

                entity.Property(e => e.UsuCdocumento)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("USU_CDOCUMENTO");

                entity.Property(e => e.UsuCnombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("USU_CNOMBRE");

                entity.Property(e => e.UsuCtelefono)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("USU_CTELEFONO");

                entity.Property(e => e.UsuDfechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("USU_DFECHA_CREACION");

                entity.Property(e => e.UsuOestado).HasColumnName("USU_OESTADO");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
