using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{   
    //definição do enum
    public enum EstadoAtual
    {
        ToDo, 
        Doing,
        Done
    }


    //definir colunas na tabela
    public class Tarefa
    {
        public int Id { get; set; }
        //chave estrangeira - Utilizador
        public Utilizador IdGestor { get; set; }
        //chave estrangeira - Utilizador
        public Utilizador IdProgramador { get; set; }
        public int OrdemExecucao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        //chave estrangeira - TipoTarefa
        public TipoTarefa IdTipoTarefa { get; set; }
        public int StoryPoints { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public EstadoAtual EstadoAtual { get; set; }

        public DateTime DataRealFim { get; set; }

        public override string ToString()
        {
            // Retorna o nome do utilizador para exibição na lista
            if (this is Tarefa tarefa)
            {
                return "" + tarefa.Descricao;
            }
            else
            {
                return "";
            }
        }
    }

}
