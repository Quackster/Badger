using Avatara.Extensions;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
