using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLista.ConsoleApp.Controladores;

namespace ToDoLista.ConsoleApp.Telas
{
    public class TelaTarefa: TelaBase, ICadastravel
    {
        private readonly ControladorCadastroTarefa controladorTarefa;

        public TelaTarefa(ControladorCadastroTarefa controladorTarefa) : base("Tarefas...")
        {
            this.controladorTarefa = controladorTarefa;
        }

        public void EditarRegistro()
        {
            ConfigurarTela("Editando tarefas...");

            bool temRegistros = VisualizarRegistros();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número da tarefa que deseja editar: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa = controladorTarefa.registros.Find(x => x.Id == id);

            if (tarefa == null)
            {
                ApresentarMensagem("Nenhuma tarefa foi encontrado com este número: " + id, TipoMensagem.Erro);
                EditarRegistro();
                return;
            }

            tarefa = ObterTarefa();

            string resultadoValidacao = controladorTarefa.AtualizarTarefa(tarefa);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                EditarRegistro();
            }
        }

        public void ExcluirRegistro()
        {
            ConfigurarTela("Excluindo tarefa...");

            bool temRegistros = VisualizarRegistrosPendentes();

            if (temRegistros == false)
                return;

            Console.Write("\nDigite o número do id da tarefa que deseja excluir: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Tarefa tarefa= controladorTarefa.registros.Find(x => x.Id == id);

            if (tarefa == null)
            {
                ApresentarMensagem("Nenhuma tarefa foi encontrado com este número: " + id, TipoMensagem.Erro);
                ExcluirRegistro();
                return;
            }


            bool conseguiuExcluir = controladorTarefa.ExcluirTarefa(tarefa);

            if (conseguiuExcluir)
                ApresentarMensagem("tarefa excluída com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem("Falha ao tentar excluir", TipoMensagem.Erro);
                ExcluirRegistro();
            }
        }

        public void InserirNovoRegistro()
        {

            Tarefa tarefa = ObterTarefa();

            string resultadoValidacao = controladorTarefa.InserirTarefa(tarefa);

            if (resultadoValidacao == "ESTA_VALIDO")
                ApresentarMensagem("Contato inserido com sucesso", TipoMensagem.Sucesso);
            else
            {
                ApresentarMensagem(resultadoValidacao, TipoMensagem.Erro);
                InserirNovoRegistro();
            }
        }

        public bool VisualizarRegistros()
        {
            ConfigurarTela("Visualizando tarefa...");

            List<Tarefa> tarefas = controladorTarefa.VisualizarTarefaConcluida();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Nenhuma tarefa cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Titulo", "Pocentagem");

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaoColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.PercentualConcluído);
            }

            return true;
        }

        public bool VisualizarRegistrosPendentes()
        {
            ConfigurarTela("Visualizando tarefa...");

            List<Tarefa> tarefas = controladorTarefa.VisualizarTarefaPendente();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem("Nenhuma tarefa cadastrado!", TipoMensagem.Atencao);
                return false;
            }

            string configuracaoColunasTabela = "{0,-10} | {1,-55} | {2,-35}";

            MontarCabecalhoTabela(configuracaoColunasTabela, "Id", "Titulo", "Pocentagem");

            foreach (Tarefa tarefa in tarefas)
            {
                Console.WriteLine(configuracaoColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.PercentualConcluído);
            }

            return true;

        }

        private Tarefa ObterTarefa()
        {
            Console.Write("Digite o título da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a data de criação: ");
           DateTime data = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite a data de conclusao: ");
            DateTime dataConclusao = DateTime.Parse(Console.ReadLine());

            Console.Write("Digite o percentual da tarefa: ");
            int percentual = Int32.Parse(Console.ReadLine());

            Console.Write("Digite a prioridade (1 - Alta, 2 - Média, 3 - Baixa ");
            int opcao = Int32.Parse(Console.ReadLine());

            return new Tarefa(titulo, data, dataConclusao, percentual, opcao);
        }
    }
}
