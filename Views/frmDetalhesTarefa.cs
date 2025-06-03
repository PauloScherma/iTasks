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
        public event Action<string> TarefaCriada;

        //public event Action<string> TarefaCriada;
        public frmDetalhesTarefa()
        {
            InitializeComponent();
        }

        private void gravarDadosButton_Click(object sender, EventArgs e)
        {
            // Atribuição de valores as variaveis
            string descricao = txtDesc.Text;
            TipoTarefa IdTipoTarefa = null;
            Utilizador IdProgramador = null;
            int OrdemExecucao = (int)nUpDownOrdem.Value;
            int StoryPoints = (int)nUpDownStoryPoints.Value;
            DateTime DataPrevistaInicio = dtInicio.Value;
            DateTime DataPrevistaFim = dtFim.Value;

            // Chama o método gravarDados do controlador para salvar os dados
            frmDetalhesTarefaController.gravarDados(IdProgramador, OrdemExecucao, descricao, DataPrevistaInicio, DataPrevistaFim, IdTipoTarefa, StoryPoints);
            frmKanbanController.mostrarTodo();
            this.Close();
            
        }
    }
}
