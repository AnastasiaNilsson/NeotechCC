using System.Linq;
using System.Collections.Generic;

namespace API;

public class Tag
{
    private readonly string _englishName;
    private readonly string _swedishhName;
    private readonly string _englishDescription;
    private readonly string _swedisgDescription;
    
    public Guid Id { get; } = Guid.NewGuid();
    public TagCategory Category { get; }

    public Tag(string enName, string seName, string enDesc, string seDesc, TagCategory category)
    {
        _englishName = enName;
        _swedishhName = seName;
        _englishDescription = enDesc;
        _swedisgDescription = seDesc;
        Category = category;
    }

    public TagDTO ToDTO(Localisation localisation)
    {
        var inEnglish = localisation == Localisation.EN;
        var localisedName = inEnglish ? _englishName : _swedishhName;
        var localisedDescriotion = inEnglish ? _englishDescription : _swedisgDescription;
        var localisedCategory = inEnglish ? Category.ToLocalisedString(Localisation.EN) : Category.ToLocalisedString(Localisation.SE);

        return new TagDTO() 
        {
            Name = localisedName, Description = localisedDescriotion, Category = localisedCategory, Id = Id
        };
    }

    public static List<Tag> ExampleTags { get; } = new List<Tag>()
    {
        new Tag(enName: "#4Kidz", seName: "#4Kidz", enDesc: "The eqip is colourful", seDesc: "Eqipen är färgglad", category: TagCategory.General),
        new Tag(enName: "#Employable+x", seName: "#Anställningsbar+x", enDesc: "You are extremely employable for some bomba reason.", seDesc: "Du är extremt anställningsbar av någon bomba anledning.", category: TagCategory.General),
        new Tag(enName: "#Load+x", seName: "#Last+x", enDesc: "The auton can carry x kgs for you.", seDesc: "Autonen kan bära x kg åt dig", category: TagCategory.Auton)
    };
    public static List<TagDTO> ExampleDTOs(Localisation localisation) => ExampleTags.Select(tag => tag.ToDTO(localisation)).ToList();
}
