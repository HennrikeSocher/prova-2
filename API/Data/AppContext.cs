
using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext {

    public AppContext(DbContextOptions<AppContext> options) : base(options) {

    }

    public DbSet<Tarefa> Tarefas {get; set;}
}