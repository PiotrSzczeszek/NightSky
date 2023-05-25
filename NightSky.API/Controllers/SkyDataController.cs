using DelegateDecompiler;
using Microsoft.AspNetCore.Mvc;
using NightSky.API.Models;
using NightSky.API.Services;

namespace NightSky.API.Controllers;

[ApiController]
[Route("api/sky-data")]
public class SkyDataController : ControllerBase
{
    private readonly ISkyDataService _skyDataService;

    public SkyDataController(ISkyDataService skyDataService)
    {
        _skyDataService = skyDataService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(SkyDataModel model)
    {
        var id = await _skyDataService.AddSkyData(model);

        return Created($"/api/stars/{id}", model);
    }
    
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICollection<SkyDataModel>>> GetBetweenDates(DateTime startDate, DateTime endDate)
    {
        var all = await _skyDataService.GetDatesRange(startDate, endDate);

        return Ok(all);
    }
    
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SkyDataModel>> GetById(int id)
    {
        var skyData = await _skyDataService.GetById(id);
        
        return Ok(skyData);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(SkyDataModel model)
    { 
        await _skyDataService.UpdateSkyData(model);

        return NoContent();
    }
}