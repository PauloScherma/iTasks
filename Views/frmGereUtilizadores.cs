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
            //atributos do forms
            

            //verifica se é gestor ou programador e mostra a view correspondente
            if (typeOfUser == "Gestor")
            {
                //define o userLogin
                userLogin = frmKanbanController.gestorLogedIn(username);

                //gestor dos getores
                if (((Gestor)userLogin).GereUtilizadores)
                {
                    using (var db = new ITaskContext())
                    {
                        //mostrar na comboBox o enum Departamento
                        cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
                        cbDepartamento.SelectedIndex = -1;

                        //mostrar na comboBox o enum NivelExperiencia
                        cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));
                        cbNivelProg.SelectedIndex = -1;

                        //mostrar na comboBox os Gestores
                        var allGestorList = db.Gestores.ToList();
                        cbGestorProg.DataSource = allGestorList;
                        cbGestorProg.DisplayMember = "Nome";      
                        cbGestorProg.ValueMember = "Id";
                        cbGestorProg.SelectedIndex = -1;
                    }
                }
                //gestor dos programadores
                else
                {
                    using (var db = new ITaskContext())
                    {
                        //mostrar na comboBox o enum NivelExperiencia
                        cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));
                        cbNivelProg.SelectedIndex = -1;

                        //mostrar na comboBox os Gestores
                        var allGestorList = db.Gestores.ToList();
                        cbGestorProg.DataSource = allGestorList;
                        cbGestorProg.DisplayMember = "Nome";
                        cbGestorProg.ValueMember = "Id";
                        cbGestorProg.SelectedIndex = -1;
                    }

                    //organiza o form
                    groupBox3.Location = new Point(16, 15);
                    this.Size = new Size(560, 565);
                    groupBox2.Visible = false;
                }
            }
        }

        private void frmGereUtilizadores_Load(object sender, EventArgs e)
        {

        }

        //gravar gestor
        private void btGravarGestor_Click(object sender, EventArgs e)
        {
            string nomeGestor = txtNomeGestor.Text.Trim();
            string usernameGestor= txtUsernameGestor.Text.Trim();
            string passGestor = txtPasswordGestor.Text.Trim();
            string departamentoGestor = cbDepartamento.SelectedValue.ToString().Trim();
            bool gereUtilizadores = chkGereUtilizadores.Checked;

            using (var db = new ITaskContext())
            {
                //verificar se o gestor já existe
                var gestorExistente = db.Gestores.FirstOrDefault(g => g.Username == usernameGestor);
                if (gestorExistente != null)
                {
                    MessageBox.Show("Já existe um gestor com este username.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //criar novo gestor
                Gestor novoGestor = new Gestor
                {
                    Nome = nomeGestor,
                    Username = usernameGestor,
                    Password = passGestor,
                    Departamento = (Departamento)Enum.Parse(typeof(Departamento), departamentoGestor),
                    GereUtilizadores = gereUtilizadores
                };
                //adicionar e gravar no contexto
                db.Gestores.Add(novoGestor);
                db.SaveChanges();
                MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //gravar programador
        private void btGravarProg_Click(object sender, EventArgs e)
        {
            string nomeProg = txtNomeProg.Text.Trim();
            string usernameProg = txtUsernameProg.Text.Trim();
            string passProg = txtPasswordProg.Text.Trim();
            string nivelProg = cbNivelProg.SelectedValue.ToString().Trim();
            string gestorProg = cbGestorProg.ValueMember;

            using(var db = new ITaskContext())
            {
                //verificar se o programadros já existe
                var progExistente = db.Gestores.FirstOrDefault(p => p.Username == usernameProg);
                if (progExistente != null)
                {
                    MessageBox.Show("Já existe um programador com este username.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Programador novoProgramador = new Programador
                {
                    Nome = nomeProg,
                    Username = usernameProg,
                    Password = passProg,
                    NivelExperiencia = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), nivelProg),
                    //dúvida!!! criar o gestor do programador? como fazer. Passo um int? ou um Gestor. so preciso do id.
                    Gestor = null
                };
                //adicionar e gravar no contexto
                db.Programadores.Add(novoProgramador);
                db.SaveChanges();
                MessageBox.Show("Programador gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
