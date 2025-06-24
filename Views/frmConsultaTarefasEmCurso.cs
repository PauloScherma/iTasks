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
    public partial class frmConsultaTarefasEmCurso : Form
    {
        public frmConsultaTarefasEmCurso(Gestor gestor)
        {
            InitializeComponent();

            // Pega o id do programador 
            int idGestor = gestor.Id;

            // Carregar as tarefas não concluídas ao carregar o formulário
            gvTarefasEmCurso.DataSource = Controllers.frmConsultarTarefasEmCursoController.mostrarTarefasEmCurso(idGestor);
        }

        public frmConsultaTarefasEmCurso() 
        {
            InitializeComponent();
            
            // Carrega o tempo estimado para cada tarefa
            gvTarefasEmCurso.DataSource = Controllers.frmConsultarTarefasEmCursoController.mostrarTarefasEmCursoAnalise();
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
