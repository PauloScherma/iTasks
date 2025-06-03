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
        //public static event Action<string> TarefaCriada;


        public static void gravarDados(Utilizador IdProgramador, int OrdemExecucao, string descricao, DateTime DataPrevistaInicio, DateTime DataPrevistaFim, TipoTarefa IdTipoTarefa, int StoryPoints)
        {
            using (var context = new ITaskContext())
            {
                var tarefa = new Tarefa
                {
                    IdProgramador = IdProgramador,
                    OrdemExecucao = OrdemExecucao,
                    Descricao = descricao,
                    DataPrevistaInicio = DataPrevistaInicio,
                    DataPrevistaFim = DataPrevistaFim,
                    IdTipoTarefa = IdTipoTarefa,
                    StoryPoints = StoryPoints,
                };
                context.Tarefas.Add(tarefa);
                context.SaveChanges();

                //TarefaCriada.Invoke(descricao); // Notifica que a tarefa foi criada

            }
        }
    }
}
