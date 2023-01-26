using Badger;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var badge = GetFromServer.ParseBadgeData("b2107Xs19116s28092");
            var badgeRendered = badge.Render();

            if (badgeRendered != null)
            {
                File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
            }

            Console.WriteLine("Done!");
        }
    }
}