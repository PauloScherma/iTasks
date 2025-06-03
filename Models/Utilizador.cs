using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    //definir colunas na tabela
    public abstract class Utilizador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            // Retorna o nome do utilizador para exibição na lista
            if (this is Gestor gestor)
            {
                return "" + gestor.Nome;
            }
            else if (this is Programador programador)
            {
                return "" + programador.Nome;
            }
            else
            {
                return "";
            }
        }
    }
}