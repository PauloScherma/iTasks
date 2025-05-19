using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            bool verification = frmLoginController.userLogin(username, password);


            //verifica se o utilizador se consegue entrar
            if (verification)
            {
                frmKanban frmKanban = new frmKanban();
                frmKanban.Show();

                string typeOfUser =frmLoginController.typeOfUser(username, password);

                //verifica se é gestor ou programador e mostra a view correspondente
                if (true) 
                {

                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Tente outra vez", "Dados inválidos");
            }
        }
    }
}
