using GradesMaterAPI.DB.DbModels;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GradesMaterAPI.DB
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository()
        {
            _connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=GradeMasterdb;Integrated Security=True;Connect Timeout=30;";
        }

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            // the using will delete the meomery that allocate when the Connection object is fetched
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                conn.Open();

                var command = new SqlCommand("SELECT * FROM Students", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateBirth = reader.GetDateTime(3),
                            Gender = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Adress = reader.GetString(6),
                            Email = reader.GetString(7),
                            EnrollmentDate = reader.GetDateTime(8),
                        });
                    }
                }

            }
            return students;
        }

        public async Task<string> InsertStudentAsync(Student student)
        {
            string error = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(this._connectionString))
                {
                    string query = "INSERT INTO Students (FirstName, LastName, DateBirth, Gender, PhoneNumber,Adress,Email,EnrollmentDate) " +
                                   "VALUES (@FirstName,@LastName,@DateBirth,@Gender,@PhoneNumber,@Adress,@Email,@EnrollmentDate)";

                    // query - is the query above and the conn - is the connection
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("LastName", student.LastName);
                        cmd.Parameters.AddWithValue("DateBirth", student.DateBirth);
                        cmd.Parameters.AddWithValue("Gender", student.Gender);
                        cmd.Parameters.AddWithValue("PhoneNumber", student.PhoneNumber);
                        cmd.Parameters.AddWithValue("Adress", student.Adress);
                        cmd.Parameters.AddWithValue("Email", student.Email);
                        cmd.Parameters.AddWithValue("EnrollmentDate", student.EnrollmentDate);

                        await conn.OpenAsync();
                        int affectedRows = await cmd.ExecuteNonQueryAsync();
                        if (affectedRows == 0)
                        {
                 
                                error = "Insert Not Commited ";
                            
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                error = "Exception " + ex.Message;
            }
            return error;
        }

        public string UpdateStudent(int studentId, Student student)
        {
            string errors = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DateBirth = @DateBirth, " +
                                   "Gender = @Gender, PhoneNumber = @PhoneNumber, Adress = @Adress, Email = @Email, " +
                                   "EnrollmentDate = @EnrollmentDate WHERE Id = @Id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", student.Id);  // Using the studentId parameter
                        cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", student.LastName);
                        cmd.Parameters.AddWithValue("@DateBirth", student.DateBirth);
                        cmd.Parameters.AddWithValue("@Gender", student.Gender);
                        cmd.Parameters.AddWithValue("@PhoneNumber", student.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Adress", student.Adress);  // Corrected spelling
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@EnrollmentDate", student.EnrollmentDate);

                        conn.Open();
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows == 0)
                        {
                            errors = "Update Not Commited";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors = ex.Message;
            }

            return errors;
        }

        public Student? GetStudent(int id)
        {
            Student student = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                conn.Open();

                var command = new SqlCommand("SELECT * FROM Students WHERE Id = @Id", conn);
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        student = new Student()
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            DateBirth = reader.GetDateTime(3),
                            Gender = reader.GetString(4),
                            PhoneNumber = reader.GetString(5),
                            Adress = reader.GetString(6),
                            Email = reader.GetString(7),
                            EnrollmentDate = reader.GetDateTime(8),
                        };
                    }

                }

            }

            return student;

        }

        public string DeleteStudent(int id)
        {
            string errors = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM Students WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int affectedRows = cmd.ExecuteNonQuery();
                        if (affectedRows == 0)
                        {
                            errors = "Update Not Commited";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors = ex.Message;
            }
            return errors;
        }

    }
}
