using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace bison_api.Model
{
    public partial class bisonContext : DbContext
    {
        public bisonContext()
        {
        }

        public bisonContext(DbContextOptions<bisonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__rol__6ABCB5E00977FACA");

                entity.ToTable("rol", "cat");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_alta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_mod")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__4E3E04AD43167918");

                entity.ToTable("usuario", "seg");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasColumnName("activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ApMaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ap_materno");

                entity.Property(e => e.ApPaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ap_paterno");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaAlta)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_alta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaMod)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_mod")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nick");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pass");

                entity.Property(e => e.RutaImagen)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ruta_imagen")
                    .HasDefaultValueSql("('assets/user_image_default.png')");

                entity.Property(e => e.Theme)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("theme");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
