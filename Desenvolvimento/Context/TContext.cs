namespace Desenvolvimento.Context
{
    public class TContext : System.Data.Entity.DbContext
    {
        public TContext() : base(nameOrConnectionString: "DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<Models.Imoveis> ImoveisModels { get; set; }

        public System.Data.Entity.DbSet<Models.Clientes> ClientesModels { get; set; }
    }
}