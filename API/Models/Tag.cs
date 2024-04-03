
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
}
