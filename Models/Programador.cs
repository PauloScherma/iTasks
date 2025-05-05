using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class Programador
    {
        public int NivelExperiencia {  get; set; }
        //chave estrangeira
        public Gestor IdGestor { get; set; }
    }
}
