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
                    Title = "Math",
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
                var affectedRows = connection.Execute(SqlCommandCons.CallCreateCourseSP, new
                {
                    Title = "Math",
                    TeacherName = "Teacher1",
                    Capacity = 10
                }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
