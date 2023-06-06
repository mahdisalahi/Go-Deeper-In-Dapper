using Dapper;
using System.Data;
using TrainDapper.Constant;
using TrainDapper.Helpers;

namespace TrainDapper.DapperExecute
{
    public class ExecuteSample
    {
        public int CallCreateCourseStoreProcedureSingle()
        {
            using (var connection = DapperHelper.GetDbConnection())
            {
                var affectedRows = connection.Execute(SqlCommandCons.CallCreateCourseSP, new
                {
                    Title = "Math1",
                    TeacherName = "Teacher1",
                    Capacity = 10
                }, commandType: System.Data.CommandType.StoredProcedure);

                return affectedRows;
            }
        }

        public int CallCreateCourseStoreProcedureMultiple()
        {
            using (var connection = DapperHelper.GetDbConnection())
            {
                var affectedRows = connection.Execute(SqlCommandCons.CallCreateCourseSP,
                    new[] //Anonymous Parameter
                    {
                        new
                        {
                            Title = "Math2",
                            TeacherName = "Teacher1",
                            Capacity = 11
                        },
                        new
                        {
                            Title = "Math3",
                            TeacherName = "Teacher2",
                            Capacity = 12
                        },
                        new
                        {
                            Title = "Math4",
                            TeacherName = "Teacher3",
                            Capacity = 13
                        }
                    }, commandType: System.Data.CommandType.StoredProcedure);

                return affectedRows;
            }
        }

        public string CallCreateCourseStoreProcedureWithDynamicParameters()
        {
            using (var connection = DapperHelper.GetDbConnection())
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Title", "Algorithm", DbType.String, ParameterDirection.Input);
                parameter.Add("@TeacherName", "TeacherAlg");
                parameter.Add("@Capacity", 5);

                parameter.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                connection.Execute(SqlCommandCons.CallCreateCourseSP, parameter, commandType: CommandType.StoredProcedure);

                var res = $"Affected Rows: {parameter.Get<int>("@RowCount")}";
                return res;
            }
        }

        public string CallCreateCourseStoreProcedureWithMultipleDynamicParameters()
        {
            using (var connection = DapperHelper.GetDbConnection())
            {
                var parameters = new List<DynamicParameters>();

                var parameter1 = new DynamicParameters();

                parameter1.Add("@Title", "Algorithm", DbType.String, ParameterDirection.Input);
                parameter1.Add("@TeacherName", "TeacherAlg");
                parameter1.Add("@Capacity", 5);
                parameter1.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                var parameter2 = new DynamicParameters();

                parameter2.Add("@Title", "Algorithm", DbType.String, ParameterDirection.Input);
                parameter2.Add("@TeacherName", "TeacherAlg");
                parameter2.Add("@Capacity", 5);
                parameter2.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

                parameters.Add(parameter1);
                parameters.Add(parameter2);

                connection.Execute(SqlCommandCons.CallCreateCourseSP, parameters.ToArray(), commandType: CommandType.StoredProcedure);

                var sumRowCount = parameters.Sum(c => c.Get<int>("@RowCount"));

                var res = $"Affected Rows: {sumRowCount}";
                return res; 
            }
        }
    }
}
