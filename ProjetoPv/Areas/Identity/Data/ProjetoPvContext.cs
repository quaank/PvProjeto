using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoPv.Models;
using System.Reflection.Emit;

namespace ProjetoPv.Areas.Identity.Data;

public class ProjetoPvContext : IdentityDbContext<ProjetoPvUser>
{
    public DbSet<Equipas> Equipas { get; set; }
    public DbSet<Treinadores> Treinadores { get; set; }

    public ProjetoPvContext(DbContextOptions<ProjetoPvContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations afterx calling base.OnModelCreating(builder);

      

        builder.Entity<Equipas>().HasData(
            new Equipas
            {
                Id = 1,
                Categoria = 2,
                Modalidade = Modalidades.Futebol,
                Nome = "Flamengo",
                TreinadoresId = 1,


            });
        builder.Entity<Equipas>().HasData(
            new Equipas
            {
                Id = 2,
                Categoria = 3,
                Modalidade = Modalidades.Futebol,
                Nome = "PSG",
                TreinadoresId = 2,


            });
        builder.Entity<Treinadores>().HasData(
            new Treinadores
            {
                Id  = 1,
                Nome = "Jose",
                Contacto = 919523432,
                Qualificacoes = "Muitas"
            }
            );
        builder.Entity<Treinadores>().HasData(
           new Treinadores
           {
               Id = 2,
               Nome = "Pedro",
               Contacto = 91952322,
               Qualificacoes = "Muitas"
           }
           );






    }

    public DbSet<ProjetoPv.Models.Competicoes>? Competicoes { get; set; }

    public DbSet<ProjetoPv.Models.Atletas>? Atletas { get; set; }

    public DbSet<ProjetoPv.Models.Treino>? Treino { get; set; }


}