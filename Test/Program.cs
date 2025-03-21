using Badger;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DoFlashGuildBadge();
            DoShockwaveBadge();

            Console.WriteLine("Done!");
        }

        private static void DoFlashGuildBadge()
        {
            var badge = Badge.ParseBadgeData(new BadgeSettings
            {
                IsShockwaveBadge = false
            }, "b00534Xs317315s321324s317313");

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
            }, "s37075s21144s21145s06110");

            var badgeRendered = badge.Render(forceWhiteBackground: true);

            if (badgeRendered != null)
            {
                File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
            }
        }
    }
}