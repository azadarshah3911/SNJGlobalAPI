using SNJGlobalAPI.DtoModels;
using System.Data;

namespace SNJGlobalAPI.GeneralServices
{
    public static class UploadFiles
    {
        public static async Task<PathResponder> SaveAsync(IFormFile formFile, string folder)
        {
            try
            {
                //create file name
                string extension = Path.GetExtension(formFile.FileName);
                if (extension.Equals(".mp3"))
                    folder += "/Audio";
                else if (extension.Equals(".pdf") || extension.Equals(".jpg") || extension.Equals(".jepg") || extension.Equals(".doc"))
                    folder += "/Pdf";
                else if (extension.Equals(".xls") || extension.Equals(".xlsx"))
                    folder += "/Excel";
                else
                    return null;

                string fileName = $"{DateTime.Now.ToString("MMddyyHHmmssff")}{extension}";

                //create folder path    
                //string webPath = "";
                //if (_env.IsDevelopment())
                //    webPath = "https://localhost:7008";
                //else
                //    webPath = _env.WebRootPath;

                var folderPath = Path.Combine("", $"Uploads/{folder}");

                //combile folder path and filename
                var filePath = Path.Combine(folderPath, fileName);

                //check if folder not exists then create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //convert into stream and upload file to server path.
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                    fileStream.Flush();
                    return new() { folderPath = folderPath, fileName = fileName , FileType = extension };
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetFolderPath(string type)
        {
            //filter folder path
            if (type.Equals(ObsType.Aud))
                return Folder.Aud;
            else if (type.Equals(ObsType.Pdf))
                return Folder.Pdf;
            else if (type.Equals(ObsType.Img))
                return Folder.Img;
            else if (type.Equals(ObsType.Profile))
                return Folder.Profile;
            else
                return Folder.Gen;
        }//folder path method
    }//upload class
}
