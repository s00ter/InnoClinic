using InnoClinic.ServiceApi.BusinessLogic.Dto.ServiceCategory;
using InnoClinic.ServiceApi.BusinessLogic.Services.ServiceCategoryService;
using InnoClinic.ServiceApi.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.ServiceApi.Api.Controllers;

[ApiController]
[Route("api/serviceCategory")]
public class ServiceCategoryController(
    IServiceCategoryService serviceCategoryService
    ) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] QueryObject query)
    {
        var res = await serviceCategoryService.GetAllServiceCategories(query);
        return Ok(res);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var res = await serviceCategoryService.GetServiceCategoryInfo(id.ToString());
        return Ok(res);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCategoryDto dto)
    {
        var res = await serviceCategoryService.CreateServiceCategory(dto);
        return Ok(res);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceCategoryDto dto)
    {
        var res = await serviceCategoryService.UpdateServiceCategory(id.ToString(), dto);
        return Ok(res);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var res = await serviceCategoryService.DeleteServiceCategory(id.ToString());
        return Ok(res);
    }
}