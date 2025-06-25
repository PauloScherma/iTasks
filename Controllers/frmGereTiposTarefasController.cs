using iTasks;
using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    internal class frmGereTiposTarefasController
    {
        public static void gravarDados(string descricao)
        {
            using (var context = new ITaskContext())
            {
                var tipoTarefa = new TipoTarefa
                {
                    Nome = descricao
                };
                context.TiposTarefa.Add(tipoTarefa);
                context.SaveChanges();
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
        public static void excluirTipoTarefa(int IdTipoTarefa)
        {
            using (var db = new ITaskContext())
            {
                //encontrar o TipoTarefa pelo ID
                var tipoTarefa = db.TiposTarefa.Find(IdTipoTarefa);
                if (tipoTarefa != null)
                {
                    //verifica se o tipoTarefa tem tarefas associadas
                    var tarefasAssociadas = db.Tarefas.Any(t => t.IdTipoTarefa.Id == IdTipoTarefa);
                    if (tarefasAssociadas)
                    {
                        MessageBox.Show("Não é possível excluir este Tipo de tarefa, pois esta tem tarefas associadas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //remover o tipo de tarefa do contexto
                    db.TiposTarefa.Remove(tipoTarefa);
                    db.SaveChanges();
                    MessageBox.Show("Tipo de Tarefa excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tipo de Tarefa não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
