using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace webApi.Models
{
    public partial class dbempresaContext : DbContext
    {
        public dbempresaContext()
        {
        }

        public dbempresaContext(DbContextOptions<dbempresaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banco> Bancos { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Cotizacione> Cotizaciones { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Reserva> Reservas { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.Idbancos)
                    .HasName("PRIMARY");

                entity.ToTable("bancos");

                entity.Property(e => e.Idbancos).HasColumnName("idbancos");

                entity.Property(e => e.Banco1)
                    .HasMaxLength(45)
                    .HasColumnName("banco");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("PRIMARY");

                entity.ToTable("categorias");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Categoria1)
                    .HasMaxLength(45)
                    .HasColumnName("categoria");
            });

            modelBuilder.Entity<Cotizacione>(entity =>
            {
                entity.HasKey(e => e.Idcotizaciones)
                    .HasName("PRIMARY");

                entity.ToTable("cotizaciones");

                entity.HasIndex(e => e.Idproducto, "cotizaciones_fk_productos_idx");

                entity.Property(e => e.Idcotizaciones).HasColumnName("idcotizaciones");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(45)
                    .HasColumnName("cliente");

                entity.Property(e => e.Estado)
                    .HasMaxLength(45)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaCotizacion)
                    .HasMaxLength(45)
                    .HasColumnName("fecha_cotizacion");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.Idproducto)
                    .HasConstraintName("cotizaciones_fk_productos");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PRIMARY");

                entity.ToTable("productos");

                entity.HasIndex(e => e.Idcategoria, "producto_fk_categorias_idx");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(45)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Existencia).HasColumnName("existencia");

                entity.Property(e => e.FechaIng)
                    .HasMaxLength(45)
                    .HasColumnName("fecha_ing");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Precio)
                    .HasPrecision(8, 2)
                    .HasColumnName("precio");

                entity.Property(e => e.Producto1)
                    .HasMaxLength(45)
                    .HasColumnName("producto");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("producto_fk_categorias");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.Idreservas)
                    .HasName("PRIMARY");

                entity.ToTable("reservas");

                entity.HasIndex(e => e.Idbanco, "reservas_fk_bancos_idx");

                entity.HasIndex(e => e.Idproducto, "reservas_fk_productos_idx");

                entity.Property(e => e.Idreservas).HasColumnName("idreservas");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(45)
                    .HasColumnName("cliente");

                entity.Property(e => e.Estado)
                    .HasMaxLength(45)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaFin)
                    .HasMaxLength(45)
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaIni)
                    .HasMaxLength(45)
                    .HasColumnName("fecha_ini");

                entity.Property(e => e.FechaReserva)
                    .HasMaxLength(45)
                    .HasColumnName("fecha_reserva");

                entity.Property(e => e.Idbanco).HasColumnName("idbanco");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.HasOne(d => d.IdbancoNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.Idbanco)
                    .HasConstraintName("reservas_fk_bancos");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.Idproducto)
                    .HasConstraintName("reservas_fk_productos");
            });
        }
    }
}
