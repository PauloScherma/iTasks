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
            //criar base de dados
            using (var db = new ITaskContext())
            {
                ////definir admin
                //var admin = new Gestor { Nome = "admin", Username = "admin", Password = "admin", Departamento = Departamento.Administração, GereUtilizadores = true };
                //db.Utilizadores.Add(admin);
                ////definir gestores
                //var paulo = new Gestor { Nome = "paulo", Username = "paulo", Password = "paulo", Departamento = Departamento.Administração, GereUtilizadores = false };
                //db.Utilizadores.Add(paulo);
                //definir programadores
                //var gestorDoProgramador = db.Gestores.FirstOrDefault(g => g.Username == "admin");
                //// como definir o id do gestor do programador?
                //var miguel = new Programador { Nome = "miguel", Username = "miguel", Password = "miguel", NivelExperiencia = NivelExperiencia.Junior, Gestor = gestorDoProgramador };
                //db.Utilizadores.Add(miguel);

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
