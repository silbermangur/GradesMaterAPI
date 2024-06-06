namespace GradesMaterAPI.DB.DbModels
{
    public class ExamSubmission
    {
        public int Id { get; set; }
        public string ExamFilePath { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public DateTime SubmittionDate { get; set; }
        public int Grade { get; set; }
      


        // nevigation prop
        public Exam Exam { get; set; }
        public Student Student { get; set; }
    }
}
