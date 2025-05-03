using Microsoft.AspNetCore.Mvc;
using server.Dtos.Port;
using server.Services;
using server.Services.Interfaces;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortController : ControllerBase
{
    private readonly IPortService _service;

    public PortController(IPortService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var ports = _service.GetAll();
        return Ok(ports);
    }

    [HttpGet("country")]
    public IActionResult GetAllWithCountryNames()
    {
        var ports = _service.GetPortsWithCountryNames();
        return Ok(ports);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var port = _service.GetById(id);
        if (port == null) return NotFound();

        return Ok(port);
    }

    [HttpPost]
    public IActionResult Create([FromBody] PortRequestDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdPort }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] PortRequestDto dto)
    {
        var updated = _service.Update(id, dto);
        if (updated == null) return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _service.Delete(id);
        if (deleted == null) return NotFound();

        return Ok(deleted);
    }
}