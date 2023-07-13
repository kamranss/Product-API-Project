namespace WebApplication1.Helper.FileExten
{
    public static partial class FileExtension
    {
        public static bool CheckIsFileUploaded(this IFormFile file)
        {
            return file == null;
        }
    }
}
