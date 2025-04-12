using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace Application.Common
{
    public class FileProcessingHandler : IFileProcessingHandler
    {
        public FileProcessingHandler()
        {

        }

        // can use this for very small file less than 1Mb
        public async Task<(byte[] data, string contentType, string fileName)> ConvertFormFileToBlobAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return (Array.Empty<byte>(), string.Empty, string.Empty);

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return (memoryStream.ToArray(), file.ContentType, file.FileName);
        }

        // can use this for very large file as larger than 1Mb
        public async Task<(string tempFilePath, string contentType, string fileName)> StreamFileToTempAsync(IFormFile file, int chunkSize = 300 * 1024)
        {
            if (file == null || file.Length == 0)
                return (string.Empty, string.Empty, string.Empty);

            var tempFilePath = Path.GetTempFileName();

            using var fileStream = file.OpenReadStream();
            using var outputStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: chunkSize, useAsync: true);
            {
                var buffer = new byte[chunkSize];
                int bytesRead;
                while ((bytesRead = await fileStream.ReadAsync(buffer.AsMemory(0, chunkSize))) > 0)
                {
                    await outputStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                }
            }

            return (tempFilePath, file.ContentType, file.FileName);
        }

    }

}