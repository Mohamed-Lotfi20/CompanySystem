using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CompanySystem.PL.Helper
{
    public class DocumentSetting
    {
        public static string UploadImage(IFormFile file, string folderName)
        {
            // 1. Get Located Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", folderName);

            // Step 2 : Get File Name And Make It Unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
         
            // Step 3 : Get File Path
            string FilePath = Path.Combine(FolderPath, FileName);

            using (var NewStream = new FileStream(FilePath, FileMode.CreateNew))
            {
                file.CopyTo(NewStream);
            }

            return FileName;
        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            if (FileName is not null && FolderName is not null)
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName, FileName);
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
            }
        }

    }
}
