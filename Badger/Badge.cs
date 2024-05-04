using Badger.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Quantization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = SixLabors.ImageSharp.Image;

namespace Badger
{
    public class Badge
    {
        public const int CANVAS_HEIGHT = 39;
        public const int CANVAS_WIDTH = 39;

        public string[] Colors = new string[]
        {
            "ffd601",
            "ec7600",
            "84de00",
            "589a00",
            "50c1fb",
            "006fcf",
            "ff98e3",
            "f334bf",
            "ff2d2d",
            "af0a0a",
            "ffffff",
            "c0c0c0",
            "373737",
            "fbe7ac",
            "977641",
            "c2eaff",
            "fff165",
            "aaff7d"
        };

        public int[] BaseTemplateProxies = new int[]
        {
        10,
        11,
        12,
        17,
        18,
        19,
        21,
        23
        };

        public int[] TemplateProxies = new int[]
{
            3,
            4,
            5,
            7,
            13,
            14,
            17,
            18,
            21,
            23,
            28,
            29,
            30,
            31,
            34,
            35,
            40,
            41,
            42,
            50,
            51,
            52,
            53,
            55,
            61,
            67
        };

        private List<BadgePart> _badgeParts;
        private bool _forceWhiteBackground;
        private BadgeSettings _badgeSettings;

        public List<BadgePart> Parts { get { return _badgeParts; } }
        public BadgeSettings Settings => _badgeSettings;

        public Badge(BadgeSettings settings)
        {
            this._badgeSettings = settings;
            this._badgeParts = new List<BadgePart>();
        }

        public static Badge ParseBadgeData(BadgeSettings settings, string badgeCode)
        {
            var badge = new Badge(settings);

            MatchCollection partMatches = Regex.Matches(badgeCode, @"[b|s][0-9]{4,6}");

            foreach (Match partMatch in partMatches)
            {
                string partCode = partMatch.Value;
                bool shortMethod = partCode.Length <= 6;

                char partType = partCode[0];
                int partId = 0; // int.Parse(partCode.Substring(1, 2));
                int partColor = 0;// int.Parse(partCode.Substring(3, 2));
                int partPosition = 0;// int.Parse(partCode.Substring(5));

                if (shortMethod)
                {
                    partId = int.Parse(partCode.Substring(1, 2));
                    partColor = int.Parse(partCode.Substring(3, 2));
                    partPosition = int.Parse(partCode.Length > 5 ? partCode.Substring(5) : "-1");
                }
                else
                {
                    partId = int.Parse(partCode.Substring(1, 3));
                    partColor = int.Parse(partCode.Substring(4, 2));
                    partPosition = int.Parse(partCode.Substring(6));
                }

                badge.Parts.Add(new BadgePart(
                    badge,
                    partType == 's' ? BadgePartType.SHAPE : BadgePartType.BASE,
                    partId, partColor, partPosition));
            }

            return badge;
        }

        public byte[]? Render(bool gifEncoder = true, bool forceWhiteBackground = false)
        {
            this._forceWhiteBackground = forceWhiteBackground;

            try
            {
                using (var canvas = new Image<Rgba32>(
                    CANVAS_WIDTH,
                    CANVAS_HEIGHT,
                    SixLabors.ImageSharp.Color.Transparent))
                {

                    foreach (var part in Parts)
                    {
                        if (this.Settings.IsShockwaveBadge)

                        {
                            canvas.Mutate(x =>
                            {
                                using (var template = this.GetShockwaveTemplate(part.Type, templateId: part.GraphicResource, proxy: false))
                                {
                                    TintImage(template, this.Colors[part.ColorResource - 1], 255);
                                    x.DrawImage(template, part.GetPosition(canvas, template), 1.0F);
                                }

                                if (this.IsTemplateProxied(part.Type, part.GraphicResource))
                                {
                                    using (var template = this.GetShockwaveTemplate(part.Type, templateId: part.GraphicResource, proxy: true))
                                    {
                                        x.DrawImage(template, part.GetPosition(canvas, template), 1.0F);
                                    }
                                }
                            });
                        }
                        else
                        {

                            if (part.Symbol1 != null)
                            {
                                using (var template = this.GetTemplate(part.Symbol1))
                                {
                                    canvas.Mutate(x =>
                                    {
                                        if (part.Color != null)
                                        {
                                            TintImage(template, part.Color, 255);
                                        }

                                        x.DrawImage(template, part.GetPosition(canvas, template), 1.0F);

                                    });
                                }
                            }

                            if (part.Symbol2 != null)
                            {
                                using (var template = this.GetTemplate(part.Symbol2))
                                {
                                    canvas.Mutate(x =>
                                    {
                                        x.DrawImage(template, part.GetPosition(canvas, template), 1.0F);

                                    });
                                }
                            }
                        }

                        //var position = part.GetPosition(canvas);
                        //canvas[position.X, position.Y] = SixLabors.ImageSharp.Color.Red.ToPixel<Rgba32>();
                    }

                    using (var ms = new MemoryStream())
                    {
                        if (_forceWhiteBackground)
                        {
                            FixTransparency(canvas);
                        }

                        canvas.Save(ms, gifEncoder ?
                            new SixLabors.ImageSharp.Formats.Gif.GifEncoder
                            {
                                ColorTableMode = SixLabors.ImageSharp.Formats.Gif.GifColorTableMode.Local,
                                Quantizer = new OctreeQuantizer(new QuantizerOptions
                                {
                                    Dither = null
                                })
                            } :
                            new SixLabors.ImageSharp.Formats.Png.PngEncoder());
                        return ms.ToArray();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public bool IsTemplateProxied(BadgePartType type, int templateId)
        {
            if (type == BadgePartType.BASE)
                return BaseTemplateProxies.Any(x => x == templateId);

            return TemplateProxies.Any(x => x == templateId);
        }

        public Image<Rgba32> GetTemplate(string symbol)
        {
            if (string.IsNullOrEmpty(Settings.BasePath))
            {
                return Image.Load<Rgba32>(Path.Combine("badges", "badgeparts", symbol));
            }

            return Image.Load<Rgba32>(Path.Combine(Settings.BasePath, "badges", "badgeparts", symbol));
        }

        public Image<Rgba32> GetShockwaveTemplate(BadgePartType type, int templateId, bool proxy = false)
        {
            var fileGraphic = templateId < 10 ? "0" + templateId : templateId.ToString();

            if (proxy)
            {
                fileGraphic = fileGraphic + "_" + fileGraphic;
            }

            if (string.IsNullOrEmpty(Settings.BasePath))
            {
                return Image.Load<Rgba32>(Path.Combine("badges", "shockwave",
                (type == BadgePartType.BASE ? "base" : "templates"),
                (templateId == 0 ? "base" : $"{fileGraphic}") + ".gif"));
            } else
            {
                return Image.Load<Rgba32>(Path.Combine(Settings.BasePath, "badges", "shockwave",
                (type == BadgePartType.BASE ? "base" : "templates"),
                (templateId == 0 ? "base" : $"{fileGraphic}") + ".gif"));
            }
        }

        private void FixTransparency(Image<Rgba32> image)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var current = image[x, y];

                    // Turn all white pixels into 'fake' white to not let Shockwave make it transparent
                    if (current == Color.White.ToPixel<Rgba32>())
                    {
                        current.R = 254;
                        current.G = 254;
                        current.B = 254;
                        current.A = 255;
                        image[x, y] = current;
                        continue;
                    }
   
                    // Turna all transparent pixels into white pixels so the Shockwave client will turn these transparent... it's weird, I know
                    if (current == Color.Transparent.ToPixel<Rgba32>())
                    {
                        current.R = 255;
                        current.G = 255;
                        current.B = 255;
                        current.A = 255;
                        image[x, y] = current;
                        continue;
                    }
                }
            }
        }

        private void TintImage(Image<Rgba32> image, string colourCode, byte alpha)
        {
            var rgb = ColorExtensions.FromHex(colourCode).ToPixel<Rgba32>();

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var current = image[x, y];

                    if (current.A > 0)
                    {
                        current.R = (byte)(rgb.R * current.R / 255);
                        current.G = (byte)(rgb.G * current.G / 255);
                        current.B = (byte)(rgb.B * current.B / 255);
                        current.A = alpha;
                    }

                    image[x, y] = current;
                }
            }
        }
    }
}
