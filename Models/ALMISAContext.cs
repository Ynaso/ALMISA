using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ALMISA.Models
{
    public partial class ALMISAContext : DbContext
    {
        public ALMISAContext()
        {
        }

        public ALMISAContext(DbContextOptions<ALMISAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Ciudade> Ciudades { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Productore> Productores { get; set; } = null!;
        public virtual DbSet<ProductosDelPedido> ProductosDelPedidos { get; set; } = null!;
        public virtual DbSet<Vendedore> Vendedores { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__8A3D240C445F7D36");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Ciudade>(entity =>
            {
                entity.HasKey(e => e.IdCiudad)
                    .HasName("PK__Ciudades__AEA2A71BBBB7ABFD");

                entity.Property(e => e.IdCiudad).HasColumnName("idCiudad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__885457EE4954B380");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.DireccionOficinaPrincipal)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccionOficinaPrincipal");

                entity.Property(e => e.IdCiudadOficinaPrincipal) // Esta es la clave foránea
                    .HasColumnName("idCiudadOficinaPrincipal");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.oCiudad)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdCiudadOficinaPrincipal) // Mapea a la clave foránea
                    .HasConstraintName("FK__Clientes__idCiud__4CA06362");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PK__Pedidos__A9F619B72BAF11B7");

                entity.Property(e => e.IdPedido).HasColumnName("idPedido");

                entity.Property(e => e.DireccionDestino)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccionDestino");

                entity.Property(e => e.EstadoActual)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("estadoActual");

                entity.Property(e => e.FechaEstimadaEntrega)
                    .HasColumnType("date")
                    .HasColumnName("fechaEstimadaEntrega");

                entity.Property(e => e.FechaRealEntrega)
                    .HasColumnType("date")
                    .HasColumnName("fechaRealEntrega");

                entity.Property(e => e.FechaRealizacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaRealizacion");

                entity.Property(e => e.IdCiudadDestino).HasColumnName("idCiudadDestino");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");

                entity.Property(e => e.PersonaQueRecibe)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("personaQueRecibe");

                entity.HasOne(d => d.oCiudad)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCiudadDestino)
                    .HasConstraintName("FK__Pedidos__idCiuda__534D60F1");

                entity.HasOne(d => d.oCliente)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__Pedidos__idClien__5441852A");

                entity.HasOne(d => d.oVendedor)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdVendedor)
                    .HasConstraintName("FK__Pedidos__idVende__52593CB8");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__07F4A1329A96846E");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.CodigoBarras)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("codigoBarras");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.IdProductor).HasColumnName("idProductor");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.PesoEnKilogramos)
                    .HasColumnType("decimal(6, 3)")
                    .HasColumnName("pesoEnKilogramos");

                entity.Property(e => e.PrecioUnitarioActual)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("precioUnitarioActual");

                entity.Property(e => e.PresentacionComercial)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("presentacionComercial");

                entity.Property(e => e.UnidadesEnBodega).HasColumnName("unidadesEnBodega");

                entity.HasOne(d => d.oCategoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Productos__idCat__48CFD27E");

                entity.HasOne(d => d.oProductor)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdProductor)
                    .HasConstraintName("FK__Productos__idPro__49C3F6B7");
            });

            modelBuilder.Entity<Productore>(entity =>
            {
                entity.HasKey(e => e.IdProductor)
                    .HasName("PK__Producto__A26E462C460FEBD6");

                entity.Property(e => e.IdProductor).HasColumnName("idProductor");

                entity.Property(e => e.DireccionOficinaPrincipal)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("direccionOficinaPrincipal");

                entity.Property(e => e.IdCiudadOficinaPrincipal).HasColumnName("idCiudadOficinaPrincipal");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.oCiudad)
                    .WithMany(p => p.Productores)
                    .HasForeignKey(d => d.IdCiudadOficinaPrincipal)
                    .HasConstraintName("FK__Productor__idCiu__45F365D3");
            });

            modelBuilder.Entity<ProductosDelPedido>(entity =>
            {
                entity.HasKey(e => e.IdPedidosProducto);  // Define la clave primaria

                entity.ToTable("ProductosDelPedido");

                entity.Property(e => e.CantidadUnidadesDespachadas).HasColumnName("cantidadUnidadesDespachadas");

                entity.Property(e => e.CantidadUnidadesRequeridas).HasColumnName("cantidadUnidadesRequeridas");

                entity.Property(e => e.IdPedido).HasColumnName("idPedido");

                entity.Property(e => e.IdProducto).HasColumnName("idProducto");

                entity.Property(e => e.PorcentajeDeDescuentoAplicado)
                    .HasColumnName("porcentajeDeDescuentoAplicado")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrecioUnitarioAplicado)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("precioUnitarioAplicado");

                entity.HasOne(d => d.oPedido)
                    .WithMany()
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Productos__idPed__571DF1D5");

                entity.HasOne(d => d.oProductos)
                    .WithMany()
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Productos__idPro__5812160E");
            });


            modelBuilder.Entity<Vendedore>(entity =>
            {
                entity.HasKey(e => e.IdVendedor)
                    .HasName("PK__Vendedor__A6693F9354406E1F");

                entity.Property(e => e.IdVendedor).HasColumnName("idVendedor");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("apellidos");

                entity.Property(e => e.DocumentoIdentidad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("documentoIdentidad");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fechaNacimiento");

                entity.Property(e => e.FechaVinculacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaVinculacion");

                entity.Property(e => e.IdCiudadBaseDeOperaciones).HasColumnName("idCiudadBaseDeOperaciones");

                entity.Property(e => e.IdVendedorJefe).HasColumnName("idVendedorJefe");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.SalarioBase)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("salarioBase");

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tipoDocumento");

                entity.HasOne(d => d.oCiudad)
                    .WithMany(p => p.Vendedores)
                    .HasForeignKey(d => d.IdCiudadBaseDeOperaciones)
                    .HasConstraintName("FK__Vendedore__idCiu__4F7CD00D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
