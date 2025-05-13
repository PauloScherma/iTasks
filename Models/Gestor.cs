using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    //definir o enum()
    public enum Departamento
    {
        IT, 
        Marketing,
        Administração
    }

    //definir colunas na tabela
    public class Gestor : Utilizador
    {
        public Departamento Departamento { get; set; }
        public int GereUtilizadores { get; set; } //ver melhor qual o "tipo"
    }
}
