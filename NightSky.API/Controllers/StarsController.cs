using Microsoft.AspNetCore.Mvc;
using NightSky.API.Models;
using NightSky.API.Services;

namespace NightSky.API.Controllers;

[ApiController]
[Route("api/stars")]
public class StarsController : ControllerBase
{
    private readonly IStarService _starService;

    public StarsController(IStarService starService)
    {
        _starService = starService;
    }

    /// <summary>
    /// Creates new star
    /// </summary>
    /// <param name="model">New star model</param>
    /// <returns>URI to new created object</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(StarModel model)
    {
        var id = await _starService.AddStar(model);

        return Created($"/api/stars/{id}", model);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICollection<StarModel>>> GetAll()
    {
        var all = await _starService.GetAll();

        return Ok(all);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StarModel>> GetById(int id)
    {
        var star = await _starService.GetById(id);
        
        return Ok(star);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(StarModel model)
    { 
        await _starService.UpdateStar(model);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _starService.DeleteStar(id);

        return NoContent();
    }
}