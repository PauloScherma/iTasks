using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks;

namespace iTasks.Controllers
{
    class frmKanbanController
    {

        public static Gestor gestorLogedIn(string username)
        {
            //pega no utilizador que está logado
            using (var db = new ITaskContext())
            {
                //ver melhor tipo
                var gestorLogedIn = db.Gestores
                    .Where(u => u.Username == username)
                    .FirstOrDefault();
                
                return gestorLogedIn;
            }
        }
        public static Programador programadorLogedIn(string username)
        {
            //pega no utilizador que está logado
            using (var db = new ITaskContext())
            {
                //ver melhor tipo
                var programadorLogedIn = db.Programadores
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                return programadorLogedIn;
            }
        }
        //verifica qual o tipo do utilizador (Gestor ou Programador)
        public static string typeOfUser(string username, string password)
        {
            using (var db = new ITaskContext())
            {
                var typeOfUser = (from user in db.Utilizadores
                                  where user.Username == username && user.Password == password
                                  select user).FirstOrDefault();

                var typeName = typeOfUser.GetType().Name;

                return typeName;

            }
        }
    }
}
