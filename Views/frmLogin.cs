using iTasks.Models;
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

            //chama funcao para ver se o utilizador existe
            bool verification = frmLoginController.userValidation(username, password);


            //verifica se o utilizador consegue entrar
            if (verification)
            {
                frmKanban frmKanban = new frmKanban(username);
                this.Hide();
                frmKanban.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Dados inválidos tente outra vez", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
