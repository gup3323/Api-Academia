using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public int CPF { get; set; }
        public string Nome { get; set;}
        public string Email { get; set;}
        public string Telefone { get; set;}
        public string Endereco { get; set;}
        public string Password { get; set; }
        public string Role { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordKey { get; set; }

    }
}
