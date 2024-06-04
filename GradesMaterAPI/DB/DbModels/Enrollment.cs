﻿namespace GradesMaterAPI.DB.DbModels
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public float FinalGrade { get; set; }

        public Student student { get; set; }
        public Course course { get; set; }
    }
}
