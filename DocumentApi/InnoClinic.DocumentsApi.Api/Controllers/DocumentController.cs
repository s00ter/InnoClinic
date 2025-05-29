using InnoClinic.DocumentsApi.BL.Services.DocumentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.DocumentsApi.Api.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/documents")]
public class DocumentController(
    IDocumentService documentService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var res = await documentService.ListAsync(cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, Guid resultId, CancellationToken cancellationToken)
    {
        var res = await documentService.UploadAsync(file, resultId, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Download(Guid id, CancellationToken cancellationToken)
    {
        var res = await documentService.DownloadAsync(id, cancellationToken);
        return File(res.Stream, res.ContentType, res.FileName);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await documentService.DeleteAsync(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}