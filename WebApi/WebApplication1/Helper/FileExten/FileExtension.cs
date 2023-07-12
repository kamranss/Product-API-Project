﻿namespace WebApplication1.Helper.FileExten
{
    public static class FileExtension
    {
        public static bool CheckFileType(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }

        public static bool CheckFileLenght(this IFormFile file, int size)
        {
            return file.Length < size;
        }

        public static bool CheckIsFileUploaded(this IFormFile file)
        {
            return file == null;
        }

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
        public static void DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath)) // checking whether path exist
            {
                System.IO.File.Delete(filePath); // deleting file from the path
            }
        }
    }
}
