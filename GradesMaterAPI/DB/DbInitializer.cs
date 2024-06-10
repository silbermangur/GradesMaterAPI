using GradesMaterAPI.DB.DbModels;

namespace GradesMaterAPI.DB
{
    // static class - mean that all the function and variables are static.
    public static class DbInitializer
    {
        // dbContext is like a seasson for the database
        public static void Initialize(GradeMasterDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            // Any() - check if there any rows in the table
            if(!dbContext.Teachers.Any())
            {
                var teacher = new DB.DbModels.Teacher[]
            {
                new Teacher {FirstName = "Albert", LastName="Einstein", Email="aa@gmail.com", PhoneNumber="0545545497", Password="12345"},
                new Teacher {FirstName = "david", LastName="becham", Email="abb@gmail.com", PhoneNumber="0545789456", Password="12345"}
            };
                dbContext.Teachers.Add(teacher[0]);
                dbContext.Teachers.Add(teacher[1]);
                dbContext.SaveChanges();
            }
           
        }
    }
}
