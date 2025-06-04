using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks;

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
    }
}
