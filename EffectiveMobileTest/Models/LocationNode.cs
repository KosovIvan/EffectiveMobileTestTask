namespace EffectiveMobileTest.Models
{
    public class LocationNode
    {
        public Dictionary<string, LocationNode> Children { get; } = new();
        public HashSet<string> Platforms { get; } = new();
    }
}