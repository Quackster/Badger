using Badger;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var badge = GetFromServer.ParseBadgeData("b0502Xs13181s01014");
            var badgeRendered = badge.Render();

            if (badgeRendered != null)
            {
                File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
            }

            Console.WriteLine("Done!");
        }
    }
}