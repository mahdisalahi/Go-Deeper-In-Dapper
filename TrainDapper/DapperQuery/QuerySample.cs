using Dapper;
using TrainDapper.Constant;
using TrainDapper.Helpers;

namespace TrainDapper.DapperQuery
{
    public class QuerySample
    {
        public void QueryAnonymous()
        {
            using (var connection = DapperHelper.ProfilerDbConnection())
            {
                var result = connection.Query(SqlQueriesCons.GetStudentWithFirstName, new { FirstNames = new[] { "TypeName", "TypeName" } });

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.FirstName}, {item.LastName}");
                }

                Console.WriteLine(DapperHelper.GetExecuteCommands());
            }
        }

        public void QueryStronglyType()
        {
            using (var connection = DapperHelper.ProfilerDbConnection())
            {
                var result = connection.Query(SqlQueriesCons.GetStudents);

                foreach (var item in result)
                {
                    Console.WriteLine($"{item.FirstName}, {item.LastName}");
                }

                Console.WriteLine(DapperHelper.GetExecuteCommands());
            }
        }
    }
}
