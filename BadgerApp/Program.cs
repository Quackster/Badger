using Badger;

namespace BadgerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var badge = GetFromServer.ParseBadgeData("b0502Xs13181s01014");
            File.WriteAllBytes("badge_shockwave.gif", badge.Render());

            var badge2 = GetFromServer.ParseBadgeData("b10074s170011s139196s29168");
            File.WriteAllBytes("badge_flash_2013.png", badge2.Render());
        }
    }
}