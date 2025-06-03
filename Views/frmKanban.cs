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
        string username = null;

        public frmKanban(string username)
        {
            InitializeComponent();
            CarregarTarefa();
            CarregarTarefasDoing();
            CarregarTarefasDone();

            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username);

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
                novaTarefaButton.Hide();
                utilizadoresToolStripMenuItem.Visible = false;
            }

            // Assign the username from the logged-in user
            this.username = userLogin.Username;
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
            frmGereUtilizadores frmGereUtilizadores = new frmGereUtilizadores(username);
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

        private void novaTarefaButton_Click(object sender, EventArgs e)
        {
            var frm = new frmDetalhesTarefa();
            frm.TarefaCriada += desc =>
            {
                CarregarTarefa(); // Atualiza a lista após criar uma tarefa
            };
            frm.ShowDialog();
        }

        private void CarregarTarefa()
        {
            lstTodo.Items.Clear();
            using (var context = new ITaskContext())
            {
                var tarefas = context.Tarefas
                    .Where(t => t.EstadoAtual == EstadoAtual.ToDo)
                    .ToList();
                foreach (var tarefa in tarefas)
                {
                    lstTodo.Items.Add($"{tarefa.Descricao}");
                }
            }
        }

        private void CarregarTarefasDoing()
        {
            lstDoing.Items.Clear();
            using (var context = new ITaskContext())
            {
                var tarefasDoing = context.Tarefas
                    .Where(t => t.EstadoAtual == EstadoAtual.Doing)
                    .ToList();
                foreach (var tarefa in tarefasDoing)
                {
                    lstDoing.Items.Add($"{tarefa.Descricao}");
                }
            }
        }

        private void CarregarTarefasDone()
        {
            lstDone.Items.Clear();
            using (var context = new ITaskContext())
            {
                var tarefasDone = context.Tarefas
                    .Where(t => t.EstadoAtual == EstadoAtual.Done)
                    .ToList();
                foreach (var tarefa in tarefasDone)
                {
                    lstDone.Items.Add($"{tarefa.Descricao}");
                }
            }
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            if (lstTodo.SelectedItem != null)
            {
                string descricaoSelecionada = lstTodo.SelectedItem.ToString();

                using (var context = new ITaskContext())
                {
                    var tarefa = context.Tarefas.FirstOrDefault(t => t.Descricao == descricaoSelecionada);
                    if (tarefa != null)
                    {
                        tarefa.EstadoAtual = EstadoAtual.Doing;
                        context.SaveChanges();
                    }
                }

                lstDoing.Items.Add(lstTodo.SelectedItem);
                lstTodo.Items.Remove(lstTodo.SelectedItem);
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para mover!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            if (lstDoing.SelectedItem != null)
            {
                string descricaoSelecionada = lstDoing.SelectedItem.ToString();
                using (var context = new ITaskContext())
                {
                    var tarefa = context.Tarefas.FirstOrDefault(t => t.Descricao == descricaoSelecionada);
                    if (tarefa != null)
                    {
                        tarefa.EstadoAtual = EstadoAtual.ToDo;
                        context.SaveChanges();
                    }
                }
                lstTodo.Items.Add(lstDoing.SelectedItem);
                lstDoing.Items.Remove(lstDoing.SelectedItem);
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para reiniciar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btSetDone_Click(object sender, EventArgs e)
        {
            if (lstDoing.SelectedItem != null)
            {
                string descricaoSelecionada = lstDoing.SelectedItem.ToString();
                using (var context = new ITaskContext())
                    {
                    var tarefa = context.Tarefas.FirstOrDefault(t => t.Descricao == descricaoSelecionada);
                    if (tarefa != null)
                    {
                        tarefa.EstadoAtual = EstadoAtual.Done;
                        context.SaveChanges();
                    }
                }
                lstDone.Items.Add(lstDoing.SelectedItem);
                lstDoing.Items.Remove(lstDoing.SelectedItem);
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para finalizar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            frmDetalhesTarefa frmDetalhesTarefa = new frmDetalhesTarefa();
            if (lstTodo.SelectedItem != null)
            {
                string descricao = lstTodo.SelectedItem.ToString();
                frmDetalhesTarefa.TarefaCriada += desc =>
                {
                    lstTodo.Items[lstTodo.SelectedIndex] = desc;
                };
                frmDetalhesTarefa.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para editar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
