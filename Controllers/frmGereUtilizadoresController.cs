using iTasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks.Controllers
{
    class frmGereUtilizadoresController
    {
        public static void criarProg(string nomeProg, string usernameProg, string passProg, string nivelProg, string gestorProg)
        {
            using (var db = new ITaskContext())
            {
                //verificar se o programadros já existe e se Verdade muda os dados
                var progExistente = db.Gestores.FirstOrDefault(p => p.Username == usernameProg);
                if (progExistente != null)
                {

                    MessageBox.Show("Já existe um programador com este username.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        //dúvida!!! criar o gestor do programador? como fazer. Passo um int? ou um Gestor. so preciso do id.
                        Gestor = null
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

                    MessageBox.Show("Já existe um gestor com este username.", "Dados foram alterados!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //criar novo gestor
                else
                { 
                    Gestor novoGestor = new Gestor
                    {
                        Nome = nomeGestor,
                        Username = usernameGestor,
                        Password = passGestor,
                        Departamento = (Departamento)Enum.Parse(typeof(Departamento), departamentoGestor),
                        GereUtilizadores = gereUtilizadores
                    };
                    //adicionar e gravar no contexto
                    db.Gestores.Add(novoGestor);
                    db.SaveChanges();
                    MessageBox.Show("Gestor gravado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //pega o nivel de experiencia
        public static List<NivelExperiencia> mostrarNivelExperiencia()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na comboBox o enum NivelExperiencia
                var nivelExperienciaList = Enum.GetValues(typeof(NivelExperiencia)).Cast<NivelExperiencia>().ToList();
                //retornar a lista
                return nivelExperienciaList;
            }
        }
        //pega os departamentos
        public static List<Departamento> mostrarDepartamentos()
        {
            using (var db = new ITaskContext())
            {
                //mostrar na comboBox o enum Departamento
                var departamentoList = Enum.GetValues(typeof(Departamento)).Cast<Departamento>().ToList();
                //retornar a lista
                return departamentoList;
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
                var allProgramadorList = db.Programadores.ToList();
                //retornar a lista
                return allProgramadorList;
            }
        }
    }
}
