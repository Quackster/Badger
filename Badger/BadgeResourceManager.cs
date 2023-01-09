using Newtonsoft.Json;

namespace Badger
{
    public static class BadgeResourceManager
    {
        public static List<BadgeResource> BadgeResources = new List<BadgeResource>();

        public static void Load()
        {
            if (BadgeResources.Count > 0)
                return;

            BadgeResources = new List<BadgeResource>();

            var contents = File.ReadAllText("badge_resource.json");

            if (contents != null) { 
                List<BadgeResource>? resourceList = JsonConvert.DeserializeObject<List<BadgeResource>>(contents);

                if (resourceList != null)
                    BadgeResources = resourceList;
            }

        }
    }
}
