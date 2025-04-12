using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IFileProcessingHandler
    {
        Task<(byte[] data, string contentType, string fileName)> ConvertFormFileToBlobAsync(IFormFile file);

        Task<(string tempFilePath, string contentType, string fileName)> StreamFileToTempAsync(IFormFile file, int chunkSize = 300 * 1024);
    }
}