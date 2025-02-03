using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using InnoClinic.DocumentsApi.DAL.Models;
using InnoClinic.DocumentsApi.DAL.Records;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.DocumentsApi.BL.Services.DocumentService;

public class DocumentService(
    BlobServiceClient blobServiceClient
    ) : IDocumentService
{
    readonly BlobContainerClient _containerClient = blobServiceClient.GetBlobContainerClient("documents");
    
    public async Task<List<DocumentBlobDto>> ListAsync()
    {
        var blobsList = new List<DocumentBlobDto>();
        await foreach (BlobItem blobItem in _containerClient.GetBlobsAsync())
        {
            var blobClient = _containerClient.GetBlobClient(blobItem.Name);

            var properties = await blobClient.GetPropertiesAsync();
            var metadata = properties.Value.Metadata;
            metadata.TryGetValue("ResultId", out var resultId);
            metadata.TryGetValue("FileName", out var fileName);
            
            blobsList.Add(new DocumentBlobDto
            {
                Id = blobItem.Name,
                Name = fileName,
                ResultId = resultId,
                ContentType = blobItem.Properties.ContentType,
                ByteSize = blobItem.Properties.ContentLength,
            });
        }
        return blobsList;
    }
    
    public async Task<Guid> UploadAsync(IFormFile blob, Guid resultId)
    {
        var fileId = Guid.NewGuid();
        var blob1 = _containerClient.GetBlobClient(fileId.ToString());
        
        await using (Stream data = blob.OpenReadStream())
        {
            await blob1.UploadAsync(data, new BlobHttpHeaders
            {
                ContentType = blob.ContentType
            });
        }
        
        var metadata = new Dictionary<string, string>
        {
            { "FileName", blob.FileName },
            { "ResultId", resultId.ToString() }
        };

        await blob1.SetMetadataAsync(metadata);
        
        return fileId;
    }

    public async Task<FileResponse> DownloadAsync(Guid id)
    {
        BlobClient file = _containerClient.GetBlobClient(id.ToString());
        var properties = await file.GetPropertiesAsync();
        var metadata = properties.Value.Metadata;
        metadata.TryGetValue("FileName", out var fileName);

        if (await file.ExistsAsync())
        {
            var content = await file.DownloadContentAsync();

            return new FileResponse(content.Value.Content.ToStream(), content.Value.Details.ContentType, fileName);
        }
        return null;
    }

    public async Task DeleteAsync(Guid id)
    {
        BlobClient file = _containerClient.GetBlobClient(id.ToString());
        await file.DeleteIfExistsAsync();
    }
}