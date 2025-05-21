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
        public static bool userValidation(string username, string password)
        {
            using (var db = new ITaskContext())
            {
                var userValidation = db.Utilizadores.Any(user => user.Username == username && user.Password == password);

                return userValidation;
            }
        }
    }
}
