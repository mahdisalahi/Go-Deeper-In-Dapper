using Dapper;
using TrainDapper.Constant;
using TrainDapper.DTOs;
using TrainDapper.Enums;
using TrainDapper.Helpers;
using TrainDapper.Models;

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
                var result = connection.Query<Student>(SqlQueriesCons.GetStudents);

                foreach (var item in result)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine(DapperHelper.GetExecuteCommands());
            }
        }

        public void QueryOneToOne()
        {
            using (var connection = DapperHelper.ProfilerDbConnection())
            {
                var result = connection.Query<Student, StudentAdditionalInfo, Student>(SqlQueriesCons.GetStudentsJoinWithStudentAdditionalInfo,
                    (student, info) =>
                    {
                        student.StudentAdditionalInfo = info;
                        return student;
                    }, splitOn: "Id");

                Console.WriteLine(DapperHelper.GetExecuteCommands());
            }
        }

        public void QueryOneToMany()
        {
            using (var connection = DapperHelper.ProfilerDbConnection())
            {
                var studentDic = new Dictionary<int, Student>();

                var result = connection.Query<Student, Score, Student>(SqlQueriesCons.GetStudentsJoinWithScores,
                    (student, score) =>
                    {
                        Student tempStudent;

                        if (!studentDic.TryGetValue(student.Id, out tempStudent))
                        {
                            tempStudent = student;
                            tempStudent.Scores = new List<Score>();
                            studentDic.Add(tempStudent.Id, tempStudent);
                        }
                        tempStudent.Scores.Add(score);
                        return tempStudent;
                    }, splitOn: "Id").Distinct().ToList();

                foreach (var student in result)
                {
                    Console.WriteLine(student);
                    if (student.Scores.Any())
                    {
                        foreach (var score in student.Scores)
                        {
                            Console.WriteLine(score);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Score!");
                    }
                }
            }
        }

        public static void QueryMultipleType()
        {
            var finalScores = new List<FinalScoreDto>();
            var midTermScores = new List<MidTermScoreDto>();
            using (var connection = DapperHelper.GetDbConnection())
            {
                using (var reader = connection.ExecuteReader(SqlQueriesCons.GetScores))
                {
                    var midTermScoreParser = reader.GetRowParser<MidTermScoreDto>();
                    var finalScoreParser = reader.GetRowParser<FinalScoreDto>();

                    while (reader.Read())
                    {
                        switch ((ScoreTypeEnum)reader.GetInt32(reader.GetOrdinal("ScoreType")))
                        {
                            case ScoreTypeEnum.MidTerm:
                                midTermScores.Add(midTermScoreParser(reader));
                                break;
                            case ScoreTypeEnum.Final:
                                finalScores.Add(finalScoreParser(reader));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            Console.WriteLine("midterm:");
            foreach (var score in midTermScores)
            {
                Console.WriteLine(score.ToString());
            }
            Console.WriteLine("final:");
            foreach (var score in finalScores)
            {
                Console.WriteLine(score.ToString());
            }
        }

    }
}
