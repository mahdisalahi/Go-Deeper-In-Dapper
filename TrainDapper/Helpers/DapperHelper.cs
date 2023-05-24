using MiniProfiler.Integrations;
using System.Data;
using System.Data.SqlClient;
using TrainDapper.Constant;

namespace TrainDapper.Helpers
{
    public class DapperHelper
    {
        public static IDbConnection GetDbConnection()
        {
            return new SqlConnection(DbConstant.ConnectionString);
        }

        public static CustomDbProfiler GetCustomDbProfiler()
        {
            return CustomDbProfiler.Current;
        }

        public static IDbConnection ProfilerDbConnection()
        {
            var fac = new SqlServerDbConnectionFactory(DbConstant.ConnectionString);
            return ProfiledDbConnectionFactory.New(fac, GetCustomDbProfiler());
        }

        public static string GetExecuteCommands()
        {
            var res = GetCustomDbProfiler().ProfilerContext.GetExecutedCommands();
            GetCustomDbProfiler().ProfilerContext.Reset();
            return res;
        }
    }
}
