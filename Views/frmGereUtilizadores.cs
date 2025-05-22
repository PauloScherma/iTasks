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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace iTasks
{
    public partial class frmGereUtilizadores : Form
    {
        Utilizador userLogin = null;

        public frmGereUtilizadores(string username)
        {
            InitializeComponent();
            //atribuimos o tipo do utilizador logado à variavel
            string typeOfUser = frmKanbanController.typeOfUser(username);

            //verifica se é gestor ou programador e mostra a view correspondente
            if (typeOfUser == "Gestor")
            {
                //define o userLogin
                userLogin = frmKanbanController.gestorLogedIn(username);

                //gestor dos getores
                if (((Gestor)userLogin).GereUtilizadores)
                {
                    //ver se é preciso
                }
                //gestor dos programadores
                else
                {
                    //escoder coisas
                    groupBox2.Visible = false;
                }
            }
            //programador
            else if (typeOfUser == "Programador")
            {

            }
        }
    }
}
