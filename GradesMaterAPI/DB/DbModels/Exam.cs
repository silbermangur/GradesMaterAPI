namespace GradesMaterAPI.DB.DbModels
{
    public class Exam
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string ExamName { get; set; }
        public string ExamDescription { get; set; }
        public DateTime ExamDate { get; set; }
        public int Duration { get; set; }
        public string RoomNumber { get; set; } 
        public Course course { get; set; }

        public ICollection<ExamSubmission> examSubmissions { get; set; }
    }
}
