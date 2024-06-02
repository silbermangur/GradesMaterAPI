namespace GradesMaterAPI.DB.DbModels
{
    public class Course
    {
        int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }

        public string TeacherId { get; set; } 

        public Teacher Teacher { get; set; }  
    }
}
