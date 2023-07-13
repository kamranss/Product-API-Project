namespace WebApplication1.Helper.FileExten
{
    public static partial class FileExtension
    {
        public static string SaveFile(this IFormFile file, IWebHostEnvironment _webHostEnvironment, string folderName)
        {
            string fileName = Guid.NewGuid() + file.FileName; // this is generating new random name 
            string path = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
