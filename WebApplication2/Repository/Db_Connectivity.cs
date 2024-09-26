namespace WebApplication2.Repository
{
    public class Db_Connectivity
    {
        public string ConnectionString {  get; private set; }
        public Db_Connectivity()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var root=builder.Build();
            ConnectionString = root.GetConnectionString("constr");
        }
    }
}
