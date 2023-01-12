# Badger
 Habbo badge imager library.
 
## How to Use

Extract badges.zip in the folder that the app is running.

# Shockwave badges - v18 - v31+

Will automatically export as .gif

```c
var badge = GetFromServer.ParseBadgeData("b0502Xs13181s01014");
var badgeRendered = badge.Render();

if (badgeRendered != null)
{
    File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
}
```

# Guild badge support

Will automatically export as .png

```c
var badge = GetFromServer.ParseBadgeData("b10074s170011s139196s29168");
File.WriteAllBytes("badge_flash.png", badge.Render());
```

## As a web server?

See: https://github.com/Quackster/Minerva
