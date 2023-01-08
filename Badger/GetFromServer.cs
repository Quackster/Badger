using System.Security.Cryptography;
using System.Xml.Linq;

namespace Badger
{
    public class GetFromServer
    {
        public static Badge ParseBadgeData(string k)
        {
            var _loc1_ = 0;
            var _loc2_ = 0;
            BadgePart _loc4_ = null;
            var _loc5_ = string.Empty;
            var _loc6_ = new Badge();
            var _loc7_ = 0;
            var _loc8_ = 0;
            BadgePartType _loc9_ = BadgePartType.BASE;
            var _loc10_ = 0;

            while (k.Length - _loc2_ >= 6)
            {
                _loc5_ = k.Substring(_loc2_, 6);

                _loc7_ = int.Parse(_loc5_.Substring(1, 2));
                _loc8_ = int.Parse(_loc5_.Substring(3, 2));

                if (_loc5_.EndsWith("X"))
                {
                    _loc9_ = BadgePartType.BASE;
                    _loc10_ = 5;
                }
                else
                {
                    _loc9_ = BadgePartType.SHAPE;
                    _loc10_ = int.Parse(_loc5_.Substring(5));
                }

                _loc4_ = new BadgePart(_loc7_, _loc8_, _loc10_, _loc9_);
                _loc6_.Parts.Add(_loc1_, _loc4_);

                _loc2_= _loc2_ + 6;
                _loc1_++;
            }

            return _loc6_;
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