using InnoClinic.DocumentsApi.DAL.Models;
using InnoClinic.DocumentsApi.DAL.Records;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.DocumentsApi.BL.Services.DocumentService;

public interface IDocumentService
{
    Task<List<DocumentBlobDto>> ListAsync();
    Task<Guid> UploadAsync(IFormFile blob, Guid resultId);
    Task<FileResponse> DownloadAsync(Guid id);
    Task DeleteAsync(Guid id);
}

