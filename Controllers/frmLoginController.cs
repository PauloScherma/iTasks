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
        public static bool usernameLogin(string username, string password)
        {
            using (var db = new ITaskContext())
            {
                
                var queryOneUser = from user in db.Utilizadores
                               where user.Username == username && user.Password == password
                               select user;

                foreach(Utilizador utilizador in queryOneUser)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
