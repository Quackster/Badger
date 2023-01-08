using Avatara.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badger
{
    public struct BadgeResource
    {
        public int Id;
        public string? ExtraData1;
        public string? ExtraData2;
        public string Type;
    }
}
