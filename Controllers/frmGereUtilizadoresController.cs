using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    class frmGereUtilizadoresController
    {
        public static void criarProg(string nomeProg, string usernameProg, string passProg, string nivelProg, Gestor gestorProg)
        {
            using (var db = new ITaskContext())
            {
                //verificar se o programadros já existe e se Verdade muda os dados
                var progExistente = db.Programadores.FirstOrDefault(p => p.Username == usernameProg);
                if (progExistente != null)
                {
                    //atribui os novos dados
                    progExistente.Nome = nomeProg;
                    progExistente.Username = usernameProg;
                    progExistente.Password = passProg;
                    progExistente.NivelExperiencia = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), nivelProg);
                    progExistente.Gestor = gestorProg;
                    //altera os dados
                    db.SaveChanges();
                    MessageBox.Show("Username já existe. Os dados dele foram alterados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //cria programador
                else
                {
                    Programador novoProgramador = new Programador
                    {
                        Nome = nomeProg,
                        Username = usernameProg,
                        Password = passProg,
                        NivelExperiencia = (NivelExperiencia)Enum.Parse(typeof(NivelExperiencia), nivelProg),
                        Gestor = gestorProg
                    };
                    //adicionar e gravar no contexto
                    db.Programadores.Add(novoProgramador);
                    db.SaveChanges();
                    MessageBox.Show("Programador gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public static void criarGestor(string usernameGestor, string nomeGestor, string passGestor, string departamentoGestor, bool gereUtilizadores)
        {
            using (var db = new ITaskContext())
            {
                //verificar se o gestor já existe e se Verdade muda os dados
                var gestorExistente = db.Gestores.FirstOrDefault(g => g.Username == usernameGestor);
                if (gestorExistente != null)
                {
                    //atribui os novos dados
                    gestorExistente.Nome = nomeGestor;
                    gestorExistente.Password = passGestor;
                    gestorExistente.Departamento = (Departamento)Enum.Parse(typeof(Departamento), departamentoGestor);
                    gestorExistente.GereUtilizadores = gereUtilizadores;
                    //altera os dados
                    db.SaveChanges();
                    MessageBox.Show("Já existe os dados dele foram alterados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //criar novo gestor
                else
                {
                    //criar gestor novo
                    Gestor novoGestor = new Gestor
                    {
                        Nome = nomeGestor,
                        Username = usernameGestor,
                        Password = passGestor,
                        Departamento = (Departamento)Enum.Parse(typeof(Departamento), departamentoGestor),
                        GereUtilizadores = gereUtilizadores
                    };
                    //adicionar e gravar
                    db.Gestores.Add(novoGestor);
                    db.SaveChanges();
                    MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //pega todos os gestores
        public static List<Gestor> mostrarGestores()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na comboBox os Gestores
                var allGestorList = db.Gestores.ToList();
                //retornar a lista
                return allGestorList;
            }
        }
        //pega todos os programadores
        public static List<Programador> mostrarProgramadores()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na comboBox os Programadores
                var allProgramadorList = db.Programadores.Include("Gestor").ToList();
                //retornar a lista
                return allProgramadorList;
            }
        }
        public static void excluirGestor(int gestorId)
        {
            using (var db = new ITaskContext())
            {
                //encontrar o gestor pelo ID
                var gestor = db.Gestores.Find(gestorId);
                if (gestor != null)
                {
                    //remover o gestor do contexto
                    db.Gestores.Remove(gestor);
                    db.SaveChanges();
                    MessageBox.Show("Gestor excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Gestor não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static void excluirProgramador(int progId)
        {
            using (var db = new ITaskContext())
            {
                //encontrar o programador pelo ID
                var programador = db.Programadores.Find(progId);
                if (programador != null)
                {
                    //remover o programador do contexto
                    db.Programadores.Remove(programador);
                    db.SaveChanges();
                    MessageBox.Show("Programador excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Programador não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
