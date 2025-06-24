using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ////criar base de dados
            //using (var db = new ITaskContext())
            //{
            //    //definir admin
            //    var admin = new Gestor {
            //        Nome = "admin", 
            //        Username = "admin", 
            //        Password = "admin", 
            //        Departamento = Departamento.Administração, 
            //        GereUtilizadores = true 
            //    };
            //    db.Utilizadores.Add(admin);

            //    //difinir tipo tarefa
            //    var tipoTarefa = new TipoTarefa { 
            //        Nome = "Desenvolvimento" 
            //    };
            //    db.TiposTarefa.Add(tipoTarefa);

            //    //guardar as alterações
            //    db.SaveChanges();
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show(); 
            Application.Run();
        }
    }
}
