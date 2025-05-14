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

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //atribuição de dados
            using (var db = new ITaskContext())
            {
                var admin = new Gestor { Nome = "admin", Username = "admin", Password = "admin", Departamento = Departamento.Administração, GereUtilizadores = true };
                db.Utilizadores.Add(admin);

                db.SaveChanges();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show(); 
            Application.Run();
        }
    }
}
