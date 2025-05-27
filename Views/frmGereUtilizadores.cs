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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace iTasks
{
    public partial class frmGereUtilizadores : Form
    {
        Utilizador userLogin = null;

        public frmGereUtilizadores(string username)
        {
            InitializeComponent();
            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username);

            //verifica se é gestor ou programador e mostra a view correspondente
            if (typeOfUser == "Gestor")
            {
                //define o userLogin
                userLogin = frmKanbanController.gestorLogedIn(username);

                //gestor dos getores
                if (((Gestor)userLogin).GereUtilizadores)
                {
                    //mostrar na comboBox o enum Departamento
                    cbDepartamento.DataSource = frmGereUtilizadoresController.mostrarDepartamentos();
                    cbDepartamento.SelectedIndex = -1;

                    //mostrar na comboBox o enum NivelExperiencia
                    cbNivelProg.DataSource = frmGereUtilizadoresController.mostrarNivelExperiencia();
                    cbNivelProg.SelectedIndex = -1;

                    //mostrar na comboBox os Gestores
                    cbGestorProg.DataSource = frmGereUtilizadoresController.mostrarGestores();
                    cbGestorProg.DisplayMember = "Nome";
                    cbGestorProg.ValueMember = "Id";
                    cbGestorProg.SelectedIndex = -1;

                    //mostra a lista de gestores
                    lstListaGestores.DataSource = frmGereUtilizadoresController.mostrarGestores();
                    lstListaGestores.DisplayMember = "Nome";
                    lstListaGestores.ValueMember = "Id";
                    lstListaGestores.SelectedIndex = -1;

                    //mostra a lista de programadores
                    lstListaProgramadores.DataSource = frmGereUtilizadoresController.mostrarProgramadores();
                    lstListaProgramadores.DisplayMember = "Nome";
                    lstListaProgramadores.ValueMember = "Id";
                    lstListaProgramadores.SelectedIndex = -1;
                }
                //gestor dos programadores
                else
                {
                    //mostrar na comboBox o enum NivelExperiencia
                    cbNivelProg.DataSource = frmGereUtilizadoresController.mostrarNivelExperiencia();
                    cbNivelProg.SelectedIndex = -1;

                    //mostrar na comboBox os Gestores
                    cbGestorProg.DataSource = frmGereUtilizadoresController.mostrarGestores();
                    cbGestorProg.DisplayMember = "Nome";
                    cbGestorProg.ValueMember = "Id";
                    cbGestorProg.SelectedIndex = -1;

                    //mostra a lista de programadores
                    lstListaProgramadores.DataSource = frmGereUtilizadoresController.mostrarProgramadores();
                    lstListaProgramadores.DisplayMember = "Nome";
                    lstListaProgramadores.ValueMember = "Id";
                    lstListaProgramadores.SelectedIndex = -1;

                    //organiza o form
                    groupBox3.Location = new Point(16, 15);
                    this.Size = new Size(560, 565);
                    groupBox2.Visible = false;
                }
            }
        }

        //gravar gestor
        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            //atribui valores
            string nomeGestor = txtNomeGestor.Text.Trim();
            string usernameGestor= txtUsernameGestor.Text.Trim();
            string passGestor = txtPasswordGestor.Text.Trim();
            string departamentoGestor = cbDepartamento.SelectedValue.ToString().Trim();
            bool gereUtilizadores = chkGereUtilizadores.Checked;

            //cria o gestor
            frmGereUtilizadoresController.criarGestor(usernameGestor, nomeGestor, passGestor, departamentoGestor, gereUtilizadores);
        }
        //gravar programador
        private void btGravarProg_Click(object sender, EventArgs e)
        {
            if (true) { 
            //atribui valores
            string nomeProg = txtNomeProg.Text.Trim();
            string usernameProg = txtUsernameProg.Text.Trim();
            string passProg = txtPasswordProg.Text.Trim();
            string nivelProg = cbNivelProg.SelectedValue.ToString().Trim();
            string gestorProg = cbGestorProg.ValueMember;
            
            //cria o programador
            frmGereUtilizadoresController.criarProg(nomeProg, usernameProg, passProg, nivelProg, gestorProg);
            }
        }

        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            //porque fica com 0? não devia ser -1 -> problema no SelectedIndexChanged?
            int indexSelecionado = lstListaGestores.SelectedIndex;

            if (indexSelecionado != -1) 
            {
                //pega o gestor selecionado na lista
                Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;

                //atribui os valores do gestor selecionado aos campos
                txtIdGestor.Text = gestorSelecionado.Id.ToString();
                txtNomeGestor.Text = gestorSelecionado.Nome;
                txtUsernameGestor.Text = gestorSelecionado.Username;
                txtPasswordGestor.Text = gestorSelecionado.Password;
                cbDepartamento.SelectedItem = gestorSelecionado.Departamento;

                bool gereGestor = gestorSelecionado.GereUtilizadores;
                if(gereGestor)
                {
                    chkGereUtilizadores.Checked = true;
                }
                else
                {
                    chkGereUtilizadores.Checked = false;
                }
            }
        }

        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            //porque fica com 0? não devia ser -1 -> problema no SelectedIndexChanged?
            int indexSelecionado = lstListaProgramadores.SelectedIndex;

            if (indexSelecionado != -1)
            {
                //pega o gestor selecionado na lista
                Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;

                //atribui os valores do gestor selecionado aos campos
                txtIdProg.Text = progSelecionado.Id.ToString();
                txtNomeProg.Text = progSelecionado.Nome;
                txtUsernameProg.Text = progSelecionado.Username;
                txtPasswordProg.Text = progSelecionado.Password;
                cbNivelProg.SelectedItem = progSelecionado.NivelExperiencia;
                //cbGestorProg.SelectedValue = progSelecionado.GestorId;
            }
        }

        //excluir gestor
        private void button1_Click(object sender, EventArgs e)
        {
            int indexSelecionado = lstListaGestores.SelectedIndex;
            Gestor gestorSelecionado = (Gestor)lstListaGestores.SelectedItem;
            int IdGestor = gestorSelecionado.Id;
            frmGereUtilizadoresController.excluirGestor(IdGestor);
        }
        //excluir programador
        private void button2_Click(object sender, EventArgs e)
        {
            int indexSelecionado = lstListaProgramadores.SelectedIndex;
            Programador progSelecionado = (Programador)lstListaProgramadores.SelectedItem;
            int IdProg = progSelecionado.Id;
            frmGereUtilizadoresController.excluirProgramador(IdProg);
        }
    }
}
