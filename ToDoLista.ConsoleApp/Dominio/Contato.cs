using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp
{
    public class Contato
    {
        private int id;
        private string nome;
        private string email;
        private string empresa;
        private string telefone;
        private string cargo;

        public Contato( string nome, string email, string empresa, string telefone, string cargo)
        {
            
            this.Nome = nome;
            this.Email = email;
            this.Empresa = empresa;
            this.Telefone = telefone;
            this.Cargo = cargo;
        }

        public string Validar()
        {
            string resultadoValidacao = "ESTA_VALIDO";

            if (!Email.Contains("@") && Email.Length > 40)
                resultadoValidacao += "Email inválido";

            if(Telefone.Length > 10)
                resultadoValidacao += "Telefone Inválido";
            return resultadoValidacao;

        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Empresa { get => empresa; set => empresa = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Cargo { get => cargo; set => cargo = value; }
    }
}
