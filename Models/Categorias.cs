using System.Text.Json.Serialization;

namespace Models
{
    public class Categorias
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        [JsonIgnore]
        public IEnumerable<Produto>? Produtos { get; set; }
    }
}
