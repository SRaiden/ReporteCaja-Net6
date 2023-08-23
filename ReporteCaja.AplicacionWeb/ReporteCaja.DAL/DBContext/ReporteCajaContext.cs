using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReporteCaja.Entity;

namespace ReporteCaja.DAL.DBContext;

public partial class ReporteCajaContext : DbContext
{
    public ReporteCajaContext()
    {
    }

    public ReporteCajaContext(DbContextOptions<ReporteCajaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CajaCierresCaja> CajaCierresCajas { get; set; }

    public virtual DbSet<CajaCierresz> CajaCierreszs { get; set; }

    public virtual DbSet<CajaDetalleCierreCaja> CajaDetalleCierreCajas { get; set; }

    public virtual DbSet<CajaReporteVenta> CajaReporteVentas { get; set; }

    public virtual DbSet<CajaTicketsDiario> CajaTicketsDiarios { get; set; }

    public virtual DbSet<CajaTicketsNoCf> CajaTicketsNoCfs { get; set; }

    public virtual DbSet<CajaUsuario> CajaUsuarios { get; set; }

    public virtual DbSet<CajaVentaInsumo> CajaVentaInsumos { get; set; }

    public virtual DbSet<Configuracion> Configuracions { get; set; }

    public virtual DbSet<GeneralEmpresa> GeneralEmpresas { get; set; }

    public virtual DbSet<GeneralSucursale> GeneralSucursales { get; set; }

    public virtual DbSet<LicenciasUsuario> LicenciasUsuarios { get; set; }

    public virtual DbSet<VistaCierresCaja> VistaCierresCajas { get; set; }

    public virtual DbSet<VistaFichadasReporte> VistaFichadasReportes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("geopedidos")
            .UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<CajaCierresCaja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_cierres_caja");

            entity.ToTable("caja_cierres_caja", "geosoft");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diferencia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Hora).HasColumnType("datetime");
            entity.Property(e => e.InicioProximoTurno).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.TotalCtaCte).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalEfectivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalEgreso).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalIngreso).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTarjeta).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalVentas).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CajaCierresz>(entity =>
        {
            entity.ToTable("caja_cierresz", "geosoft");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.NcCantidadCanceladas).HasColumnName("NC_CantidadCanceladas");
            entity.Property(e => e.NcCantidadEmitidas).HasColumnName("NC_CantidadEmitidas");
            entity.Property(e => e.NcTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NC_Total");
            entity.Property(e => e.NcTotalExcento)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NC_TotalExcento");
            entity.Property(e => e.NcTotalGravado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NC_TotalGravado");
            entity.Property(e => e.NcTotalIva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NC_TotalIva");
            entity.Property(e => e.NcTotalNoGravado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NC_TotalNoGravado");
            entity.Property(e => e.NcTotalTributos)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("NC_TotalTributos");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.TicketsCantidadCancelados).HasColumnName("Tickets_CantidadCancelados");
            entity.Property(e => e.TicketsCantidadEmitidos).HasColumnName("Tickets_CantidadEmitidos");
            entity.Property(e => e.TicketsTotal)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tickets_Total");
            entity.Property(e => e.TicketsTotalExcento)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tickets_TotalExcento");
            entity.Property(e => e.TicketsTotalGravado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tickets_TotalGravado");
            entity.Property(e => e.TicketsTotalIva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tickets_TotalIva");
            entity.Property(e => e.TicketsTotalNoGravado)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tickets_TotalNoGravado");
            entity.Property(e => e.TicketsTotalTributos)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Tickets_TotalTributos");
        });

        modelBuilder.Entity<CajaDetalleCierreCaja>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_detalle_cierre_caja");

            entity.ToTable("caja_detalle_cierre_caja", "geosoft");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<CajaReporteVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_venta_productos");

            entity.ToTable("caja_reporte_ventas", "geosoft");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(10, 5)");
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.KilosProducto).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalKilos).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CajaTicketsDiario>(entity =>
        {
            entity.ToTable("caja_tickets_diarios", "geosoft");

            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalDelivery).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalMostrador).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<CajaTicketsNoCf>(entity =>
        {
            entity.ToTable("caja_tickets_no_cf", "geosoft");

            entity.Property(e => e.Cuit)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaCaja).HasColumnType("datetime");
            entity.Property(e => e.Iva).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Letra)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<CajaUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_usuarios");

            entity.ToTable("caja_usuarios", "geosoft");

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
        });

        modelBuilder.Entity<CajaVentaInsumo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("caja_venta_insumos", "geosoft");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.FactorUnidad).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SubCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 3)");
        });

        modelBuilder.Entity<Configuracion>(entity =>
        {
            entity.ToTable("Configuracion", "geosoft");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Propiedad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("propiedad");
            entity.Property(e => e.Recurso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("recurso");
            entity.Property(e => e.Valor)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<GeneralEmpresa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_empresas");

            entity.ToTable("general_empresas", "geosoft");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ColorFondo).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ColorFuente).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MailAvisoPedido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Mail_Aviso_Pedido");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Empresa");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RgbFondo)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("rgbFondo");
            entity.Property(e => e.RgbFuente)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("rgbFuente");
        });

        modelBuilder.Entity<GeneralSucursale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_sucursales");

            entity.ToTable("general_sucursales", "geosoft");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Alias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CodigoArea)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CostoEnvio).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.Domicilio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.FechaLimiteConexionesVencidas).HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.HorarioId).HasColumnName("Horario_Id");
            entity.Property(e => e.ListaPreciosId).HasColumnName("ListaPrecios_Id");
            entity.Property(e => e.Localidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Nombre_Sucursal");
            entity.Property(e => e.NumeroSucursal).HasColumnName("Numero_Sucursal");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UltimoPago).HasColumnType("datetime");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LicenciasUsuario>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("licencias_usuarios", "geosoft");

            entity.Property(e => e.Codigo).ValueGeneratedNever();
            entity.Property(e => e.Contraseña)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VistaCierresCaja>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vistaCierresCaja", "geosoft");

            entity.Property(e => e.EmpresaId).HasColumnName("Empresa_Id");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Hora).HasColumnType("datetime");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreSucursal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_Id");
            entity.Property(e => e.TotalCtaCte).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalEfectivo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalEgreso).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalIngreso).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTarjeta).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalVentas).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VistaFichadasReporte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vista_Fichadas_Reporte", "geosoft");

            entity.Property(e => e.ApellidoNombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.FechaOperativa).HasColumnType("date");
            entity.Property(e => e.Hora).HasColumnType("datetime");
            entity.Property(e => e.HorasNetas).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Motivo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
