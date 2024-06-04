namespace GradesMaterAPI.DB.DbModels
{
    public class Course
    {
        int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }

        public string TeacherId { get; set; } 

        public Teacher Teacher { get; set; }

        #region -- Navigation Properties -- 
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        #endregion
    }
}
