namespace TrainDapper.Constant
{
    public class SqlCommandCons
    {
        #region Course TB

        public static string CallCreateCourseSP = "CreateCourse";

        #endregion

        #region Student TB

        public static string InsertInStudent = @"insert into Students (FirstName, LastName, NationalCode, BirthDate) 
                                                values (@FirstName, @LastName, @NationalCode, @BirthDate);";
        public static string UpdateStudentById = "update Students set FirstName = @FirstName where Id = @Id;";
        public static string DeleteStudentById = "delete from Students where Id = @Id;";
        public static string DeleteStudentByFirstName = "delete from Students where FirstName = @FirstName;";

        #endregion
    }
}
