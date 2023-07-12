namespace WebApplication1.Services.FileServ
{
    public interface IFileService
    {
        string ReadFile(string path, string body);
    }
}
