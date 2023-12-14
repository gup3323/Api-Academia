using System.Text.Json.Serialization;

namespace Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public float ValorTotal { get; set; }
        public DateTime TransacaoData { get; set; }
        public string FormaPagamento { get; set; }
        public int IdCliente { get; set; }
        public int IdProduto { get; set; }
        public int QtdProduto { get; set; }
        [JsonIgnore]
        public string Avaliacao { get; set; } 
        public IEnumerable<Produto>? Produtos { get; set; }

    }
}
