using InnoClinic.DocumentsApi.BL.Services.PhotoService;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.DocumentsApi.Api.Controller;

[ApiController]
[Route("api/photo")]
public class PhotoController(
    IPhotoService photoService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res = await photoService.ListAsync();
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var res = await photoService.UploadAsync(file);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var res = await photoService.DownloadAsync(id);
        return File(res.Stream, res.ContentType, res.FileName);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await photoService.DeleteAsync(id);
        return Ok("Photo mas deleted");
    }
}