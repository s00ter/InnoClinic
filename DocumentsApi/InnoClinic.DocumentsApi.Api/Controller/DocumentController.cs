using InnoClinic.DocumentsApi.BL.Services.DocumentService;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.DocumentsApi.Api.Controller;

[ApiController]
[Route("api/document")]
public class DocumentController(
    IDocumentService documentService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await documentService.ListAsync();
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, Guid resultId)
    {
        var res = await documentService.UploadAsync(file, resultId);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var res = await documentService.DownloadAsync(id);
        return File(res.Stream, res.ContentType, res.FileName);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await documentService.DeleteAsync(id);
        return Ok("File was deleted");
    }
}