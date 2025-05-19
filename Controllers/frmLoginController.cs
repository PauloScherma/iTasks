using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks;

namespace iTasks
{
    public class frmLoginController
    {
        //verifica se o username e a palavra pass estão corretos
        public static bool userLogin(string username, string password)
        {
            using (var db = new ITaskContext())
            {
                var userValidation = db.Utilizadores.Any(user => user.Username == username && user.Password == password);

                return userValidation;
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

                var typeName = typeOfUser.GetType().BaseType.Name;

                return typeName;

            }
        }
    }
}
