using iTasks;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controllers
{
    internal class frmConsultarTarefasConcluidasController
    {
        //Devine o que vai ser mostrado na tarefa "SemiModelo"
        internal class TarefaConcluidas
        {
            public int Id { get; set; }
            public string Descricao { get; set; }
            public Programador Nome { get; set; }
            public int OrdemExecucao { get; set; }
            public DateTime DataPrevistaInicio { get; set; }
            public DateTime DataPrevistaFim { get; set; }
            public DateTime DataRealInicio { get; set; }
            public DateTime DataCriacao { get; set; }
            public DateTime DataRealFim { get; set; }
            public double TempoEmDias => (DataRealFim - DataRealInicio).TotalDays;
            public double TempoPrevisto => (DataPrevistaFim - DataPrevistaInicio).TotalDays;
        }

        //motras  tarefas concluídas por programador passado por parâmetro
        public static List<TarefaConcluidas> mostrarTarefasConcluidas(int idProgramador)
        {
            using (var context = new ITaskContext())
            {
                return context.Tarefas
                    .Where(t => t.EstadoAtual == (EstadoAtual)2 && t.IdProgramador.Id == idProgramador)
                    .Select(t => new TarefaConcluidas
                    {
                        Id = t.Id,
                        Nome = t.IdProgramador,
                        Descricao = t.Descricao,
                        OrdemExecucao = t.OrdemExecucao,
                        DataCriacao = t.DataCriacao,
                        DataPrevistaInicio = t.DataPrevistaInicio,
                        DataPrevistaFim = t.DataPrevistaFim,
                        DataRealInicio = t.DataRealInicio,
                        DataRealFim = t.DataRealFim
                    })
                    .ToList();
            }
        }
        //mostra tarefas concluídas por gestor passado por parâmetro
        public static List<TarefaConcluidas> mostrarTarefasConcluidasPorProgramador(int idGestor)
        {
            using (var context = new ITaskContext())
            {
                return context.Tarefas
                    .Where(t => t.EstadoAtual == (EstadoAtual)2 && t.IdGestor.Id == idGestor)
                    .Select(t => new TarefaConcluidas
                    {
                        Id = t.Id,
                        Nome = t.IdProgramador,
                        Descricao = t.Descricao,
                        OrdemExecucao = t.OrdemExecucao,
                        DataCriacao = t.DataCriacao,
                        DataPrevistaInicio = t.DataPrevistaInicio,
                        DataPrevistaFim = t.DataPrevistaFim,
                        DataRealInicio = t.DataRealInicio,
                        DataRealFim = t.DataRealFim
                    })
                    .ToList();
            }
        }
    }
}
