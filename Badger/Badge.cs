﻿using Avatara.Extensions;
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

        private Dictionary<int, BadgePart> _badgeParts;

        public Dictionary<int, BadgePart> Parts { get { return _badgeParts; } }

        public Badge()
        {
            _badgeParts= new Dictionary<int, BadgePart>();
        }

        public void Render()
        {
            using (var canvas = new Image<Rgba32>(
                CANVAS_WIDTH, 
                CANVAS_HEIGHT, 
                SixLabors.ImageSharp.Color.Transparent))
            {

                foreach (var part in Parts.Values)
                {
                    canvas.Mutate(x =>
                    {

                        using (var template = this.GetTemplate(part.Type, part.Graphic, proxy: false))
                        {
                            TintImage(template, this.Colors[part.Color - 1], 255);
                            x.DrawImage(template, part.GetPosition(canvas, template), 1.0F);
                        }

                        if (this.IsTemplateProxied(part.Type, part.Graphic))
                        {
                            using (var template = this.GetTemplate(part.Type, part.Graphic, proxy: true))
                            {
                                x.DrawImage(template, part.GetPosition(canvas, template), 1.0F);
                            }
                        }
                    });

                    //var position = part.GetPosition(canvas);
                    //canvas[position.X, position.Y] = SixLabors.ImageSharp.Color.Red.ToPixel<Rgba32>();
                }

                canvas.SaveAsPng("badge.gif");
            }
        }

        public bool IsTemplateProxied(BadgePartType type, int templateId)
        {
            if (type == BadgePartType.BASE)
                return false;

            return TemplateProxies.Any(x => x == templateId);
        }

        public Image<Rgba32> GetTemplate(BadgePartType type, int templateId, bool proxy = false)
        {
            var fileGraphic = templateId < 10 ? "0" + templateId : templateId.ToString();

            if (proxy)
            {
                fileGraphic = fileGraphic + "_" + fileGraphic;
            }

            return Image.Load<Rgba32>(Path.Combine("badges",
                (type == BadgePartType.BASE ? "base" : "templates"),
                (templateId == 0 ? "base" : $"{fileGraphic}") + ".gif"));
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