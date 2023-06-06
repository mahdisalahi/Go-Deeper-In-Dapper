namespace TrainDapper.DTOs
{
    public class FinalScoreDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double? ScoreEarned { get; set; }
        public override string ToString()
        {
            return $"{StudentId}, {CourseId} = {ScoreEarned}";
        }
    }
}
