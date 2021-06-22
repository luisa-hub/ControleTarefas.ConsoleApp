using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp
{
    public class Tarefa
    {
        private int id;
        private string titulo;
        private DateTime dataCriação;
        private Nullable<DateTime> dataConclusão;
        private int percentualConcluído;
        private int opcao;
        private string prioridade;

        Dictionary<int, string> prioridades = new Dictionary<int, string>()
        {
            {1, "Alta"},
            {2, "Normal" },
            {3, "Baixa" }
        };



        public Tarefa(string titulo, DateTime dataCriação, Nullable<DateTime> dataConclusão, int percentualConcluído, int opcao)
        {
            Titulo = titulo;
            DataCriação = dataCriação;            
            PercentualConcluído = percentualConcluído;
            DataConclusão = dataConclusão;        
            this.opcao = opcao;
            DefinirPrioridade();
        }

        public Tarefa(string titulo, DateTime dataCriação, Nullable<DateTime> dataConclusão, int percentualConcluído, string prioridade)
        {
            Titulo = titulo;
            DataCriação = dataCriação;
            PercentualConcluído = percentualConcluído;
            DataConclusão = dataConclusão;
            this.Prioridade = prioridade;
        }

        public void DefinirPrioridade()
        {
            foreach (var item in prioridades)
            {
                if (item.Key == opcao)
                {
                    this.Prioridade = item.Value;
                    break;
                }
            }
        }
        public string Validar()
        {
            string resultadoValidacao = "ESTA_VALIDO";

            if (percentualConcluído < 0 || percentualConcluído > 100)
                resultadoValidacao += "A porcentagem é entre 0 e 100";

            if (opcao<1 || opcao>3)
                resultadoValidacao += "A opcao é 1, 2 ou 3";

            return resultadoValidacao;           


        }

        public int Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public DateTime DataCriação { get => dataCriação; set => dataCriação = value; }
        public Nullable<DateTime> DataConclusão { get => dataConclusão; set => dataConclusão = value; }
        public int PercentualConcluído { get => percentualConcluído; set => percentualConcluído = value; }
        public string Prioridade { get => prioridade; set => prioridade = value; }
    }
}
