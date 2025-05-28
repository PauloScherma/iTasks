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
    }
}
