using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProdutoCategoriaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        public string DescricaoProduto { get; set; }
        public string DescricaoCategoria { get; set; }
    }
}
