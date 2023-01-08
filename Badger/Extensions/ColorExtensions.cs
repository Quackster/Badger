using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Avatara.Extensions
{
    public static class ColorExtensions
    {
        public static Color FromHex(string hexColourCode)
        {
            return Color.FromRgb(
                byte.Parse(hexColourCode.Substring(0, 2), NumberStyles.HexNumber),
                byte.Parse(hexColourCode.Substring(2, 2), NumberStyles.HexNumber),
                byte.Parse(hexColourCode.Substring(4, 2), NumberStyles.HexNumber));
        }
    }
}
