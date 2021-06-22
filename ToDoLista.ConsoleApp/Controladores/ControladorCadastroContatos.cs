using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.MobileControls;

namespace ToDoLista.ConsoleApp.Controladores
{
    public class ControladorCadastroContato
    {
        public List<Contato> registros;
        public string enderecoDBTarefas =
               @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        SqlConnection conexaoComBanco = new SqlConnection();

        public ControladorCadastroContato() {

            registros = new List<Contato>();
        }

        public string InserirNovoContato(Contato contato)
        {
            string resultadoValidacao = contato.Validar();

            if (resultadoValidacao == "ESTA_VALIDO") {

                AbrirConexao();

                SqlCommand comandoInsercao = new SqlCommand();
                comandoInsercao.Connection = conexaoComBanco;

                string sqlInsercao =
                    @"INSERT INTO TBCONTATOS
	        (
	             NOME,
                 EMAIL,
                EMPRESA,
                TELEFONE,
                CARGO

	        ) 
	        VALUES 
	        (
	            @NOME, 
	            @EMAIL, 
	            @EMPRESA, 
	            @TELEFONE, 
	           @CARGO
	            );";

                sqlInsercao +=
                    @"SELECT SCOPE_IDENTITY();";

                comandoInsercao.CommandText = sqlInsercao;

                InserirNoBanco(contato, comandoInsercao);

                object id = comandoInsercao.ExecuteScalar();

                contato.Id = Convert.ToInt32(id);

                FecharConexao();
            }

            return resultadoValidacao;
        }

        private static void InserirNoBanco(Contato contato, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NOME", contato.Nome);
            comando.Parameters.AddWithValue("EMAIL", contato.Email);
            comando.Parameters.AddWithValue("EMPRESA", contato.Empresa);
            comando.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comando.Parameters.AddWithValue("CARGO", contato.Cargo);
        }

        public List<Contato> VisualizarContatoPorCargo()
        {
            AbrirConexao();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [NOME], 
		                [EMAIL], 
		                [EMPRESA] ,
	                    [TELEFONE],
                        [CARGO] 
                    FROM 
                        TBTAREFAS
                    GROUP BY [CARGO]";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = AdicionarContatosNaLista(leitorContatos);           

            FecharConexao();

            return contatos;

        }

        private static List<Contato> AdicionarContatosNaLista(SqlDataReader leitorContatos)
        {
            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);

                Contato c = new Contato(nome, email, empresa, telefone, cargo);
                c.Id = id;

                contatos.Add(c);
            }

            return contatos;
        }

        public bool ExcluirContato(Contato contato)
        {            

            AbrirConexao();

            SqlCommand comandoExclusao = new SqlCommand();
            comandoExclusao.Connection = conexaoComBanco;

            string sqlExclusao =
                @"DELETE FROM TBCONTATOS 	                
	                WHERE 
		                [ID] = @ID";

            comandoExclusao.CommandText = sqlExclusao;

            comandoExclusao.Parameters.AddWithValue("ID", contato.Id);

            comandoExclusao.ExecuteNonQuery();

            FecharConexao();

            return true;
        }

        public string AtualizarContato(Contato contato)
        {
            string resultadoValidacao = contato.Validar();

            if (resultadoValidacao == "ESTA_VALIDO")
            {
                AbrirConexao();

                SqlCommand comandoAtualizacao = new SqlCommand();
                comandoAtualizacao.Connection = conexaoComBanco;

                string sqlAtualizacao =
                    @"UPDATE TBCONTATOS
	        SET	
		    [NOME] = @NOME, 
		    [EMAIL]= @EMAIL, 
		    [EMPRESA] = @EMPRESA,
	        [TELEFONE] = @TELEFONE,
            [CARGO] = @CARGO
            

	        WHERE 
		    [ID] = @ID";

                comandoAtualizacao.CommandText = sqlAtualizacao;

                InserirNoBanco(contato, comandoAtualizacao);

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
