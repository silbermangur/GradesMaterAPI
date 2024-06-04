﻿namespace GradesMaterAPI.DB.DbModels
{
    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        //TODO Enum (Point/precentage)

         
        public Course Course { get; set; }

        public AssignmentSubmission assignmentSubmission { get; set; }
    }
}
