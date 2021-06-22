using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoLista.ConsoleApp.Telas
{
    public interface ICadastravel
    {
        void InserirNovoRegistro();

        void EditarRegistro();

        void ExcluirRegistro();

        bool VisualizarRegistros();

        string ObterOpcao();
    }
}
