# Badger
 Habbo badge imager library.
 
## How to Use

```
            var badge = GetFromServer.ParseBadgeData("b0502Xs13181s01014");
            var badgeRendered = badge.Render();

            if (badgeRendered != null)
            {
                File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
            }

            var badge2 = GetFromServer.ParseBadgeData("b10074s170011s139196s29168");
            File.WriteAllBytes("badge_flash_2013.png", badge2.Render());
```
