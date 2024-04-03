
namespace API;

public class Tag (int id, TagCategory category, string englishName, string swedishName, string englishDescription, string swedishDescription) : INeotechEdge
{
    public int Id { get; } = id;
    public TagCategory Category { get; } = category;
    public string EnglishName { get; } = englishName;
    public string SwedishhName { get; } = swedishName;
    public string EnglishDescription { get; } = englishDescription;
    public string SwedishDescription { get; } = swedishDescription;
    
    public static INeotechEdge CreateFromGoogleData(Dictionary<string, string> data)
    {
        var isValidCategory = Enum.TryParse<TagCategory>(data["Category"], out var category);
        if (!isValidCategory) throw new ArgumentException($"Invalid tag category '{data["Category"]}' found. Unable to create tag with id {data["UUID"]}");

        var isInteger = int.TryParse(data["UUID"], out var id);
        if (!isInteger) throw new ArgumentException($"Invalid tag UUID '{data["UUID"]}' found. Value is not an integer.");

        var englishName = data["English Name"];
        var swedishName = data["Swedish Name"];
        var englishDescription = data["English Description"];
        var swedishDescription = data["Swedish Description"];
        
        return new Tag(id, category, englishName, swedishName, englishDescription, swedishDescription);
    }

    public TagDTO ToDTO(Localisation localisation)
    {
        var inEnglish = localisation == Localisation.EN;
        var localisedName = inEnglish ? EnglishName : SwedishhName;
        var localisedDescriotion = inEnglish ? EnglishDescription : SwedishDescription;
        var localisedCategory = inEnglish ? Category.ToLocalisedString(Localisation.EN) : Category.ToLocalisedString(Localisation.SE);

        return new TagDTO(localisedName, localisedCategory, localisedDescriotion, id);
    }

    public static List<Tag> ExampleTags { get; } = new List<Tag>()
    {
        new Tag(id: 1, englishName: "#4Kidz", swedishName: "#4Kidz", englishDescription: "The eqip is colourful", swedishDescription: "Eqipen är färgglad", category: TagCategory.General),
        new Tag(id: 2, englishName: "#Employable+x", swedishName: "#Anställningsbar+x", englishDescription: "You are extremely employable for some bomba reason.", swedishDescription: "Du är extremt anställningsbar av någon bomba anledning.", category: TagCategory.General),
        new Tag(id: 3, englishName: "#Load+x", swedishName: "#Last+x", englishDescription: "The auton can carry x kgs for you.", swedishDescription: "Autonen kan bära x kg åt dig", category: TagCategory.Auton)
    };
    public static List<TagDTO> ExampleDTOs(Localisation localisation) => ExampleTags.Select(tag => tag.ToDTO(localisation)).ToList();
}
