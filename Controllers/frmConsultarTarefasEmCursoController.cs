using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks;

namespace iTasks.Controllers
{
    internal class frmConsultarTarefasEmCursoController
    {
        public static List<Tarefa> mostrarTarefasEmCurso()
        {
            using (var context = new ITaskContext())
            {
                return context.Tarefas
                    .Where(t => t.EstadoAtual != (EstadoAtual)2)
                    .ToList();
            }
        }
    }
}
