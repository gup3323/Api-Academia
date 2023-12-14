namespace Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string Descricao { get; set; }
        public int CategoriasId { get; set; }
        public Categorias? Categorias { get; set; }
    }
}
