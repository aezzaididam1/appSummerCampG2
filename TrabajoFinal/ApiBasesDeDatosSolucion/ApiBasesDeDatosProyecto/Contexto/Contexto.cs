﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApiBasesDeDatosProyecto.Models; // Asegúrate de agregar este using

namespace ApiBasesDeDatosProyecto.Context
{
    public class Contexto : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<VistaClientesPaises> VistaClientesPaises { get; set; }
        public DbSet<ProAlmClientePorPaisDto> ProAlmClientePorPaisDtos { get; set; }
        public DbSet<MonitoringData> MonitoringDatas { get; set; }  // Agregar esta línea

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pais>().HasData(
                new Pais { Id = 1, Nombre = "España", Divisa = "USD", Iso3 = "ESP" },
                new Pais { Id = 2, Nombre = "Francia", Divisa = "EUR", Iso3 = "FRA" },
                new Pais { Id = 3, Nombre = "Italia", Divisa = "USD", Iso3 = "ITA" },
                new Pais { Id = 4, Nombre = "Albania", Divisa = "USD", Iso3 = "ALB" }
            );

            modelBuilder.Entity<Cliente>().HasData(
                new Cliente
                {
                    Id = 1,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    FechaNacimiento = new DateTime(1990, 1, 1),
                    PaisId = 1,
                    Empleo = "Delincuente",
                    Email = "amin1@gmail.com"
                },
                new Cliente
                {
                    Id = 2,
                    Nombre = "Maria",
                    Apellido = "Lopez",
                    FechaNacimiento = new DateTime(1985, 5, 23),
                    PaisId = 2,
                    Empleo = "Profesor",
                    Email = "amin2@gmail.com"
                },
                new Cliente
                {
                    Id = 3,
                    Nombre = "Carlos",
                    Apellido = "Gomez",
                    FechaNacimiento = new DateTime(1978, 11, 12),
                    PaisId = 3,
                    Empleo = "Abogado",
                    Email = "amin3@gmail.com"
                }
            );

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Pais)
                .WithMany(p => p.Clientes)
                .HasForeignKey(c => c.PaisId);

            modelBuilder.Entity<VistaClientesPaises>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VistaClientesPaises");
            });
        }
    }
}
