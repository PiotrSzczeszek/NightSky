using Microsoft.AspNetCore.Mvc;
using NightSky.API.Models;
using NightSky.API.Services;

namespace NightSky.API.Controllers;

[ApiController]
[Route("api/constallations")]
public class ConstellationsController : ControllerBase
{
    private readonly IConstellationService _constellationService;

    public ConstellationsController(IConstellationService starService)
    {
        _constellationService = starService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(ConstellationModel model)
    {
        var id = await _constellationService.AddConstellation(model);

        return Created($"/api/stars/{id}", model);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICollection<ConstellationModel>>> GetAll()
    {
        var all = await _constellationService.GetAll();

        return Ok(all);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ConstellationModel>> GetById(int id)
    {
        var star = await _constellationService.GetById(id);
        
        return Ok(star);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(ConstellationModel model)
    { 
        await _constellationService.UpdateConstellation(model);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _constellationService.DeleteConstellation(id);

        return NoContent();
    }
}