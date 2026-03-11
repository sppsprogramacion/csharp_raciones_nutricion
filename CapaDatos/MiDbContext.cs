

using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Reflection.Emit;

namespace CapaDatos
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MiDbContext: DbContext
    {
        public MiDbContext()
       : base("name=MyConnectionString")
        {
        }

        public DbSet<DMenu> Menus { get; set; }
        public DbSet<DObservacionElaborada> ObservacionesElaborada { get; set; }
        public DbSet<DObservacionGeneral> ObservacionesGeneral { get; set; }
        public DbSet<DObservacionSolicitada> ObservacionesSolicitada { get; set; }
        public DbSet<DRacionElaborada> RacionesElaboradas { get; set; }
        public DbSet<DRacionElaboradaDetalles> RacionesElaboradasDetalles { get; set; }
        public DbSet<DRacionSolicitada> RacionesSolicitadas { get; set; }
        public DbSet<DRacionesSolicitadasDetalles> RacionesSolicitadasDetalles { get; set; }
        public DbSet<DRol> Roles { get; set; }
        public DbSet<DSap> Sap { get; set; }
        public DbSet<DSapMenu> SapMenu { get; set; }
        public DbSet<DTipoMenu> TiposMenu { get; set; }
        public DbSet<DUnidad> Unidades { get; set; }
        public DbSet<DUsuario> Usuarios { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //relaciones tipos_menu
            modelBuilder.Entity<DTipoMenu>()
                .HasRequired(t => t.menu)
                .WithMany()     //sin propiedad de navegación en DMenu
                .HasForeignKey(t => t.menu_id)
                .WillCascadeOnDelete(false);

            //relaciones usuraios
            modelBuilder.Entity<DUsuario>()
                .HasRequired(u => u.rol)
                .WithMany() // sin propiedad de navegación en DRol
                .HasForeignKey(u => u.rol_id)
                .WillCascadeOnDelete(false);

            //relaciones sap_menus
            modelBuilder.Entity<DSapMenu>()
                .HasRequired(u => u.sap)
                .WithMany() // sin propiedad de navegación en DSap
                .HasForeignKey(u => u.sap_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DSapMenu>()
                .HasRequired(u => u.tipo_menu)
                .WithMany() // sin propiedad de navegación en DTipoMenu
                .HasForeignKey(u => u.tipo_menu_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DSapMenu>()
                .HasRequired(u => u.unidad)
                .WithMany() // sin propiedad de navegación en DUnidad
                .HasForeignKey(u => u.unidad_id)
                .WillCascadeOnDelete(false);

            //relaciones raciones elaboradas
            modelBuilder.Entity<DRacionElaborada>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación en DUsuario
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);

            //relaciones raciones elaboradas detalles
            modelBuilder.Entity<DRacionElaboradaDetalles>()
               .HasRequired(u => u.racion_elaborada)
               .WithMany(r => r.raciones_elaboradas_detalles) // con propiedad de navegación en DRacionElaborada
               .HasForeignKey(u => u.racion_elaborada_id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionElaboradaDetalles>()
               .HasRequired(u => u.sap)
               .WithMany() // sin propiedad de navegación en DSap
               .HasForeignKey(u => u.sap_id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionElaboradaDetalles>()
                .HasRequired(u => u.unidad)
                .WithMany() // sin propiedad de navegación en DUnidad
                .HasForeignKey(u => u.unidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionElaboradaDetalles>()
                .HasRequired(u => u.tipo_menu)
                .WithMany() // sin propiedad de navegación en DTipoMenu
                .HasForeignKey(u => u.tipo_menu_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionElaboradaDetalles>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación en DUsuario
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);

            //relaciones observaciones elaborada
            modelBuilder.Entity<DObservacionElaborada>()
               .HasRequired(u => u.racion_elaborada)
               .WithMany(r => r.observaciones_elaborada)
               .HasForeignKey(u => u.racion_elaborada_id)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<DObservacionElaborada>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);

            //relaciones raciones solicitadas
            modelBuilder.Entity<DRacionSolicitada>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación en DUsuario
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);            

            //relaciones raciones solicitadas detalles

            modelBuilder.Entity<DRacionesSolicitadasDetalles>()
               .HasRequired(u => u.racion_solicitada)
               .WithMany(r => r.raciones_solicitadas_detalles) // sin propiedad de navegación en DRol
               .HasForeignKey(u => u.racion_solicitada_id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionesSolicitadasDetalles>()
               .HasRequired(u => u.sap)
               .WithMany() // sin propiedad de navegación en DRol
               .HasForeignKey(u => u.sap_id)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionesSolicitadasDetalles>()
                .HasRequired(u => u.unidad)
                .WithMany() // sin propiedad de navegación en DRol
                .HasForeignKey(u => u.unidad_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionesSolicitadasDetalles>()
                .HasRequired(u => u.tipo_menu)
                .WithMany() // sin propiedad de navegación en DRol
                .HasForeignKey(u => u.tipo_menu_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DRacionesSolicitadasDetalles>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación en DRol
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);

            

            //relaciones observaciones solicitada
            modelBuilder.Entity<DObservacionSolicitada>()
               .HasRequired(u => u.racion_solicitada)
               .WithMany(r => r.observaciones_solicitada)
               .HasForeignKey(u => u.racion_solicitada_id)
               .WillCascadeOnDelete(false);


            modelBuilder.Entity<DObservacionSolicitada>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);

            //relaciones observaciones elaborada
            modelBuilder.Entity<DObservacionGeneral>()
                .HasRequired(u => u.usuario)
                .WithMany() // sin propiedad de navegación
                .HasForeignKey(u => u.usuario_id)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
            
        }
    }
}
