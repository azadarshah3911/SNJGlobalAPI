using AutoMapper;
using SNJGlobalAPI.DtoModels;
using SNJGlobalAPI.GeneralServices;
using SNJGlobalAPI.Repositories.CommonInterfaces;

namespace SNJGlobalAPI.Repositories.CommonRepos
{
    public class UploadFileRepo : IUploadFile
    {
        private string UploadDirectory => Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        public async Task<Responder<string>> UploadFile(UploadFileDto dto, string folder)
        {
            var finalFilechunks = "Uploads/Test";
            if (dto.File == null || dto.File.Length <= 0)
                return Rr.NoData<string>("File");

            // Generate a unique filename for each chunk
            var chunkFilename = $"{dto.File.FileName}.part{dto.CurrentChunk}";
            if (!Directory.Exists(finalFilechunks))
                Directory.CreateDirectory(finalFilechunks);
            // Save the chunk
            var chunkFilePath = Path.Combine(chunkFilename, chunkFilename);
          

            using (var stream = new FileStream(chunkFilePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            // If it's the last chunk, merge all the chunks into the final file
            if (dto.CurrentChunk == dto.TotalChunks)
            {
                var finalFilePath = "Uploads/Test";
                //check if folder not exists then create it
                if (!Directory.Exists(finalFilePath))
                {
                    Directory.CreateDirectory(finalFilePath);
                }
                foreach (var chunkPath in Directory.EnumerateFiles(finalFilechunks, $"{dto.File.FileName}.part*"))
                {
                    using (var chunkStream = new FileStream(chunkPath, FileMode.Open))
                    using (var finalStream = new FileStream(finalFilePath, FileMode.Append))
                    {
                        await chunkStream.CopyToAsync(finalStream);
                    }
                    File.Delete(chunkPath); // Delete the individual chunk files after merging
                }
                return Rr.Success<string>("File uploaded successfully");
            }

            return Rr.Success<string>("Chunk uploaded successfully");
        }




        /* public async Task<Responder<string>> UploadFile(UploadFileDto dto, string folder)
         {
             if (dto.File != null)
             {
                 if (dto.CurrentChunk == 0)
                 {
                     await ClearUploadDirectory(folder);
                 }

                 var folderPath = Path.Combine(UploadDirectory, folder);
                 var filePath = Path.Combine(folderPath, dto.File.FileName + ".part" + dto.CurrentChunk);

                 if (!Directory.Exists(folderPath))
                 {
                     Directory.CreateDirectory(folderPath);
                 }

                 using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                 {
                     await dto.File.CopyToAsync(stream);
                 }

                 if (dto.CurrentChunk == dto.TotalChunks - 1)
                 {
                     var combinedFilePath = Path.Combine(folderPath, dto.File.FileName);
                     await CombineFileChunks(combinedFilePath, dto.TotalChunks);

                     // Perform additional operations on the complete file
                     // ...

                     return Rr.Success<string>("done");
                 }
             }

             return Rr.Fail<string>("File");
         }

         private async Task ClearUploadDirectory(string folder)
         {
             var directoryPath = Path.Combine(UploadDirectory, folder);

             if (Directory.Exists(directoryPath))
             {
                 var directoryInfo = new DirectoryInfo(directoryPath);
                 foreach (var file in directoryInfo.GetFiles())
                 {
                     await DeleteFileWithRetry(file.FullName);
                 }
             }
             else
             {
                 Directory.CreateDirectory(directoryPath);
             }
         }

         private async Task CombineFileChunks(string combinedFilePath, int totalChunks)
         {
             using (var combinedStream = new FileStream(combinedFilePath, FileMode.Create, FileAccess.Write))
             {
                 for (var currentChunk = 0; currentChunk < totalChunks; currentChunk++)
                 {
                     var chunkFilePath = combinedFilePath + ".part" + currentChunk;
                     using (var chunkStream = new FileStream(chunkFilePath, FileMode.Open, FileAccess.Read))
                     {
                         await chunkStream.CopyToAsync(combinedStream);
                     }
                 }
             }

             // Delete the chunk files
             for (var currentChunk = 0; currentChunk < totalChunks; currentChunk++)
             {
                 var chunkFilePath = combinedFilePath + ".part" + currentChunk;
                     System.IO.File.Delete(chunkFilePath);
             }
         }
 */

    }



}
