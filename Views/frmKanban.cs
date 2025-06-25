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
            lstTodo.DataSource = frmKanbanController.mostrarTodo();
            lstDoing.DataSource = frmKanbanController.mostrarDoing();
            lstDone.DataSource = frmKanbanController.mostrarDone();

            label1.Text = "Nome de utilizador: " + username;

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
                tarefasEmCursoToolStripMenuItem.Visible = false;
                btPrevisao.Hide();
            }

            // atribui o username ao "username"
            this.username = userLogin.Username;
        }

        //botões que abrem outros forms
        #region

        private void btPrevisao_Click(object sender, EventArgs e)
        {
            frmConsultaTarefasEmCurso frmConsultaTarefasEmCurso = new frmConsultaTarefasEmCurso();
            frmConsultaTarefasEmCurso.ShowDialog();
        }

        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username);

            //verifica se é gestor ou programador e mostra a view correspondente
            if (typeOfUser == "Programador")
            {
                Programador userLogin = frmKanbanController.programadorLogedIn(username);
                frmConsultarTarefasConcluidas frmConsultarTarefasConcluidas = new frmConsultarTarefasConcluidas(userLogin);
                frmConsultarTarefasConcluidas.ShowDialog();
            }
            else
            {
                Gestor userLogin = frmKanbanController.gestorLogedIn(username);
                frmConsultarTarefasConcluidas frmConsultarTarefasConcluidas = new frmConsultarTarefasConcluidas(userLogin);
                frmConsultarTarefasConcluidas.ShowDialog();
            }
        }

        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username);

            Gestor userLogin = frmKanbanController.gestorLogedIn(username);
            frmConsultaTarefasEmCurso frmConsultaTarefasEmCurso = new frmConsultaTarefasEmCurso(userLogin);
            frmConsultaTarefasEmCurso.ShowDialog();
        }

        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereUtilizadores frmGereUtilizadores = new frmGereUtilizadores(username);
            frmGereUtilizadores.ShowDialog();
        }

        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGereTiposTarefas frmGereTiposTarefas = new frmGereTiposTarefas();
            frmGereTiposTarefas.ShowDialog();
        }

        private void novaTarefaButton_Click(object sender, EventArgs e)
        {
            frmDetalhesTarefa frmDetalhesTarefa = new frmDetalhesTarefa((Gestor)userLogin);
            frmDetalhesTarefa.ShowDialog();

            // Atualiza as listas após fechar o formulário de detalhes
            lstTodo.DataSource = frmKanbanController.mostrarTodo();
            lstDoing.DataSource = frmKanbanController.mostrarDoing();
            lstDone.DataSource = frmKanbanController.mostrarDone();
        }
        #endregion

        //função para verificar a saida
        private void frmKanban_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem a certeza que queres sair?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
                e.Cancel = true;
        }

        private void btSetDoing_Click(object sender, EventArgs e)
        {
            if (lstTodo.SelectedItem != null)
            {
                string descricaoSelecionada = lstTodo.SelectedItem.ToString();
                using (var context = new ITaskContext())
                {
                    var tarefa = context.Tarefas.Include("IdProgramador").FirstOrDefault(t => t.Descricao == descricaoSelecionada);
                    if (tarefa != null)
                    {
                        string typeOfUser = frmKanbanController.typeOfUser(username);
                        if (typeOfUser == "Programador")
                        {
                            var programador = frmKanbanController.programadorLogedIn(username);
                            if (tarefa.IdProgramador == null || tarefa.IdProgramador.Id != programador.Id)
                            {
                                MessageBox.Show("Você só pode movimentar tarefas atribuídas a você.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Verifica se o programador já tem 2 tarefas em Doing
                            int doingCount = context.Tarefas
                                .Where(t => t.IdProgramador.Id == programador.Id && t.EstadoAtual == EstadoAtual.Doing)
                                .Count();

                            if (doingCount >= 2)
                            {
                                MessageBox.Show("Você já possui 2 tarefas em andamento (Doing). Conclua ou mova alguma para poder iniciar outra.", "Limite atingido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            //Verifica se a tarefa anterior está concluída
                            var tarefaAnterior = context.Tarefas
                                .Where(t => t.IdProgramador.Id == programador.Id && t.OrdemExecucao < tarefa.OrdemExecucao)
                                .OrderByDescending(t => t.OrdemExecucao)
                                .FirstOrDefault(t => t.EstadoAtual == EstadoAtual.ToDo);

                            if (tarefaAnterior != null)
                            {
                                MessageBox.Show("As tarefas devem ser começadas pela ordem pretendida.", "Ordem de Execução", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Verifica se a tarefa anterior está concluída
                            tarefaAnterior = context.Tarefas
                                .Where(t => t.IdProgramador.Id == programador.Id && t.OrdemExecucao < tarefa.OrdemExecucao)
                                .OrderByDescending(t => t.OrdemExecucao)
                                .FirstOrDefault(t => t.EstadoAtual != EstadoAtual.Done);
                        }
                        tarefa.EstadoAtual = EstadoAtual.Doing;
                        tarefa.DataRealInicio = DateTime.Now;
                        context.SaveChanges();
                    }
                }
                lstDoing.DataSource = frmKanbanController.mostrarDoing();
                lstTodo.DataSource = frmKanbanController.mostrarTodo();
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para iniciar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btSetTodo_Click(object sender, EventArgs e)
        {
            if (lstDoing.SelectedItem != null)
            {
                string descricaoSelecionada = lstDoing.SelectedItem.ToString();
                using (var context = new ITaskContext())
                {
                    var tarefa = context.Tarefas.Include("IdProgramador").FirstOrDefault(t => t.Descricao == descricaoSelecionada);
                    if (tarefa != null)
                    {
                        string typeOfUser = frmKanbanController.typeOfUser(username);
                        if (typeOfUser == "Programador")
                        {
                            var programador = frmKanbanController.programadorLogedIn(username);
                            if (tarefa.IdProgramador == null || tarefa.IdProgramador.Id != programador.Id)
                            {
                                MessageBox.Show("Você só pode movimentar tarefas atribuídas a você.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        tarefa.EstadoAtual = EstadoAtual.ToDo;
                        context.SaveChanges();
                    }
                }
                lstTodo.DataSource = frmKanbanController.mostrarTodo();
                lstDoing.DataSource = frmKanbanController.mostrarDoing();
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
                    var tarefa = context.Tarefas.Include("IdProgramador").FirstOrDefault(t => t.Descricao == descricaoSelecionada);
                    if (tarefa != null)
                    {
                        string typeOfUser = frmKanbanController.typeOfUser(username);
                        if (typeOfUser == "Programador")
                        {
                            var programador = frmKanbanController.programadorLogedIn(username);
                            if (tarefa.IdProgramador == null || tarefa.IdProgramador.Id != programador.Id)
                            {
                                MessageBox.Show("Você só pode movimentar tarefas atribuídas a você.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            //Verificar se a tarefa anterior está concluída
                            var tarefaAnterior = context.Tarefas
                                .Where(t => t.IdProgramador.Id == programador.Id && t.OrdemExecucao < tarefa.OrdemExecucao)
                                .OrderByDescending(t => t.OrdemExecucao)
                                .FirstOrDefault(t => t.EstadoAtual != EstadoAtual.Done);

                            if (tarefaAnterior != null)
                            {
                                MessageBox.Show("As tarefas devem ser finalizadas pela ordem pretendida.", "Ordem de Execução", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        tarefa.EstadoAtual = EstadoAtual.Done;
                        tarefa.DataRealFim = DateTime.Now;
                        context.SaveChanges();
                    }
                }
                lstDone.DataSource = frmKanbanController.mostrarDone();
                lstDoing.DataSource = frmKanbanController.mostrarDoing();
            }
            else
            {
                MessageBox.Show("Selecione uma tarefa para concluir!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lstTodo_DoubleClick(object sender, EventArgs e)
        {
            if (lstTodo.SelectedItem is Tarefa tarefaSelecionada)
            {
                string username = this.username;
                frmDetalhesTarefa frm = new frmDetalhesTarefa(tarefaSelecionada, username);
                frm.ShowDialog();
                lstTodo.DataSource = frmKanbanController.mostrarTodo();
                lstDoing.DataSource = frmKanbanController.mostrarDoing();
                lstDone.DataSource = frmKanbanController.mostrarDone();
            }
        }

        private void lstDoing_DoubleClick(object sender, EventArgs e)
        {
            if (lstDoing.SelectedItem is Tarefa tarefaSelecionada)
            {
                string username = this.username;
                frmDetalhesTarefa frm = new frmDetalhesTarefa(tarefaSelecionada, username);
                frm.ShowDialog();
                lstTodo.DataSource = frmKanbanController.mostrarTodo();
                lstDoing.DataSource = frmKanbanController.mostrarDoing();
                lstDone.DataSource = frmKanbanController.mostrarDone();
            }
        }

        private void lstDone_DoubleClick(object sender, EventArgs e)
        {
            if (lstDone.SelectedIndex != -1)
            {
                Tarefa tarefaSelecionada = (Tarefa)lstDone.SelectedItem;
                string username = this.username; 
                frmDetalhesTarefa frm = new frmDetalhesTarefa(tarefaSelecionada, username);
                frm.ShowDialog();
                lstTodo.DataSource = frmKanbanController.mostrarTodo();
                lstDoing.DataSource = frmKanbanController.mostrarDoing();
                lstDone.DataSource = frmKanbanController.mostrarDone();
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
        }

        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Verifica se o utilizador logado é um Gestor
            string typeOfUser = frmKanbanController.typeOfUser(username);
            if (typeOfUser != "Gestor")
            {
                MessageBox.Show("Apenas gestores podem exportar tarefas concluídas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Busca as tarefas concluídas 
            var tarefasDone = frmKanbanController.mostrarDone();


            if (tarefasDone.Count == 0)
            {
                MessageBox.Show("Não existem tarefas concluídas para exportar.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Abre diálogo para escolher onde salvar o ficheiro
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "CSV files (*.csv)|*.csv";
                dlg.Title = "Exportar Tarefas Concluídas";
                dlg.FileName = "tarefas_concluidas.csv";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var sb = new StringBuilder();

                    // Cabeçalho
                    sb.AppendLine("Programador;Descricao;DataPrevistaInicio;DataPrevistaFim;TipoTarefa;DataRealInicio;DataRealFim");

                    // Linhas de dados
                    foreach (var tarefa in tarefasDone)
                    {
                        string programador = tarefa.IdProgramador != null ? tarefa.IdProgramador.Nome : "";
                        string descricao = tarefa.Descricao?.Replace(";", ",") ?? "";
                        string dataPrevistaInicio = tarefa.DataPrevistaInicio.ToString("yyyy-MM-dd");
                        string dataPrevistaFim = tarefa.DataPrevistaFim.ToString("yyyy-MM-dd");
                        string tipoTarefa = tarefa.IdTipoTarefa != null ? tarefa.IdTipoTarefa.Nome : "";
                        string dataRealInicio = tarefa.DataRealInicio != DateTime.MinValue ? tarefa.DataRealInicio.ToString("yyyy-MM-dd") : "";
                        string dataRealFim = tarefa.DataRealFim != DateTime.MinValue ? tarefa.DataRealFim.ToString("yyyy-MM-dd") : "";

                        sb.AppendLine($"{programador};{descricao};{dataPrevistaInicio};{dataPrevistaFim};{tipoTarefa};{dataRealInicio};{dataRealFim}");
                    }

                    // Escreve o ficheiro
                    System.IO.File.WriteAllText(dlg.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Exportação concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}

