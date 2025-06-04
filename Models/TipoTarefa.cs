using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    //definir colunas na tabela
    public class TipoTarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public override string ToString()
        {
            // Retorna o nome do utilizador para exibição na lista
            if (this is TipoTarefa tipoTarefa)
            {
                return "" + tipoTarefa.Nome;
            }
            else
            {
                return "";
            }
        }
    }
}
