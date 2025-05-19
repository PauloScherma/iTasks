using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{

    //definir o enum()
    public enum NivelExperiencia
    {
        Junior,
        Senior
    }

    //definir colunas na tabela
    public class Programador : Utilizador
    {
        public NivelExperiencia NivelExperiencia {  get; set; } 
        //chave estrangeira
        public Gestor Gestor { get; set; }
    }
}
