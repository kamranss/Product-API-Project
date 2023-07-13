namespace WebApplication1.Helper.FileExten
{
    public static partial class FileExtension
    {
        public static bool CheckFileLenght(this IFormFile file, int size)
        {
            return file.Length < size;
        }

    }
}
