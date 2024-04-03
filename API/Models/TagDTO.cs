
namespace API;

public class TagDTO (string name, string category, string description, int id)
{    
    public string Name { get; } = name;
    public string Category { get; } = category;
    public string Description { get; } = description;
    public int Id { get; } = id;
}
