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

    [HttpPost]
    public async Task<IActionResult> Create(StarModel model)
    {
        var id = await _starService.AddStar(model);

        return Created($"/api/stars/{id}", model);
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<StarModel>>> GetAll()
    {
        var all = await _starService.GetAll();

        return Ok(all);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StarModel>> GetById(int id)
    {
        var star = await _starService.GetById(id);
        
        return Ok(star);
    }

    [HttpPut]
    public async Task<IActionResult> Update(StarModel model)
    { 
        await _starService.UpdateStar(model);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _starService.DeleteStar(id);

        return NoContent();
    }
}