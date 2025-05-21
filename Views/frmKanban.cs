using iTasks.Controllers;
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
    public partial class frmKanban : Form
    {
        Utilizador userLogin = null;

        public frmKanban(string username, string password)
        {
            InitializeComponent();

            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username, password);

            //verifica se é gestor ou programador e mostra a view correspondente
            if (typeOfUser == "Gestor")
            {
                //define o userLogin
                userLogin = frmKanbanController.gestorLogedIn(username);
                //atribui o nome do utilizador à label
                labelNomeUtilizador.Text = "Bem-vindo: " + userLogin.Nome;
                //gestor dos getores
                if (((Gestor)userLogin).GereUtilizadores)
                {
                    //ver se é preciso
                }
                //gestor dos programadores
                else
                {
                    //escoder coisas
                    gerirUtilizadoresToolStripMenuItem.Visible = false;
                }
            }
            //programador
            else if (typeOfUser == "Programador")
            {
                //define o userLogin
                userLogin = frmKanbanController.programadorLogedIn(username);
                //atribui o nome do utilizador à label
                labelNomeUtilizador.Text = "Bem-vindo: " + userLogin.Nome;
                //escoder coisas
                btNova.Hide();
                utilizadoresToolStripMenuItem.Visible = false;
            }
        }

        //botões que abrem outros forms
        #region
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultarTarefasConcluidas frmConsultarTarefasConcluidas = new frmConsultarTarefasConcluidas();
            frmConsultarTarefasConcluidas.Show();
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaTarefasEmCurso frmConsultaTarefasEmCurso = new frmConsultaTarefasEmCurso();
            frmConsultaTarefasEmCurso.Show();
        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereUtilizadores frmGereUtilizadores = new frmGereUtilizadores();
            frmGereUtilizadores.Show();
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereTiposTarefas frmGereTiposTarefas = new frmGereTiposTarefas();
            frmGereTiposTarefas.Show();
        }
        #endregion

        //função para verificar a saida
        private void frmKanban_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem a certeza que queres sair?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                e.Cancel = true;
        }
    }
}
