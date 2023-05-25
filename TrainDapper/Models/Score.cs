using TrainDapper.Enums;

namespace TrainDapper.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public ScoreTypeEnum ScoreType { get; set; }
        public double? ScoreEarned { get; set; }

        public override string ToString()
        {
            return $"{StudentId}, {CourseId}, {ScoreType} = {ScoreEarned}";
        }
    }
}
