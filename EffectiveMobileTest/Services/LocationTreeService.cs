using EffectiveMobileTest.Models;

namespace EffectiveMobileTest.Services
{
    public class LocationTreeService
    {
        public LocationTree LocationTree { get; private set; } = new();

        public void Replace(LocationTree locationTree)
        {
            LocationTree = locationTree;
        }
    }
}