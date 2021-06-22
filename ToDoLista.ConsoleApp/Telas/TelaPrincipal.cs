using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Controladores;

namespace ToDoLista.ConsoleApp.Telas
{
    public class TelaPrincipal :TelaBase

    {
        ControladorCadastroContato controladorContato;
        ControladorCadastroTarefa controladorTarefa;

        TelaTarefa telaTarefa;
        TelaContato telaContato;

        public TelaPrincipal() : base("Tela Principal")
        {
            controladorContato = new ControladorCadastroContato();
            controladorTarefa = new ControladorCadastroTarefa();


            telaTarefa = new TelaTarefa(controladorTarefa);
            telaContato = new TelaContato(controladorContato);
            
        }
        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Contatos");
                Console.WriteLine("Digite 2 para o Cadastro de Tarefas");
                
                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = telaContato;

                if (opcao == "2")
                    telaSelecionada = telaTarefa;

               
                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2" && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }

    }
}
