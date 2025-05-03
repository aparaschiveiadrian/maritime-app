using Microsoft.AspNetCore.Mvc;
using server.Dtos.Voyage;
using server.Services;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VoyageController : ControllerBase
{
    private readonly VoyageService _service;

    public VoyageController(VoyageService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var voyages = _service.GetAll();
        return Ok(voyages);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var voyage = _service.GetById(id);
        return voyage == null ? NotFound() : Ok(voyage);
    }

    [HttpPost]
    public IActionResult Create([FromBody] VoyageRequestDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdVoyage }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] VoyageRequestDto dto)
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