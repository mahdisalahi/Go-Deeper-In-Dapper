using Dapper;
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

        public void CallCreateCourseStoreProcedureMultiple()
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
            }
        }
    }
}
