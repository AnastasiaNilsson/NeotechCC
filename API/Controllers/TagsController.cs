using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagsController : ControllerBase
{
    private GoogleSheets _googleSheets;
    public TagsController(GoogleSheets googleSheets) => _googleSheets = googleSheets;

    [HttpGet]
    public ActionResult<IEnumerable<TagDTO>> GetTags([FromQuery] [RegularExpression(@"^(?i)(se|en)$")] string localisation)
    {
        return Program.AllTags.Select(tag => tag.ToDTO(localisation.ToLocalisationEnum())).ToList();
    }
    
    [HttpGet("{id}")]
    public ActionResult<TagDTO> GetTag(string localisation, int id)
    {
        var tag = Program.AllTags.FirstOrDefault(tag => tag.Id == id);
        if (tag is null) return NotFound();

        return tag.ToDTO(localisation.ToLocalisationEnum());
    }

    [HttpPut]
    public async Task<IActionResult> PutDataAsync()
    {
        Program.AllTags = await _googleSheets.RefreshDataOfType<Tag>();
        return Ok();
    }
}
