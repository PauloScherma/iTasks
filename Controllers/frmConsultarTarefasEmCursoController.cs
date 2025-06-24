using iTasks;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTasks.Controllers.frmConsultarTarefasConcluidasController;

namespace iTasks.Controllers
{
    //Devine o que vai ser mostrado na tarefa "SemiModelo"
    internal class TarefaEmCurso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Programador NomeProgramador { get; set; }
        public int OrdemExecucao { get; set; }
        public DateTime DataPrevistaInicio { get; set; }
        public DateTime DataPrevistaFim { get; set; }
        public DateTime DataRealInicio { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataRealFim { get; set; }
        public double TempoEmFalta => (DataPrevistaFim - DataRealInicio).TotalDays;
    }
    internal class TarefaEstimada
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double TempoEstimado { get; set; }
    }

    internal class frmConsultarTarefasEmCursoController
    {
        public static List<TarefaEmCurso> mostrarTarefasEmCurso(int idGestor)
        {
            {
                using (var db = new ITaskContext())
                {
                    //Mostra a lista na GridView

                    return db.Tarefas
                        .Where(t => t.EstadoAtual != (EstadoAtual)2 && t.IdGestor.Id == idGestor)
                        .Select(t => new TarefaEmCurso
                        {
                            Id = t.Id,
                            NomeProgramador = t.IdProgramador,
                            Descricao  = t.Descricao,
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

        public static List<TarefaEstimada> mostrarTarefasEmCursoAnalise()
        {
            using (var db = new ITaskContext())
            {
                // Pega as tarefas concluídas 
                var tarefasConcluidas = db.Tarefas
                .Where(t => t.EstadoAtual == (EstadoAtual)2)
                .ToList();

                // Agrupar por Story Points e calcular a média de horas
                var mediasPorStoryPoint = tarefasConcluidas
                .GroupBy(t => t.StoryPoints)
                .ToDictionary(
                    g => g.Key, //story points
                    g => g.Average(t =>(t.DataRealFim - t.DataRealInicio).TotalHours) //media horas
                );

                // Filtrar as tarefas em curso do programador
                var tarefasEmCurso = db.Tarefas
                .Where(t => t.EstadoAtual != (EstadoAtual)2)
                .ToList();

                //Mostra a lista na GridView
                return db.Tarefas
                    .Where(t => t.EstadoAtual != (EstadoAtual)2)
                    .ToList() //busca e materializa na memoria uma lista de tarefas para que o select possa trabalhar nessa lista
                    .Select(t =>
                    {
                        // Calcular o tempo em falta para cada tarefa em curso
                        double estimativa = mediasPorStoryPoint.TryGetValue(t.StoryPoints, out double media)
                            ? media
                            : 2.0; // valor padrão

                        return new TarefaEstimada
                        {
                            Id = t.Id,
                            Descricao = t.Descricao,
                            TempoEstimado = estimativa
                        };
                    })
                    .ToList();           
            }
        }
    }
}
