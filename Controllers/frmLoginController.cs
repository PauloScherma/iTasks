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
        //atribuição de dados
        public void addToDataBase()
        {
            using (var db = new ITaskContext())
            {

                db.SaveChanges();
            }
        }
    }
}
