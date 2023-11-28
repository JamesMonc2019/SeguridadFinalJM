using Microsoft.EntityFrameworkCore;
using SeguridadFinalJM.Entidades;

namespace SeguridadFinalJM
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)  { }
       
       
        public DbSet<Tarjeta>? Tarjeta { get; set; } 
    }
}
