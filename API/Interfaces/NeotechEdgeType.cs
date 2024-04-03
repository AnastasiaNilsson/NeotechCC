
using Microsoft.OpenApi.Any;

namespace API;

// Interface for all core Neotech Edge roleplaying game data types
public interface INeotechEdge 
{
    public static abstract INeotechEdge CreateFromGoogleData(Dictionary<string, string> data);
}
