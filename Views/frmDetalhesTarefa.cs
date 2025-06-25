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
    public partial class frmDetalhesTarefa : Form
    {
        private Gestor gestorCriador;

        public frmDetalhesTarefa(Gestor gestor)
        {
            InitializeComponent();

            gestorCriador = gestor;
            txtGestor.Text = gestorCriador.Nome;

            cbProgramador.DataSource = frmDetalhesTarefaController.mostrarProgramadores(gestorCriador);

            cbTipoTarefa.DataSource = frmDetalhesTarefaController.mostrarTiposTarefas();
        }

        public frmDetalhesTarefa(Tarefa tarefa, string username)
        {
            InitializeComponent();

            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username);

            //verifica se é gestor ou programador e mostra a view correspondente
            if (typeOfUser == "Gestor")
            {
                gestorCriador = frmKanbanController.gestorLogedIn(username);
                txtGestor.Text = gestorCriador.Nome;

                //parte de baixo
                txtDesc.Text = tarefa.Descricao;
                nUpDownOrdem.Value = tarefa.OrdemExecucao;
                nUpDownStoryPoints.Value = tarefa.StoryPoints;
                dtInicio.Value = tarefa.DataPrevistaInicio;
                dtFim.Value = tarefa.DataPrevistaFim;
                //validar
                cbProgramador.Text = tarefa.IdProgramador.Nome;
                cbTipoTarefa.Text = tarefa.IdTipoTarefa.Nome;
            }
            //programador
            else if (typeOfUser == "Programador")
            {
                txtDesc.Enabled = false;
                nUpDownOrdem.Enabled = false;
                nUpDownStoryPoints.Enabled = false;
                dtInicio.Enabled = false;
                dtFim.Enabled = false;
                cbProgramador.Enabled = false;
                cbTipoTarefa.Enabled = false;
            }
            if (tarefa != null)
            {
                //parte de cima
                txtId.Text = tarefa.Id.ToString();
                txtDataCriacao.Text = tarefa.DataCriacao.ToString();
                txtEstado.Text = tarefa.EstadoAtual.ToString();
                txtDataRealini.Text = "Por definir";
                txtDataRealFim.Text = "Por definir";
                if (tarefa.EstadoAtual.ToString() == "Doing" || tarefa.EstadoAtual.ToString() == "Done")
                    txtDataRealini.Text = tarefa.DataRealInicio.ToString();
                if (tarefa.EstadoAtual.ToString() == "Done")
                    txtDataRealFim.Text = tarefa.DataRealFim.ToString();

                //parte de baixo
                txtDesc.Text = tarefa.Descricao;
                nUpDownOrdem.Value = tarefa.OrdemExecucao;
                nUpDownStoryPoints.Value = tarefa.StoryPoints;
                dtInicio.Value = tarefa.DataPrevistaInicio;
                dtFim.Value = tarefa.DataPrevistaFim;
                //validar
                cbProgramador.Text = tarefa.IdProgramador.Nome;
                cbTipoTarefa.Text = tarefa.IdTipoTarefa.Nome;

                cbProgramador.DataSource = frmDetalhesTarefaController.mostrarProgramadores(gestorCriador);
                cbTipoTarefa.DataSource = frmDetalhesTarefaController.mostrarTiposTarefas();

                cbProgramador.SelectedItem = cbProgramador.Items
                    .OfType<Programador>()
                    .FirstOrDefault(p => p.Id == tarefa.IdProgramador.Id);

                cbTipoTarefa.SelectedItem = cbTipoTarefa.Items
                    .OfType<TipoTarefa>()
                    .FirstOrDefault(t => t.Id == tarefa.IdTipoTarefa.Id);
            }
        }

        private void gravarDadosButton_Click(object sender, EventArgs e)
        {
            // Validação dos campos obrigatórios
            if (txtDesc.Text.Trim() == string.Empty ||
                cbTipoTarefa.SelectedItem == null ||
                cbProgramador.SelectedItem == null)
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Atribuição de valores às variáveis
            string descricao = txtDesc.Text;
            TipoTarefa IdTipoTarefa = cbTipoTarefa.SelectedItem as TipoTarefa;
            Programador IdProgramador = cbProgramador.SelectedItem as Programador;
            int OrdemExecucao = (int)nUpDownOrdem.Value;
            int StoryPoints = (int)nUpDownStoryPoints.Value;
            DateTime DataPrevistaInicio = dtInicio.Value;
            DateTime DataPrevistaFim = dtFim.Value;

            // Validação das datas
            if (DataPrevistaInicio.Date < DateTime.Now.Date)
            {
                MessageBox.Show("A Data Prevista de Início não pode ser menor que a data atual.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DataPrevistaFim.Date < DataPrevistaInicio.Date)
            {
                MessageBox.Show("A Data Prevista de Fim não pode ser menor que a Data Prevista de Início.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validação de ordem duplicada para o mesmo programador
            if (frmDetalhesTarefaController.ExisteOrdemParaProgramador(IdProgramador, OrdemExecucao))
            {
                MessageBox.Show("Já existe uma tarefa com esta ordem para o programador selecionado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int.TryParse(txtId.Text, out int tarefaId);

            // Chama o método gravarDados do controlador para salvar os dados
            frmDetalhesTarefaController.gravarDados(
                tarefaId,
                IdProgramador,
                OrdemExecucao,
                descricao,
                DataPrevistaInicio,
                DataPrevistaFim,
                IdTipoTarefa,
                StoryPoints,
                gestorCriador
            );

            // Exibe aviso de criação ou edição
            if (tarefaId == 0)
                MessageBox.Show("Tarefa criada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Tarefa editada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Atualiza a lstTodo
            frmKanbanController.mostrarTodo();

            this.Close();
        }

        private void btExcluir_Click(object sender, EventArgs e)
        {
            string IdTarefa = txtId.Text;
            if (IdTarefa == null)
            {
                MessageBox.Show("Impossivél excluir uma tarefa que ainda não foi criada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmDetalhesTarefaController.excluirTarefa(IdTarefa);

            frmKanbanController.mostrarTodo();
            frmKanbanController.mostrarDoing();
            frmKanbanController.mostrarDone();
        }
    }
}
