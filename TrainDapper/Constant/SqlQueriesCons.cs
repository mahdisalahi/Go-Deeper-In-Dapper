namespace TrainDapper.Constant
{
    public class SqlQueriesCons
    {
        #region Students TB

        public static string GetStudentWithFirstName = "select * from Students where FirstName in @FirstNames";
        private static string GetStudents = "select * from Students";
        private static string GetCustomColumnsStudents = "select FirstName Name, LastName Family from Students";
        private static string GetStudentsJoinWithStudentAdditionalInfo = "select * from Students as s inner join StudentAdditionalInfo as a  on s.id=a.id";
        private static string GetStudentsJoinWithScores = "select * from Students as s inner join Scores as a  on s.id=a.studentId";

        #endregion

        #region Scores TB

        private static string GetScores = "select * from Scores";

        #endregion
    }
}
