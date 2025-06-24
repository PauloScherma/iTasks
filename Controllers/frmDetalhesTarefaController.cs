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
        public static void gravarDados(Utilizador IdProgramador, int OrdemExecucao, string descricao, DateTime DataPrevistaInicio, DateTime DataPrevistaFim, TipoTarefa IdTipoTarefa, int StoryPoints, Gestor gestorCriador)
        {
            using (var context = new ITaskContext())
            {
                var tipoTarefaExistente = context.TiposTarefa.Find(IdTipoTarefa.Id);
                var programadorExistente = context.Programadores.Find(IdProgramador.Id);
                var gestorExistente = context.Gestores.Find(gestorCriador.Id);
                var tarefaExistente = context.Tarefas.FirstOrDefault(t => t.Descricao == descricao);

                if (tarefaExistente == null)
                {
                    var tarefa = new Tarefa
                    {
                        IdProgramador = programadorExistente,
                        OrdemExecucao = OrdemExecucao,
                        Descricao = descricao,
                        DataPrevistaInicio = DataPrevistaInicio,
                        DataPrevistaFim = DataPrevistaFim,
                        IdTipoTarefa = tipoTarefaExistente,
                        StoryPoints = StoryPoints,
                        DataCriacao = DateTime.Now,
                        DataRealInicio = new DateTime(2000, 1, 1),
                        DataRealFim = new DateTime(2000, 1, 1),
                        IdGestor = gestorExistente // Preenche o gestor automaticamente
                    };

                    context.Tarefas.Add(tarefa);
                }
                else
                {
                    tarefaExistente.IdProgramador = programadorExistente;
                    tarefaExistente.OrdemExecucao = OrdemExecucao;
                    tarefaExistente.Descricao = descricao;
                    tarefaExistente.DataPrevistaInicio = DataPrevistaInicio;
                    tarefaExistente.DataPrevistaFim = DataPrevistaFim;
                    tarefaExistente.IdTipoTarefa = tipoTarefaExistente;
                    tarefaExistente.StoryPoints = StoryPoints;
                    tarefaExistente.DataCriacao = DateTime.Now;
                    tarefaExistente.DataRealInicio = new DateTime(2000, 1, 1);
                    tarefaExistente.DataRealFim = new DateTime(2000, 1, 1);
                    tarefaExistente.IdGestor = gestorCriador; // Atualiza o gestor
                }

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
        public static List<Programador> mostrarProgramadores(Gestor gestor)
        {
            using (var db = new ITaskContext())
            {
                // Filtra apenas os programadores associados ao gestor informado
                var programadoresDoGestor = db.Programadores
                    .Where(p => p.Gestor.Id == gestor.Id)
                    .ToList();
                return programadoresDoGestor;
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

        public static bool ExisteOrdemParaProgramador(Utilizador programador, int ordemExecucao)
        {
            using (var context = new ITaskContext())
            {
                return context.Tarefas
                    .Any(t => t.IdProgramador.Id == programador.Id && t.OrdemExecucao == ordemExecucao);
            }
        }
    }
}
