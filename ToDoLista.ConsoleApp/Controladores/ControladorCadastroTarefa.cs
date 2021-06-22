using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp.Controladores
{
    public class ControladorCadastroTarefa
    {
        public List<Tarefa> registros;
        public string enderecoDBTarefas =
               @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        SqlConnection conexaoComBanco = new SqlConnection();

        public ControladorCadastroTarefa()
        {
            registros = new List<Tarefa>();
        }

        public string InserirTarefa(Tarefa tarefa)
        {
            string resultadoValidacao = tarefa.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                AbrirConexao();

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao =
                    @"INSERT INTO TBTAREFAS
	        (
	             TITULO, 
	             DATACRIACAO, 
	             DATACONCLUSAO, 
	             PERCENTUALCONCLUSAO, 
	             PRIORIDADE
	        ) 
	        VALUES 
	        (
	            @TITULO, 
	            @DATACRIACAO, 
	            @DATACONCLUSAO, 
	            @PERCENTUALCONCLUSAO, 
	            @PRIORIDADE
	            );";

                sqlInsercao +=
                    @"SELECT SCOPE_IDENTITY();";

                comandoInsercao.CommandText = sqlInsercao;

                comandoInsercao.Parameters.AddWithValue("Titulo", tarefa.Titulo);
                comandoInsercao.Parameters.AddWithValue("DataCriacao", tarefa.DataCriação);
                comandoInsercao.Parameters.AddWithValue("DataConclusao", tarefa.DataConclusão);
                comandoInsercao.Parameters.AddWithValue("PercentualConclusao", tarefa.PercentualConcluído);
                comandoInsercao.Parameters.AddWithValue("Prioridade", tarefa.Prioridade);

                object id = comandoInsercao.ExecuteScalar();

                tarefa.Id = Convert.ToInt32(id);

                FecharConexao();
            }

            return resultadoValidacao;
        }

        

        public List<Tarefa> VisualizarTarefaConcluida()
        {
            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [TITULO], 
		                [DATACRIACAO], 
		                [DATACONCLUSAO] ,
	                    [PERCENTUALCONCLUSAO],
                        [PRIORIDADE] 
                    FROM 
                        TBTAREFAS
                    WHERE
                        [PERCENTUALCONCLUSAO] = 100";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = AdicionarTarefas(leitorTarefas);

            
            FecharConexao();

            return tarefas;
        }


        public List<Tarefa> VisualizarTarefaPendente()
        {

            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                    [ID],
                    [TITULO], 
		            [DATACRIACAO], 
		            [DATACONCLUSAO] ,
	                [PERCENTUALCONCLUSAO],
                    [PRIORIDADE] 
                    FROM 
                        TBTAREFAS
                    WHERE
                        [PERCENTUALCONCLUSAO] < 100";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = AdicionarTarefas(leitorTarefas);

           
            FecharConexao();

            return tarefas;
        }

        private static List<Tarefa> AdicionarTarefas(SqlDataReader leitorTarefas)
        {
            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int percentualConclusao = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUSAO"]);
                string prioridade = Convert.ToString(leitorTarefas["PRIORIDADE"]);

                Tarefa t = new Tarefa(titulo, dataCriacao, dataConclusao, percentualConclusao, prioridade);
                t.Id = id;

                tarefas.Add(t);
            }

            return tarefas;
        }

        public bool ExcluirTarefa(Tarefa tarefa)       {


            AbrirConexao();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBTAREFAS 	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", tarefa.Id);

            comandoExclusao.ExecuteNonQuery();

            FecharConexao();

            return true;
        }

        public string AtualizarTarefa(Tarefa tarefa)
        {
            string resultadoValidacao = tarefa.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                AbrirConexao();

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao =
                    @"UPDATE TBTAREFAS 
	        SET	
		    [TITULO] = @TITULO, 
		    [DATACRIACAO]= @DATACRIACAO, 
		    [DATACONCLUSAO] = @DATACONCLUSAO,
	        [PERCENTUALCONCLUSAO] = @PERCENTUALCONSLUSAO,
            [PRIORIDADE] = @PRIORIDADE

	        WHERE 
		    [ID] = @ID";

                comandoAtualizacao.CommandText = sqlAtualizacao;

                comandoAtualizacao.Parameters.AddWithValue("Titulo", tarefa.Titulo);
                comandoAtualizacao.Parameters.AddWithValue("DataCriacao", tarefa.DataCriação);
                comandoAtualizacao.Parameters.AddWithValue("DataConclusao", tarefa.DataConclusão);
                comandoAtualizacao.Parameters.AddWithValue("PercentualConclusao", tarefa.PercentualConcluído);
                comandoAtualizacao.Parameters.AddWithValue("Prioridade", tarefa.Prioridade);

                comandoAtualizacao.ExecuteNonQuery();

                FecharConexao();
            }

            return resultadoValidacao;
        }

        private void FecharConexao()
        {
            conexaoComBanco.Close();
        }

        private void AbrirConexao()
        {
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();
        }
    }
}
