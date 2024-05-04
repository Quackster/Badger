# Badger
 Habbo badge imager library.
 
### How to Use

Extract badges.zip in the folder that the app is running.

**Shockwave badges - v18 - v31+**

Will automatically export as .gif

```c
var badge = GetFromServer.ParseBadgeData(new BadgeSettings
{
    IsShockwaveBadge = false
}, "b0502Xs13181s01014");

var badgeRendered = badge.Render();

if (badgeRendered != null)
{
    File.WriteAllBytes("badge_shockwave.gif", badgeRendered);
}
```

**Guild badge support**

Will automatically export as .png

You will need to re-export badge.resource.json from the database with the columns matching if you wish to support the specific emulator being used, as some of these badge resource guild data structures are different depending on the emulator.

```c
var badge = GetFromServer.ParseBadgeData(new BadgeSettings
{
    IsShockwaveBadge = true
}, "b10074s170011s139196s29168");
File.WriteAllBytes("badge_flash.png", badge.Render());
```

### Available on NuGet

NuGet link: https://www.nuget.org/packages/Badger-Imager/1.0.0

Package: 

```
NuGet\Install-Package Badger-Imager -Version 1.0.0
```


### As a web server?

See: https://github.com/Quackster/Minerva
