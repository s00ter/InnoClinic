using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;
using InnoClinic.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ServiceApi.Api.Controllers;

[ApiController]
[Authorize(Policy = "OnlyForMembers")]
[Route("api/serviceCategories")]
public class ServiceCategoryController(
    IServiceCategoryService serviceCategoryService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryPaginationArguments query, 
        CancellationToken cancellationToken)
    {
        var res = await serviceCategoryService.GetAllServiceCategoriesAsync(query, cancellationToken);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        var res = await serviceCategoryService.GetServiceCategoryInfoAsync(id, cancellationToken);
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCategoryRequest request, 
        CancellationToken cancellationToken)
    {
        var res = await serviceCategoryService.CreateServiceCategoryAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = res.Id }, res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceCategoryRequest request, 
        CancellationToken cancellationToken)
    {
        await serviceCategoryService.UpdateServiceCategoryAsync(id, request, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        await serviceCategoryService.DeleteServiceCategoryAsync(id, cancellationToken);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}