using Badger;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DoFlashGuildBadge();
            DoShockwaveBadge();

            Console.WriteLine("Done!");
        }

        private static void DoFlashGuildBadge()
        {
            var badge = Badge.ParseBadgeData(new BadgeSettings
            {
                IsShockwaveBadge = false
            }, "b2107Xs19116s28092");

            var badgeRendered = badge.Render();

            if (badgeRendered != null)
            {
                File.WriteAllBytes("badge_flash_guild.gif", badgeRendered);
            }
        }

        private static void DoShockwaveBadge()
        {
            var badge = Badge.ParseBadgeData(new BadgeSettings
            {
                IsShockwaveBadge = true
            }, "b0503Xs09114s05013s05015");

            var badgeRendered = badge.Render();

            if (badgeRendered != null)
            {
                File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
            }
        }
    }
}