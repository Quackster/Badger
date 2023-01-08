using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Badger
{
    public class GetFromServer
    {
        public static Badge ParseBadgeData(string badgeCode)
        {
            var badge = new Badge(); 

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
                    partPosition = int.Parse(partCode.Substring(5));
                } else
                {
                    partId = int.Parse(partCode.Substring(1, 3));
                    partColor = int.Parse(partCode.Substring(4, 2));
                    partPosition = int.Parse(partCode.Substring(6));
                }

                badge.Parts.Add(new BadgePart(
                    partType == 's' ? BadgePartType.SHAPE : BadgePartType.BASE,
                    partId, partColor, partPosition));
            }

            return badge;
        }
    }
}

/*using System.Security.Cryptography;
using System.Xml.Linq;

namespace Badger
{
    public class GetFromServer
    {

        public static Badge getData(string aData)
        {
            var badge = new Badge(); 

            var _loc1_ = 0;
            string _loc3_ = null;
            string _loc9_ = null;
            string _loc10_ = null;
            var _loc12_ = false;
            var _loc7_ = 4;
            int _loc2_ = 0;
            int _loc8_ = 0;
            int _loc5_ = 0;

            while (_loc1_ < 5)
            {
                var badgePart = new BadgePart();

                _loc1_ = _loc1_ + 1;
            }

            return badge;
        }
    }
}*/