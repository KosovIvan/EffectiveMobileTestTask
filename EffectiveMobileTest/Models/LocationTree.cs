namespace EffectiveMobileTest.Models
{
    public class LocationTree
    {
        private readonly LocationNode _root = new();

        public void Add(string location, string platform)
        {
            var parts = location.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            var node = _root;

            foreach (var part in parts)
            {
                if (!node!.Children.TryGetValue(part, out var child))
                {
                    child = new LocationNode();
                    node.Children[part] = child;
                }
                node = child;
            }
            node.Platforms.Add(platform);
        }

        public List<string> GetPlatforms(string location)
        {
            var parts = location.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries);
            var node = _root;
            HashSet<string> result = new();

            foreach (var part in parts)
            {
                if (!node!.Children.TryGetValue(part, out var child)) break;
                result.UnionWith(child.Platforms);
                node = child;
            }
            return result.ToList();
        }
    }
}