using Microsoft.EntityFrameworkCore;
using Models;

namespace Academia.WebApi.Data
{
    public class AppAcademiaContext : DbContext 
    {
        public AppAcademiaContext(DbContextOptions options):base(options) { }
        public DbSet<Carrinho> Carrinho { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<ItemCarrinho> ItensCarrinho { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasData(
                    new Cliente
                    {
                        Id = 1,
                        Nome = "Pedro",
                        Password = "pedro123",
                        Role = "Cliente"
                    },
                    new Cliente
                    {
                        Id = 2,
                        Nome = "João",
                        Password = "joao123",
                        Role = "Admin"
                    }
                );

            modelBuilder.Entity<Categorias>()
                .HasData(new Categorias { Id = 1, Descricao = "Garrafinhas" }, new Categorias { Id = 2, Descricao = "Creatinas" }, new Categorias { Id = 3, Descricao = "Whey" });
        }






    }

}
