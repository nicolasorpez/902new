using IDGS902UT.API.Entities;
using IDGS902UT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IDGS902UT.API.DbContexts

{
    public class CityInfoDbContext : DbContext
    {
        //crear las tablas  d la db usando Obsets
        public DbSet<City>Cities { get; set; }
        public DbSet<PointOfInterest> PoinstOfInterest { get; set; }


        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Iguala de la Independencia", Description = "La bella calurosa!!!" },
                new City { Id = 2, Name = "Taxco de Alarcon", Description = "Ciudad Colonial" },
                new City { Id = 3, Name = "Acapulco de Juarez", Description = "La bahía más hermosa de México" }
            );

            //Sembrando los puntos de interes de iguala
            modelBuilder.Entity<PointOfInterest>().HasData(
                    new PointOfInterest { Id = 1, Name = "Aasta Monumental", Description = "La mas alta de Mexico", CityId = 1 },
                    new PointOfInterest { Id = 2, Name = "Laguna de Tuxpan", Description = "Pa' la cruda" , CityId = 1 },
                    new PointOfInterest { Id = 3, Name = "Centro Hitórico", Description = "Cuna de la Bandera Naciona", CityId = 1 }

                );
            }
        }
    }

