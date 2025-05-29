using System.Collections.Frozen;
using InnoClinic.DocumentsApi.BL.Records;
using InnoClinic.DocumentsApi.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.DocumentsApi.BL.Services.PhotoService;

public interface IPhotoService
{
    Task<FrozenSet<PhotoBlob>> ListAsync(
        CancellationToken cancellationToken = default);
    Task<Guid> UploadAsync(IFormFile blob, 
        CancellationToken cancellationToken = default);
    Task<FileResponse> DownloadAsync(Guid id, 
        CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, 
        CancellationToken cancellationToken = default);
}