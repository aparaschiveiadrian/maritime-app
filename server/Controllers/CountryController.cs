using Microsoft.AspNetCore.Mvc;
using server.Dtos.Country;
using server.Services;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly CountryService _service;

        public CountryController(CountryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var countries = _service.GetAll();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var country = _service.GetById(id);
            if (country == null) return NotFound();

            return Ok(country);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CountryRequestDto dto)
        {
            var created = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), 
                new { id = created.IdCountry }, 
                created
                );
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CountryRequestDto dto)
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
}