using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks;

namespace iTasks.Controllers
{
    internal class frmDetalhesTarefaController
    {
        public static void gravarDados(Utilizador IdProgramador, int OrdemExecucao, string descricao, DateTime DataPrevistaInicio, DateTime DataPrevistaFim, TipoTarefa IdTipoTarefa, int StoryPoints)
        {
            using (var context = new ITaskContext())
            {
                // Buscar o TipoTarefa existente pelo Id
                var tipoTarefaExistente = context.TiposTarefa.Find(IdTipoTarefa.Id);
                var programadorExistente = context.Programadores.Find(IdProgramador.Id);

                var tarefa = new Tarefa
                {
                    IdProgramador = IdProgramador,
                    OrdemExecucao = OrdemExecucao,
                    Descricao = descricao,
                    DataPrevistaInicio = DataPrevistaInicio,
                    DataPrevistaFim = DataPrevistaFim,
                    IdTipoTarefa = tipoTarefaExistente, // Associar o objeto rastreado
                    StoryPoints = StoryPoints,
                    DataCriacao = DateTime.Now,
                    DataRealInicio = new DateTime (2000, 1, 1),
                    DataRealFim = new DateTime(2000, 1, 1),
                };

                context.Tarefas.Add(tarefa);
                context.SaveChanges();



            }
        }
        public static List<Programador> mostrarProgramadores()
        {
            using (var db = new ITaskContext())
            {
                // Mostrar na comboBox apenas os Programadores
                var allProgramadoresList = db.Programadores.ToList();
                // Retornar a lista
                return allProgramadoresList;
            }
        }
        public static List<TipoTarefa> mostrarTiposTarefas()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na comboBox os Tipos de Tarefas
                var allTipoTarefaList = db.TiposTarefa.ToList();
                //retornar a lista
                return allTipoTarefaList;
            }
        }
    }
}
