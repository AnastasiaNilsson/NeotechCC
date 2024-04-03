
using Microsoft.OpenApi.Any;

namespace API;

// Interface for all core Neotech Edge roleplaying game data types
public interface NeotechEdgeType 
{
    public static abstract NeotechEdgeType CreateFromGoogleData(Dictionary<string, string> data);
}
