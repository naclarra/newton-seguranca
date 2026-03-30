namespace NewtonSegurancaAPI.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string TokenRecuperacao { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public List<Pedido> Pedidos { get; set; } = new();
}
