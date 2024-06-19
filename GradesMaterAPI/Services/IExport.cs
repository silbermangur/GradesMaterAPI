namespace GradesMaterAPI.Services
{
    public interface IExport<T>
    {
        public void Export(string path, List<T> list);
    }
}