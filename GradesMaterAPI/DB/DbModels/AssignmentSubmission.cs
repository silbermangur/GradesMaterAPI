namespace GradesMaterAPI.DB.DbModels
{
    public class AssignmentSubmission
    {
        public int Id { get; set; }
        public string FilePath { get; set; }   
        public string AssignmentName { get; set; }
        public DateTime SubmittionDate { get; set; }   
        public string Feedback {  get; set; }  
        public int Grade {  get; set; } 
        public int StudentId { get; set; }

        public int AssignmentId { get; set; }  


        // nevigation prop
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }


    }
}
