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
            CarregarTiposTarefa();
        }

        private void btGravar_Click(object sender, EventArgs e)
        {
            string descricao = txtDesc.Text;

            frmGereTiposTarefasController.gravarDados(descricao);
            CarregarTiposTarefa(); // Atualiza a lista após gravar
        }

        private void CarregarTiposTarefa()
        {
            lstLista.Items.Clear();
            using (var context = new ITaskContext())
            {
                var tipos = context.TiposTarefa.ToList();
                foreach (var tipo in tipos)
                {
                    lstLista.Items.Add($"{tipo.Id} - {tipo.Nome}");
                }
            }
        }
    }
}
