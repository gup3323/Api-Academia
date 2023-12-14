namespace Models
{
    public class ItemCarrinho
    {
        public int Id { get; set; }
        public int IdCarrinho { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public float Preco { get; set; }
        public string Avaliacao { get; set; }
        public Produto? Produto { get; set; }
        public Carrinho? Carrinho { get; set; }
    }
}
