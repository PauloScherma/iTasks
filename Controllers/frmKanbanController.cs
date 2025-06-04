using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTasks;

namespace iTasks.Controllers
{
    class frmKanbanController
    {
        //verifica qual o tipo do utilizador (Gestor)
        public static Gestor gestorLogedIn(string username)
        {
            //pega no utilizador que está logado
            using (var db = new ITaskContext())
            {
                //ver melhor tipo
                var gestorLogedIn = db.Gestores
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                return gestorLogedIn;
            }
        }
        //verifica qual o tipo do utilizador (Programador)
        public static Programador programadorLogedIn(string username)
        {
            //pega no utilizador que está logado
            using (var db = new ITaskContext())
            {
                //ver melhor tipo
                var programadorLogedIn = db.Programadores
                    .Where(u => u.Username == username)
                    .FirstOrDefault();

                return programadorLogedIn;
            }
        }
        //verifica qual o tipo do utilizador (Gestor ou Programador)
        public static string typeOfUser(string username)
        {
            using (var db = new ITaskContext())
            {
                var typeOfUser = (from user in db.Utilizadores
                                  where user.Username == username
                                  select user).FirstOrDefault();

                var typeName = typeOfUser.GetType().Name;

                return typeName;

            }
        }
        public static List<Tarefa> mostrarTodo()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na lstBox as Tarefas
                var allTarefaList = db.Tarefas.ToList()
                .Where(t => t.EstadoAtual == EstadoAtual.ToDo)
                .ToList();
                return allTarefaList;
            }
        }

        public static List<Tarefa> mostrarDoing()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na lstBox as Tarefas
                var allTarefaList = db.Tarefas.ToList()
                .Where(t => t.EstadoAtual == EstadoAtual.Doing)
                .ToList();
                return allTarefaList;
            }
        }

        public static List<Tarefa> mostrarDone()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na lstBox as Tarefas
                var allTarefaList = db.Tarefas.ToList()
                .Where(t => t.EstadoAtual == EstadoAtual.Done)
                .ToList();
                return allTarefaList;
            }
        }
    }
}

