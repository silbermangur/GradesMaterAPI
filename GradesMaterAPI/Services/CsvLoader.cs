namespace GradesMaterAPI.Services
{
    public class CsvLoader : ICsvLoader
    {
        string testVal = "";
        public CsvLoader()
        {
            testVal = Guid.NewGuid().ToString();
        }

     

        public string test()
        {
            throw new NotImplementedException();
        }
    }
}
