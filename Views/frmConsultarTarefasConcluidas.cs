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
    public partial class frmConsultarTarefasConcluidas : Form
    {
        //caso seja programador
        public frmConsultarTarefasConcluidas(Programador programador)
        {
            InitializeComponent();

            // Pega o id do programador 
            int idProgramador = programador.Id;

            // Carregar as tarefas concluídas ao carregar o formulário
            gvTarefasConcluidas.DataSource = Controllers.frmConsultarTarefasConcluidasController.mostrarTarefasConcluidas(idProgramador);
            var tempoEmDias = gvTarefasConcluidas.Columns["TempoEmDias"];
            tempoEmDias.DefaultCellStyle.Format = "F8";
            var tempoPrevisto = gvTarefasConcluidas.Columns["TempoPrevisto"];
            tempoPrevisto.Visible = false;
            var nome = gvTarefasConcluidas.Columns["Nome"];
            nome.Visible = false;
        }
        //caso seja gestor
        public frmConsultarTarefasConcluidas(Gestor gestor)
        {
            InitializeComponent();

            // Pega o id do gestor 
            int idGestor = gestor.Id;
            
            // Carregar as tarefas concluídas por programador ao carregar o formulário
            gvTarefasConcluidas.DataSource = Controllers.frmConsultarTarefasConcluidasController.mostrarTarefasConcluidasPorProgramador(idGestor);
            var tempoEmDias = gvTarefasConcluidas.Columns["TempoEmDias"];
            tempoEmDias.DefaultCellStyle.Format = "F8";
            var tempoPrevisto = gvTarefasConcluidas.Columns["TempoPrevisto"];
            tempoPrevisto.DefaultCellStyle.Format = "F8";
        }
        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
