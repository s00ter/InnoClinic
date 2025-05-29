using System.Collections.Frozen;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using InnoClinic.DocumentsApi.BL.Records;
using InnoClinic.DocumentsApi.DAL.Models;
using Microsoft.AspNetCore.Http;

namespace InnoClinic.DocumentsApi.BL.Services.PhotoService;

public class PhotoService(
    BlobServiceClient blobServiceClient
    ) : IPhotoService
{
    private readonly BlobContainerClient _containerClient = blobServiceClient.GetBlobContainerClient("photos");
    
    public async Task<FrozenSet<PhotoBlob>> ListAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var blobsList = new List<PhotoBlob>();
        await foreach (var blobItem in _containerClient.GetBlobsAsync())
        {
            var blobClient = _containerClient.GetBlobClient(blobItem.Name);
            var properties = await blobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
            var metadata = properties.Value.Metadata;
            
            metadata.TryGetValue("FileName", out var fileName);
            
            blobsList.Add(new PhotoBlob
            {
                Id = blobItem.Name,
                Name = fileName,
                ContentType = blobItem.Properties.ContentType,
                ByteSize = blobItem.Properties.ContentLength,
            });
        }
        return blobsList.ToFrozenSet();
    }
    
    public async Task<Guid> UploadAsync(IFormFile blob, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var fileId = Guid.NewGuid();
        var blob1 = _containerClient.GetBlobClient(fileId.ToString());
        
        await using (var data = blob.OpenReadStream())
        {
            await blob1.UploadAsync(data, new BlobHttpHeaders
            {
                ContentType = blob.ContentType
            }, cancellationToken: cancellationToken);
        }
        
        var metadata = new Dictionary<string, string>
        {
            { "FileName", blob.FileName }
        };
        
        await blob1.SetMetadataAsync(metadata, cancellationToken: cancellationToken);
        
        return fileId;
    }

    public async Task<FileResponse> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var file = _containerClient.GetBlobClient(id.ToString());
        
        if (!await file.ExistsAsync(cancellationToken))
        {
            throw new Exception("Photo not found");
        }
        
        var properties = await file.GetPropertiesAsync(cancellationToken: cancellationToken);
        var metadata = properties.Value.Metadata;
        metadata.TryGetValue("FileName", out var fileName);
        var content = await file.DownloadContentAsync(cancellationToken);

        return new FileResponse(content.Value.Content.ToStream(), content.Value.Details.ContentType, fileName);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var file = _containerClient.GetBlobClient(id.ToString());
        await file.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}