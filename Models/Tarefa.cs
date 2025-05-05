using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        //chave estrangeira - Utilizador
        public Utilizador IdGestor { get; set; }
        //chave estrangeira - Utilizador
        public Utilizador IdProgramador { get; set; }
        public int OrdemExecucao { get; set; } //o que é esta ordem execucao???
        public string Descricao { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        //chave estrangeira - TipoTarefa
        public TipoTarefa IdTipoTarefa { get; set; }
        public string StoryPoints { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public int EstadoAtual { get; set; }
    }
}
