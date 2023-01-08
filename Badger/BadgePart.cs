using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Badger
{
    public class BadgePart
    {
        public int GraphicResource { get; set; }
        public int ColorResource { get; set; }

        public string Symbol1 { get; set; }
        public string Symbol2 { get; set; }
        public string Color { get; set; }

        public int Location { get; set; }
        public BadgePartType Type { get; set; }
        public List<BadgeResource> Resources { get; set; }

        public BadgePart(BadgePartType type, int graphic, int color, int location)
        {
            Type = type;
            GraphicResource = graphic;
            ColorResource = color;
            Location = location;
            Resources = BadgeResourceManager.BadgeResources;//.Where(x => x.Id == graphic).ToList();

            if (Resources != null && Resources.Count > 0)
            {
                switch (type)
                {
                    case BadgePartType.SHAPE:
                        Symbol1 = Resources.Where(x => x.Id == graphic).FirstOrDefault(x => x.Type == "Symbol")?.ExtraData1;
                        Symbol2 = Resources.Where(x => x.Id == graphic).FirstOrDefault(x => x.Type == "Symbol")?.ExtraData2;
                        Color = Resources.Where(x => x.Id == color).FirstOrDefault(x => x.Type == "Color1")?.ExtraData1;
                        break;
                    case BadgePartType.BASE:
                        Symbol1 = Resources.Where(x => x.Id == graphic).FirstOrDefault(x => x.Type == "Base")?.ExtraData1;
                        Symbol2 = Resources.Where(x => x.Id == graphic).FirstOrDefault(x => x.Type == "Base")?.ExtraData2;
                        Color = Resources.Where(x => x.Id == color).FirstOrDefault(x => x.Type == "Color1")?.ExtraData1;
                        break;
                }
            }
        }

        public Point GetPosition(Image<Rgba32> canvas, Image<Rgba32> template)
        {

            int x = 0;
            int y = 0;

            var templateBounds = template.Bounds();
            var canvasBounds = canvas.Bounds();

            switch (this.Location)
            {
                case 1:
                    x = (canvasBounds.Width - templateBounds.Width) / 2;
                    break;
                case 2:
                    x = canvasBounds.Width - templateBounds.Width;
                    break;
                case 3:
                    y = (canvasBounds.Height / 2) - (templateBounds.Height / 2);
                    break;
                case 4:
                    x = (canvasBounds.Width - templateBounds.Width) / 2;
                    y = (canvasBounds.Height / 2) - (templateBounds.Height / 2);
                    break;
                case 5:
                    x = canvasBounds.Width - templateBounds.Width;
                    y = (canvasBounds.Height / 2) - (templateBounds.Height / 2);
                    break;
                case 6:
                    y = canvasBounds.Height - templateBounds.Width;
                    break;
                case 7:
                    x = ((canvasBounds.Width - templateBounds.Height) / 2);
                    y = canvasBounds.Height - templateBounds.Height;
                    break;
                case 8:
                    x = canvasBounds.Width - templateBounds.Height;
                    y = canvasBounds.Height - templateBounds.Height;
                    break;
                default:
                    x = 0;
                    y = 0;
                    break;
            }


            return new Point(x, y);
        }
    }
}
