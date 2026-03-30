namespace NewtonSegurancaAPI.Models;

public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public List<Avaliacao> Avaliacoes { get; set; } = new();
}

public class Pedido
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataPedido { get; set; } = DateTime.Now;
    public string Status { get; set; } = "Pendente";
    public decimal ValorTotal { get; set; }
}

public class Avaliacao
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
    public string Comentario { get; set; } = string.Empty;
    public int Nota { get; set; }
}
