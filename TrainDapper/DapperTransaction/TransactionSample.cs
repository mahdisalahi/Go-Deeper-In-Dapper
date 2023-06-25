using Dapper;
using TrainDapper.Constant;
using TrainDapper.Helpers;
using TrainDapper.Models;

namespace TrainDapper.DapperTransaction
{
    public class TransactionSample
    {
        public void TransactionTest()
        {
            using (var cnn = DapperHelper.GetDbConnection())
            {
                cnn.Open();
                using (var transaction = cnn.BeginTransaction())
                {
                    var addedCourseCount = cnn.Execute(SqlCommandCons.InsertCourse,
                        new[]
                        {
                            new { Title = "Biology", TeacherFullname = "ali akbari", Capacity = 15 },
                            new { Title = "chemistry", TeacherFullname = "amir akbari", Capacity = 15 },
                            new { Title = "Physics", TeacherFullname = "iman akbari", Capacity = 15 }
                        }, transaction: transaction);

                    var addedStudentsCount = cnn.Execute(SqlCommandCons.InsertInStudent, new Student[]
                    {
                        new Student()
                        {
                            FirstName = "mina",
                            LastName = "minaei",
                            NationalCode = "0123456789",
                            BirthDate = DateTime.Now.AddYears(-30)
                        },
                        new Student()
                        {
                            FirstName = "sina",
                            LastName = "minaei",
                            NationalCode = "0123456789123",
                            BirthDate = DateTime.Now.AddYears(-30)
                        }
                    }, transaction: transaction);

                    transaction.Commit();

                    Console.WriteLine($"Courses Count:{addedCourseCount}");
                    Console.WriteLine($"students Count:{addedStudentsCount}");
                }
            }
        }
    }
}
