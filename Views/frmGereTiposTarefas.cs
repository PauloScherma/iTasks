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

            frmGereTiposTarefasController.gravarDados(descricao);
            lstLista.DataSource = frmGereTiposTarefasController.mostrarTiposTarefas();
        }
    }
}
