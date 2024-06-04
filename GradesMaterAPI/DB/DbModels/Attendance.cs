namespace GradesMaterAPI.DB.DbModels
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public string RoomNumber { get; set; }

        public DateTime Start {  get; set; }
        public int DurationMinuets { get; set; }
      
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public Student student { get; set; }
        public Course course { get; set; }

    }
}
