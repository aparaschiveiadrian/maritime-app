using Microsoft.AspNetCore.Mvc;
using server.Dtos.Ship;
using server.Services;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipController : ControllerBase
{
    private readonly IShipService _service;

    public ShipController(IShipService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var ships = _service.GetAll();
        return Ok(ships);
    }

    [HttpGet("id")]
    public IActionResult GetById(int id)
    {
        var ship = _service.GetById(id);
        return ship == null ? NotFound() : Ok(ship);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ShipRequestDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdShip }, created);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] ShipRequestDto dto)
    {
        var updated = _service.Update(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        return deleted == null ? NotFound() : Ok(deleted);
    }
}