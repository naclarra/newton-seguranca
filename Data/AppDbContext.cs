using Microsoft.EntityFrameworkCore;
using NewtonSegurancaAPI.Models;

namespace NewtonSegurancaAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Avaliacao> Avaliacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed Usuarios
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario { Id = 1, Nome = "Admin", Email = "admin@loja.com", SenhaHash = "AQAAAAEAACcQAAAAE...[HASHED_PASSWORD]...", Role = "Admin", TokenRecuperacao = "SECRET_ADMIN_TOKEN_123" },
            new Usuario { Id = 42, Nome = "João Silva", Email = "joao@email.com", SenhaHash = "secret123", Role = "User", TokenRecuperacao = "TOKEN_JOAO_456" },
            new Usuario { Id = 43, Nome = "Maria Souza", Email = "maria@email.com", SenhaHash = "maria789", Role = "User", TokenRecuperacao = "TOKEN_MARIA_789" }
        );

        // Seed Produtos
        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = 1, Nome = "Notebook Gamer", Descricao = "Notebook potente para jogos", Preco = 5000 },
            new Produto { Id = 2, Nome = "Smartphone", Descricao = "Celular moderno", Preco = 2500 }
        );

        // Seed Pedidos
        modelBuilder.Entity<Pedido>().HasData(
            new Pedido { Id = 101, UsuarioId = 42, ValorTotal = 5000, Status = "Entregue" },
            new Pedido { Id = 102, UsuarioId = 43, ValorTotal = 2500, Status = "Processando" }
        );

        // Seed Avaliacoes
        modelBuilder.Entity<Avaliacao>().HasData(
            new Avaliacao { Id = 1, ProdutoId = 1, UsuarioNome = "Anônimo", Comentario = "Muito bom!", Nota = 5 }
        );
    }
}
