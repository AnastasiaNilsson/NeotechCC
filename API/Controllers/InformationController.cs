using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class InformationController : ControllerBase
{

    public static Localisation GetLocalisation(string localisationString)
    {
        var localisationExists = Enum.TryParse<Localisation>(localisationString.ToUpper(), out var localisationEnum);
        if (!localisationExists) throw new ArgumentException("Requested localisation does not exist");

        return localisationEnum;
    }


    [HttpGet("tags")]
    public ActionResult<IEnumerable<TagDTO>> GetTags([FromQuery] [RegularExpression(@"^(?i)(se|en)$")] string localisation)
    {
        return Tag.ExampleDTOs(GetLocalisation(localisation));
    }
        
    [HttpGet("tags/{id}")]
    public ActionResult<TagDTO> GetTag(string localisation, Guid id)
    {
        var tag = Tag.ExampleDTOs(GetLocalisation(localisation)).FirstOrDefault(tag => tag.Id == id);
        if (tag is null) return NotFound();

        return tag;
    }
}
