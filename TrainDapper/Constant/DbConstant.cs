namespace TrainDapper.Constant
{
    public class DbConstant
    {
        private readonly IConfiguration _configuration;
        public static string ConnectionString;
        public DbConstant(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("SampleDbForDapper");
        }
    }
}
