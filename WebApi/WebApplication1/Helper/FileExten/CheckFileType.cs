namespace WebApplication1.Helper.FileExten
{
    public static partial class FileExtension
    {
        public static bool CheckFileType(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
    }
}
