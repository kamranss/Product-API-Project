namespace WebApplication1.Helper.FileExten
{
    public static partial class FileExtension
    {
        public static void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath)) // checking whether path exist
            {
                System.IO.File.Delete(filePath); // deleting file from the path
            }
        }
    }
}
