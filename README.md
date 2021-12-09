# PowOut - A Highly Configurable EXiLED 3.0+ Plugin for SCP Secret Laboratory
------------------------------------------------------------------------------
### Note: I have not submitted this plugin for official release as of yet, so its only available here. Doubt anyone I don't know will find it, but still. No taking credit. Also, it is EXTREMELY out of date, but hopefully I will fix that soon.
-------------------------------------------------------------------------------
### Current Version- 0.7 PreRelease

## Features
PowOut has three primary features right now, two pertaining to blackouts and one as an experimental (mostly unrelated) QoL feature.

### Rolling Blackouts
The **primary feature** is a configurable Blackout event that can occur at random or set intervals, can have varying severity, and can affect varying levels of the complex. Blackouts can entail full system failures (all lights out), simpler brown-outs, or only partial system failures. The event has a configurable CASSIE announcement, duration configuration including random ranges between a set min and max, frequency configurations including random between a min and max, configuration for what zones are affected, and a "Room Blacklist" that excludes any rooms included in it.

In a future version, Blackout Events will also have a configurable "sub-event" called Brown-Out Events that simply *dim* the lights.

The **secondary feature** is a brand-new addition in the 0.7 prerelease update just pushed here yesterday. Blackout Events now affect the complex security systems, meaning *all doors in the compound* are subject to failure during a blackout. Simply put, doors can be opened automatically during Blackout Events now. This feature is highly configurable as well, including a door blacklist, "advanced glitch" event that I haven't bothered to code yet, and code that inspired me to include a 3rd mini-feature. This feature is VERY new and only my first iteration of door control, so plenty is subject to change. Feel free to report any issues you encounter in the Issues tab.

The **third feature** is an experimental SCP Unstuck system. This system has a LOT of little issues that can break it, but under normal non-testing scenarios, it works just fine. I am currently working on a more dynamic and powerful version of this feature, possibly to make into an entirely separate plugin, but this more basic version functions for now. 

This Unstucker works ONLY for SCP players. If an SCP player finds themselves jebaited into a room, now locked inside, they can free themselves by trying to open the door. The Unstucker will notice they're locked and free them after a configurable wait time. 
*NOTE: At the moment, the Unstucker will only notice you're stuck **after you try to open the door**, so make sure to tell your SCP players.*

## Config
```css
pow_out:
  # Is 'PowOut' plugin enabled?
  is_enabled: true
  # CASSIE plays warning on PowOut events? [Default: True]
  pow_out_cassie: true
  # CASSIE voice line to play when a blackout occurs.
  pow_out_msg: Error .g1 .g2 .g4 malfunction
  # Should Blackout events occur? [Default: True] NOTE: If both blackouts AND brownouts disabled, defaults to Blackout events.
  pow_out_do_blackouts: true
  # Should Brownout events occur? Picks one at random if both enabled. [Default: True]
  pow_out_do_brownouts: true
  # Percent chance for a brownout instead of a blackout. [Default: 40, Max 100]
  pow_out_brownout_ratio: 40
  # Minimum PowOut duration in seconds. [Default: 150]
  pow_out_min_rng: 150
  # Max PowOut duration in seconds. [Default: 180]
  pow_out_max_rng: 180
  # Min cooldown of PowOut events during match, in seconds. [Default: 360]
  pow_out_freq_min_rng: 360
  # Max cooldown of PowOut events during match, in seconds. [Default: 600]
  pow_out_freq_max_rng: 600
  # Should a power outage open doors? [Default: True]
  pow_out_door_control: true
  # Doors to EXCLUDE from the plugin's door controls. Misspelled names are excluded automatically.
  pow_out_door_blacklist:
  - CHECKPOINT_EZ_HCZ
  - CHECKPOINT_LCZ_A
  - CHECKPOINT_LCZ_B
  - ESCAPE_PRIMARY
  - ESCAPE_SECONDARY
  - GATE_A
  - GATE_B
  - SURFACE_NUKE
  # Should the sever cause power outages to free stuck SCP players? [Default: True]
  pow_out_unstuck: true
  # Message to broadcast to SCP player being let out. Leave blank if no broadcast is desired.
  pow_out_unstuck_msg: SCP-079 has let you out.
  # Time SCP player will see the unstuck message. [Default: 8]
  pow_out_unstuck_msg_time: 8
```
## Config Documentation

```
  # CASSIE plays warning on PowOut events? [Default: True]
  pow_out_cassie: true
  # CASSIE voice line to play when a blackout occurs.
  pow_out_msg: Error .g1 .g2 .g4 malfunction
```
If enabled, the CASSIE announcer heard at the start of the match will announce the message you write in pow_out_msg. Note that CASSIE has a limited vocabulary and can't speak full English. Take a look at this link for CASSIE's vocabulary- https://steamcommunity.com/sharedfiles/filedetails/?id=1577299753

```
  # Should Blackout events occur? [Default: True] NOTE: If both blackouts AND brownouts disabled, defaults to Blackout events.
  pow_out_do_blackouts: true
```
If pow_out_do_blackouts is **false**, then the plugin does not call any Blackout Events. Essentially disables the plugin, minus the stuck player detector.

```
  # Should Brownout events occur? Picks one at random if both enabled. [Default: True]
  pow_out_do_brownouts: true
  # Percent chance for a brownout instead of a blackout. [Default: 40, Max 100]
  pow_out_brownout_ratio: 40
```
Currently does nothing but will *eventually* allow the Blackout subroutine to pick between Brownouts and Blackouts when calling a power event. Brownouts are coming in the next update. Most likely.
```
  # Minimum PowOut duration in seconds. [Default: 150]
  pow_out_min_rng: 150
  # Max PowOut duration in seconds. [Default: 180]
  pow_out_max_rng: 180
```
These values set **how long** a power outage lasts in seconds. The plugin will take the *pow_out_min_rng* as the lowest possible duration and the *pow_out_max_rng* as the longest possible duration, picking a random time between them. For gameplay balance purposes, I wouldn't let power outages last any longer than 3 minutes (180 seconds), but that's up to you.
```
  # Min cooldown of PowOut events during match, in seconds. [Default: 360]
  pow_out_freq_min_rng: 360
  # Max cooldown of PowOut events during match, in seconds. [Default: 600]
  pow_out_freq_max_rng: 600
```
These values set **how often** a power outage event happens. Similar to above, it picks a random value between your defined minimum and maximum. Default is 6-10 minutes in between events.
```
  # Should a power outage open doors? [Default: True]
  pow_out_door_control: true
```
If set to true, enables the door control subroutine for the Blackout Events. If enabled, doors will be opened and locked open during blackouts, excluding doors in rooms added to the blacklist.
```
# Doors to EXCLUDE from the plugin's door controls. Misspelled names are excluded automatically.
  pow_out_door_blacklist:
  - CHECKPOINT_EZ_HCZ
  - CHECKPOINT_LCZ_A
  - CHECKPOINT_LCZ_B
  - ESCAPE_PRIMARY
  - ESCAPE_SECONDARY
  - GATE_A
  - GATE_B
  - SURFACE_NUKE
```
If any room name is added to this list, the doors within it will not be affected by DoorControl. By default, I added the checkpoints, gates, and nuke so that a power outage won't be the last hurdle for that single remaining D-Class who managed to speedrun through all the carnage, almost at the bunker. Names valid for this list are listed in the #resources chat in the EXiLED Discord server, but here is a direct link: https://discord.com/channels/656673194693885975/668962626780397569/765647220963409932

Any names NOT in this list just will not work, so make sure you know how to spell. Known issue is that Checkpoint doors still open, even though the blacklist still lets you use the keycard panel normally (as it's supposed to).
```
  # Should the sever cause power outages to free stuck SCP players? [Default: True]
  pow_out_unstuck: true
  # Message to broadcast to SCP player being let out. Leave blank if no broadcast is desired.
  pow_out_unstuck_msg: SCP-079 has let you out.
  # Time SCP player will see the unstuck message. [Default: 8]
  pow_out_unstuck_msg_time: 8
```
These last 3 lines all pertain to the Unstucker. At the moment, I disabled the ability to customize the stuck time, but I will add it back once I fix a few problems.
If *pow_out_unstuck* is set to true, the Unstucker is turned on. *pow_out_unstuck_msg* is the message sent to the player. The message utilizes the Hint system, so only the affected player will see it, and there is no vocabulary limit like with CASSIE. Duration is defined in *pow_out_unstuck_msg_time*. Please keep it to under 20 seconds as any longer delays the amount of time the Unstucker takes to detect stuck SCP players.
