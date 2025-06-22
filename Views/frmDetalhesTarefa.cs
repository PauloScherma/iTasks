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
        public frmDetalhesTarefa()
        {
            InitializeComponent();

            cbProgramador.DataSource = frmDetalhesTarefaController.mostrarProgramadores();

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

                cbProgramador.DataSource = frmDetalhesTarefaController.mostrarProgramadores();
                cbTipoTarefa.DataSource = frmDetalhesTarefaController.mostrarTiposTarefas();
            }
        }

        private void gravarDadosButton_Click(object sender, EventArgs e)
        {
            // Atribuição de valores as variaveis
            string descricao = txtDesc.Text;
            TipoTarefa IdTipoTarefa = cbTipoTarefa.SelectedItem as TipoTarefa;
            Utilizador IdProgramador = cbProgramador.SelectedItem as Utilizador;
            int OrdemExecucao = (int)nUpDownOrdem.Value;
            int StoryPoints = (int)nUpDownStoryPoints.Value;
            DateTime DataPrevistaInicio = dtInicio.Value;
            DateTime DataPrevistaFim = dtFim.Value;

            // Chama o método gravarDados do controlador para salvar os dados
            frmDetalhesTarefaController.gravarDados(IdProgramador, OrdemExecucao, descricao, DataPrevistaInicio, DataPrevistaFim, IdTipoTarefa, StoryPoints);

            // Atualiza a lstTodo
            frmKanbanController.mostrarTodo();

            this.Close();
        }
    }
}
