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
        private Tarefa tarefaAtual;

        public frmDetalhesTarefa()
        {
            InitializeComponent();

            cbProgramador.DataSource = frmDetalhesTarefaController.mostrarProgramadores();
            cbProgramador.SelectedIndex = -1;

            cbTipoTarefa.DataSource = frmDetalhesTarefaController.mostrarTiposTarefas();
            cbTipoTarefa.SelectedIndex = -1;
        }

        // Novo construtor para edição
        public frmDetalhesTarefa(Tarefa tarefa) : this()
        {
            tarefaAtual = tarefa;
            if (tarefa != null)
            {
                txtDesc.Text = tarefa.Descricao;
                nUpDownOrdem.Value = tarefa.OrdemExecucao;
                nUpDownStoryPoints.Value = tarefa.StoryPoints;
                dtInicio.Value = tarefa.DataPrevistaInicio;
                dtFim.Value = tarefa.DataPrevistaFim;

                // Seleciona o programador e tipo de tarefa corretos
                cbProgramador.SelectedItem = cbProgramador.Items
                    .OfType<Utilizador>()
                    .FirstOrDefault(u => u.Id == tarefa.IdProgramador?.Id);

                cbTipoTarefa.SelectedItem = cbTipoTarefa.Items
                    .OfType<TipoTarefa>()
                    .FirstOrDefault(t => t.Id == tarefa.IdTipoTarefa?.Id);
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
