using Microsoft.EntityFrameworkCore;
using KalumManagement.Entities;
namespace KalumManagement
{
    public class KalumDBContext : DbContext
    {

        public DbSet<CarreraTecnica> CarreraTecnica { get; set; }
        public DbSet<Jornada> Jornada { get; set; }

        public DbSet<ExamenAdmision> ExamenAdmision { get; set; }

        public DbSet<Aspirante> Aspirante { get; set; }

        public DbSet<ResultadoExamenAdmision> ResultadoExamenAdmision {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("carreratecnica").HasKey(ct => new { ct.CarreraId });
            modelBuilder.Entity<Jornada>().ToTable("jornada").HasKey(j => new { j.JornadaId });
            modelBuilder.Entity<ExamenAdmision>().ToTable("examenadmision").HasKey(e => new { e.ExamenId });
            modelBuilder.Entity<Aspirante>().ToTable("aspirante").HasKey(a => new { a.NoExpediente });
            modelBuilder.Entity<ResultadoExamenAdmision>().ToTable("resultadoexamenadmision").HasKey(rs => new {rs.Nota});

            modelBuilder.Entity<Aspirante>()
            .HasOne<CarreraTecnica>(a => a.CarreraTecnica)
            .WithMany(ct => ct.Aspirantes)
            .HasForeignKey(a => a.CarreraId);

            modelBuilder.Entity<Aspirante>()
            .HasOne<Jornada>(a => a.Jornada)
            .WithMany(j => j.Aspirantes)
            .HasForeignKey(a => a.JornadaId);
            

            modelBuilder.Entity<Aspirante>()
            .HasOne<ExamenAdmision>(a => a.ExamenAdmision)
            .WithMany(ea => ea.Aspirantes)
            .HasForeignKey(a => a.ExamenId);


        }
        public KalumDBContext(DbContextOptions options) : base(options)
        {

        }

    }

}