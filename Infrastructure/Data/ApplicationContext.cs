using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Client> Clientes { get; set; }
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<PreVen> PrecioVenta { get; set; }
        public DbSet<Artic> Articulos { get; set; }
        public DbSet<PedWebCab> PedidosCab { get; set; }
        public DbSet<PedWebArt> PedidosDet { get; set; }
        public DbSet<BocEnt> BocaEntrega { get; set; }
        public DbSet<BonArtCli> BonificacionesArticuloCliente { get; set; }
        public DbSet<BonClaDet> BonificacionesClaseDetalle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo de tabla Clien
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Clien");
                entity.HasKey(e => e.CodCli)
                      .HasName("PK_Cliente");

                entity.Property(e => e.CodCli).HasColumnName("codcli");
                entity.Property(e => e.NomCli).HasColumnName("nomcli");
                entity.Property(e => e.SaldoDeuCc).HasColumnName("saldodeucc");
                entity.Property(e => e.SaldoVencCc).HasColumnName("saldovencc");
                entity.Property(e => e.LimiteCredito).HasColumnName("limcrecli");
                entity.Property(e => e.LimiteCreditoUso).HasColumnName("limcrecliuso");
            });

            // Mapeo de tabla sisusuar
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("sisusuar");
                entity.HasKey(e => e.CodUsr)
                      .HasName("PK_Usuario");

                entity.Property(e => e.CodUsr).HasColumnName("codusr");
                entity.Property(e => e.NomUsr).HasColumnName("nomusr");
                entity.Property(e => e.AdmUsr).HasColumnName("admusr");
                entity.Property(e => e.PwdUsr).HasColumnName("pwdusr");
                entity.Property(e => e.Inactivo).HasColumnName("inactivo");
                entity.Property(e => e.CodGrp).HasColumnName("codgrp");
            });

            // Mapeo de tabla deudeu
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("deudeu");
                entity.HasKey(e => e.CSIDcbtDeu)
                      .HasName("PK_Factura");  // RECORDAR QUE LA TABLA FACTURAS (deudeu) NO TIENE PK. HABLAR CON ARI.

                entity.Property(e => e.CSIDcbtDeu).HasColumnName("CSIDcbtdeu");
                entity.Property(e => e.FechaVto).HasColumnName("fecvtodeu");
                entity.Property(e => e.CodCli).HasColumnName("codcli");
                entity.Property(e => e.NroDocCli).HasColumnName("nrodoccli");
                entity.Property(e => e.CodTipoCbt).HasColumnName("codtipcbtdeu");
                entity.Property(e => e.CemCbt).HasColumnName("cemcbtdeu");
                entity.Property(e => e.NroCbt).HasColumnName("nrocbtdeu");
                entity.Property(e => e.FechaCbt).HasColumnName("feccbtdeu");
                entity.Property(e => e.CodCta).HasColumnName("codctadeu");
                entity.Property(e => e.CodMon).HasColumnName("codmon");
                entity.Property(e => e.ImpCot).HasColumnName("impcot");
                entity.Property(e => e.ImporteOriginal).HasColumnName("imporideu");
                entity.Property(e => e.Saldo).HasColumnName("saldodeu");
                entity.Property(e => e.Observaciones).HasColumnName("obsdeu");
                entity.Property(e => e.CodVen).HasColumnName("codven");
                entity.Property(e => e.CodZonaVta).HasColumnName("codzonvta");
                entity.Property(e => e.ImpIntCob).HasColumnName("impintcobdeu");
                entity.Property(e => e.CobInt).HasColumnName("cobintdeu");
                entity.Property(e => e.DifCot).HasColumnName("difcot");
                entity.Property(e => e.FechaCbtOri).HasColumnName("feccbtori");
                entity.Property(e => e.CodVen).HasColumnName("codven");
                entity.Property(e => e.CSIDci).HasColumnName("csidci");
                entity.Property(e => e.IntMora).HasColumnName("intmora");
                entity.Property(e => e.ImpIntDeu).HasColumnName("impintdeu");
                entity.Property(e => e.CodVen).HasColumnName("codven");
                entity.Property(e => e.NroCuo).HasColumnName("nrocuodeu");
                entity.Property(e => e.CSIDndDev).HasColumnName("CSIDnddev");
                entity.Property(e => e.ImpIf).HasColumnName("impifdeu");
                entity.Property(e => e.IvaIf).HasColumnName("ivaifdeu");
                entity.Property(e => e.FechaAnuPorCi).HasColumnName("fecanuporci");
                entity.Property(e => e.DtoRec).HasColumnName("dtorec");
                entity.Property(e => e.CodSuc).HasColumnName("codsuc");
                entity.Property(e => e.CodDimEle1).HasColumnName("coddimele1");
                entity.Property(e => e.FechaCasFlo).HasColumnName("feccasflo");
                entity.Property(e => e.ExcDeuFrgRec).HasColumnName("excdeufrgrec");
                entity.Property(e => e.ClaveEmp).HasColumnName("claveemp");
                entity.Property(e => e.EstadoCC).HasColumnName("estadocc");
            });



            // --- mapeo tabla PreVen ---
            modelBuilder.Entity<PreVen>(entity =>
            {
                entity.ToTable("PreVen");

                entity.HasKey(p => new { p.CodList, p.CodArt });

                entity.Property(p => p.CodArt)
                    .HasMaxLength(20); // ajustá según tu script SQL

                entity.Property(e => e.CodList).HasColumnName("CodList");
                entity.Property(e => e.CodArt).HasColumnName("CodArt");
                entity.Property(e => e.CodUniMed).HasColumnName("codunimed");
                entity.Property(e => e.CodMon).HasColumnName("CodMon");
                entity.Property(e => e.Precio).HasColumnName("Precio");
                entity.Property(e => e.FecUltMod).HasColumnName("FecUltMod");
                entity.Property(e => e.FecIng).HasColumnName("FecIng");
                entity.Property(e => e.UsrIng).HasColumnName("UsrIng");
                entity.Property(e => e.WksIng).HasColumnName("WksIng");
                entity.Property(e => e.FecExp).HasColumnName("fecexp");

                entity.HasOne(p => p.Articulo)
                    .WithMany(a => a.Precios)
                    .HasForeignKey(p => p.CodArt);
            });

            // --- mapeo tabla Artic ---
            modelBuilder.Entity<Artic>(entity =>
            {
                entity.ToTable("Artic");

                entity.HasKey(a => a.CodArt);

                entity.Property(a => a.CodArt)
                    .HasMaxLength(20);

                entity.Property(a => a.DesArt)
                    .HasMaxLength(100);

                entity.Property(e => e.CodArt).HasColumnName("CodArt");
                entity.Property(e => e.DesArt).HasColumnName("DesArt");
                entity.Property(e => e.Inactivo).HasColumnName("Inactivo");
            });

            // --- mapeo tabla PedWebCab ---
            modelBuilder.Entity<PedWebCab>(entity =>
            {
                entity.ToTable("PedWebCab");

                entity.HasKey(c => c.Csid);

                // PK calculada = concat(codtipcbt, cemcbt, nrocbt, codcli)
                entity.Property(c => c.Csid)
                    .HasMaxLength(50);

                entity.Property(c => c.CodTipCbt).HasMaxLength(5);
                entity.Property(c => c.CemCbt).HasMaxLength(5);
                entity.Property(c => c.NroCbt).HasMaxLength(20);


                entity.Property(e => e.CodTipCbt).HasColumnName("codtipcbt");
                entity.Property(e => e.CemCbt).HasColumnName("cemcbt");
                entity.Property(e => e.NroCbt).HasColumnName("nrocbt");
                entity.Property(e => e.Csid).HasColumnName("csid");
                entity.Property(e => e.FecCbt).HasColumnName("feccbt");
                entity.Property(e => e.FecEntCbt).HasColumnName("fecentcbt");
                entity.Property(e => e.CodCli).HasColumnName("codcli");
                entity.Property(e => e.CodIva).HasColumnName("codiva");
                entity.Property(e => e.CodSuc).HasColumnName("codsuc");
                entity.Property(e => e.CodList).HasColumnName("codlist");
                entity.Property(e => e.CodClaBon).HasColumnName("codclabon");
                entity.Property(e => e.CodVen).HasColumnName("codven");
                entity.Property(e => e.CodTra).HasColumnName("codtra");
                entity.Property(e => e.CodConVta).HasColumnName("codconvta");
                entity.Property(e => e.CodMon).HasColumnName("codmon");
                entity.Property(e => e.ImpCot).HasColumnName("impcot");
                entity.Property(e => e.ImpGraCbt).HasColumnName("impgracbt");
                entity.Property(e => e.PorDesCbt).HasColumnName("pordescbt");
                entity.Property(e => e.ImpDesCbt).HasColumnName("impdescbt");
                entity.Property(e => e.PorRecCbt).HasColumnName("porreccbt");
                entity.Property(e => e.ImpRecCbt).HasColumnName("impreccbt");
                entity.Property(e => e.ImpNetGraCbt).HasColumnName("impnetgracbt");
                entity.Property(e => e.ImpIvaCbt).HasColumnName("impivacbt");
                entity.Property(e => e.ImpTotCbt).HasColumnName("imptotcbt");
                entity.Property(e => e.ObsCbt).HasColumnName("obscbt");
                entity.Property(e => e.AutPed).HasColumnName("autped");
                entity.Property(e => e.UsrIngreso).HasColumnName("usringreso");
                entity.Property(e => e.FecIngreso).HasColumnName("fecingreso");
                entity.Property(e => e.UsrAut).HasColumnName("usraut");
                entity.Property(e => e.FecAut).HasColumnName("fecaut");
                entity.Property(e => e.NroBocEnt).HasColumnName("nrobocent");
                entity.Property(e => e.PedWebInc).HasColumnName("pedwebinc");
                entity.Property(e => e.ObsComunicado).HasColumnName("obscomunicado");
                entity.Property(e => e.FecPedWebInc).HasColumnName("fecpedwebinc");
                entity.Property(e => e.AutPedSec).HasColumnName("AutPedSec");
                entity.Property(e => e.UsrAutSec).HasColumnName("UsrAutSec");
                entity.Property(e => e.FecAutSec).HasColumnName("FecAutSec");
                entity.Property(e => e.Confirmado).HasColumnName("Confirmado");


                entity.HasMany(c => c.Detalles)
                    .WithOne(d => d.Pedido)
                    .HasForeignKey(d => d.Csid);
            });

            // --- mapeo tabla PedWebArt ---
            modelBuilder.Entity<PedWebArt>(entity =>
            {
                entity.ToTable("PedWebArt");

                entity.HasKey(d => new { d.Csid, d.Secuencia });

                entity.Property(d => d.CodArt).HasMaxLength(20);


                entity.Property(e => e.Csid).HasColumnName("csid");
                entity.Property(e => e.Secuencia).HasColumnName("secuencia");
                entity.Property(e => e.CodArt).HasColumnName("codart");
                entity.Property(e => e.CodIvaArt).HasColumnName("codivaart");
                entity.Property(e => e.CodClaBon).HasColumnName("codclabon");
                entity.Property(e => e.DesArtAmp).HasColumnName("desartamp");
                entity.Property(e => e.FecEntArt).HasColumnName("fecentart");
                entity.Property(e => e.CanArt).HasColumnName("canart");
                entity.Property(e => e.PreArt).HasColumnName("preart");
                entity.Property(e => e.ImpBonArt).HasColumnName("impbonart");
                entity.Property(e => e.ImpGraArt).HasColumnName("impgraart");
                entity.Property(e => e.ImpDesArt).HasColumnName("impdesart");
                entity.Property(e => e.ImpPrecArt).HasColumnName("imprecart");
                entity.Property(e => e.ImpNetGraArt).HasColumnName("impnetgraart");
                entity.Property(e => e.ImpIvaArt).HasColumnName("impivaart");


                entity.HasOne(d => d.Articulo)
                    .WithMany(a => a.PedidosDetalle)
                    .HasForeignKey(d => d.CodArt);
            });

            // Mapeo de tabla BocEnt
            modelBuilder.Entity<BocEnt>(entity =>
            {
                entity.ToTable("BocEnt");
                entity.HasKey(e => new { e.CodCli, e.NroBocEnt })
                      .HasName("PK_BocEnt");  // <-------- AVERIGUAR BIEN CUAL ES LA PK, la actual es la supuesta en base a estructura de tabla
                      

                entity.Property(e => e.CodCli).HasColumnName("codcli").IsRequired();
                entity.Property(e => e.NroBocEnt).HasColumnName("nrobocent").IsRequired(); ;
                entity.Property(e => e.NomBocEnt).HasColumnName("nombocent").HasMaxLength(50).IsRequired(); ;
                entity.Property(e => e.DomBocEnt).HasColumnName("dombocent").HasMaxLength(60).IsRequired(false); // nullable

            });


            // Mapeo de tabla BonArtCli

            modelBuilder.Entity<BonArtCli>(entity =>
            {
                entity.ToTable("BonArtCli");
                entity.HasKey(e => new { e.CodCli, e.CodArt })
                      .HasName("PK_BonArtCli");

                entity.Property(e => e.CodCli).HasColumnName("CodCli");
                entity.Property(e => e.CodArt).HasColumnName("CodArt");
                entity.Property(e => e.CodClaBon).HasColumnName("CodClaBon");
                entity.Property(e => e.Inactivo).HasColumnName("inactivo");

                // VER COMO SON LAS RELACIONES CON BONCLADET, PEDWEBCAB Y PEDWEBART (o si la relacion es con Artic y Clien)
            });

            // Mapeo de tabla BonClaDet

            modelBuilder.Entity<BonClaDet>(entity =>
            {
                entity.ToTable("BonClaDet");
                entity.HasKey(e => new { e.CodClaBon, e.Secuencia })
                      .HasName("PK_BonClaDet");

                entity.Property(e => e.CodClaBon).HasColumnName("CodClaBon");
                entity.Property(e => e.Secuencia).HasColumnName("Secuencia");
                entity.Property(e => e.TipEsc).HasColumnName("TipEsc").HasMaxLength(1).IsFixedLength();
                entity.Property(e => e.ValEscDes).HasColumnName("ValEscDes");
                entity.Property(e => e.ValEscHas).HasColumnName("ValEscHas");
                entity.Property(e => e.PorBonImp).HasColumnName("PorBonImp");
                entity.Property(e => e.PorBonCan).HasColumnName("PorBonCan");
                entity.Property(e => e.DisFac).HasColumnName("disfac").HasMaxLength(1).IsFixedLength();

                // VER COMO SON LAS RELACIONES CON BONARTCLI, PEDWEBCAB Y PEDWEBART (o si la relacion es con Artic y Clien)
            });
        }
    }
}

