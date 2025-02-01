using InnoClinic.DocumentsApi.DAL.Models;
using InnoClinic.DocumentsApi.DAL.Records;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.DocumentsApi.BL.Services.PhotoService;

public interface IPhotoService
{
    Task<List<PhotoBlobDto>> ListAsync();
    Task<Guid> UploadAsync(IFormFile blob);
    Task<FileResponse> DownloadAsync(Guid id);
    Task DeleteAsync(Guid id);
}