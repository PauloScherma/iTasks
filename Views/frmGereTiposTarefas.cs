using iTasks.Controllers;
using iTasks.Models; // Adicione este using
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
    public partial class frmGereTiposTarefas : Form
    {
        public frmGereTiposTarefas()
        {
            InitializeComponent();
            lstLista.DataSource = frmGereTiposTarefasController.mostrarTiposTarefas();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            string descricao = txtDesc.Text;

            if (descricao == string.Empty)
            {
                MessageBox.Show("Por favor, dê uma descrição", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var db = new ITaskContext())
            {
                // Verifica se já existe um Tipo de Tarefa com essa descrição
                var tipoTarefaExistente = db.TiposTarefa.FirstOrDefault(t => t.Nome == descricao);
                if (tipoTarefaExistente != null)
                {
                    MessageBox.Show("Já existe um Tipo de Tarefa com essa descrição.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            frmGereTiposTarefasController.gravarDados(descricao);
            lstLista.DataSource = null;
            lstLista.DataSource = frmGereTiposTarefasController.mostrarTiposTarefas();
        }

        private void btExcluirTipoTarefa_Click(object sender, EventArgs e)
        {
            int indexSelecionado = lstLista.SelectedIndex;
            TipoTarefa TipoTarefaSelecionado = (TipoTarefa)lstLista.SelectedItem;
            int IdTipotarefa = TipoTarefaSelecionado.Id;
            frmGereTiposTarefasController.excluirTipoTarefa(IdTipotarefa);

            //atualiza as listBoxs
            lstLista.DataSource = null;
            lstLista.DataSource = frmGereTiposTarefasController.mostrarTiposTarefas();
        }

        private void lstLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLista.SelectedIndex != -1)
            {
                TipoTarefa tipoSelecionado = (TipoTarefa)lstLista.SelectedItem;
                txtDesc.Text = tipoSelecionado.Nome;
                txtId.Text = tipoSelecionado.Id.ToString();
            }
        }
    }
}
