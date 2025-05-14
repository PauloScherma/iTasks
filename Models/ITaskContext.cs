using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Models
{
    public class ITaskContext : DbContext
    {
        public ITaskContext() : base("iTaskDB")
        {

        }

        public DbSet<Gestor> Gestores { get; set; }
        public DbSet<Programador> Programadores { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<TipoTarefa> TiposTarefa { get; set; }
        public DbSet<Utilizador> Utilizadores { get; set; }
    }
}
