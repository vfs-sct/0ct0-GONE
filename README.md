0ct0-GONE BETA
---------------------------------------------------

This is the Beta submission for 0CT0-GONE.

Josh Paquette			Jesse Rougeau
Evan Landry			Kristin Ruff-Frederickson
Andrew Icardi			Roger Crusafon-Pont

CONTROLS
===================
W/S - Move forward/back
A/D - Move left/right
Q/E - Rotate left/right
Space/Ctrl - Move up/down
Mouse - Look around
Tab - Scan nearby materials, highlighting them in their associated color
1 - Scavenge tool, harvest targeted object
2 - Repair Tool (uses Nanites)
3 - Grab Tool
4 - Satellite Placer
Left Click - Use equipped tool
Scroll Wheel - Throttle acceleration
V - Open Inventory
C - Open crafting menu when near space station
F - Refuel Octo when near space station
Esc - Pause/Unpause/Close Menu/Skip Cutscene
F1 - Debug menu
====================

INSTRUCTIONS
===================
The game is playable from start to finish. Use ESC to skip cutscenes.
===================

BUGS/WARNINGS
===================

===================


In this build:

GAMEPLAY
===================
Beta
---------------------------------------------------------------------------------------------
-Major performance enhancement by shifting storm to use object pooling
-Refactored and bugfixed highlighting
-Bug fixes related to storms
-Bug fixes related to ship health
-Bug fixes related to placeable satellites


Old
---------------------------------------------------------------------------------------------
-New asteroid weather system
	-Debris for salvaging is generated and flies through the playing field. Static debris fields are deprecated.
	-Storms trigger periodically, causing the player to need to dodge and look for cover to avoid taking damage.
	-Storms ramp up over time, creating a time constraint for the player to complete the game.
-Health deprecated. Octo's fuel (energy) drains both by using his thrusters and by taking physical damage.
-Refueling Satellite rework
	-Refuel satellite is now used to produce Goo Glue over time instead of fuel.
-Station repair
	-The main station now takes damage which can be repaired with Goo Glue
	-If the station hits 0 health, the game is over
-New shield/asteroid barrier
	-Large objects in the world that can shelter Octo during storms
-Added/reworked narrative events
===================

UI
===================
Beta
---------------------------------------------------------------------------------------------
-Shifted UI look from navy blue to yellow for visibility
-Crafting screen now sorts and highlights recipes based on what is craftable
-Intro scroll is correctly timed with dialogue and skippable with ESC
-Added outro for completing the game
-Expanded tutorial
-Updated credits
-Renamed Goo Glue "Nanites"
-Changed energy bar colours for clarity
-Added gradients to panels for aesthetics
-Added hover tooltips to placeable satellites
-Added more pop text and player-facing error messages
-Renamed "Speed" "Velocity"
-Fixed bugs related to the crafting screen
-Fixed bugs related salvaging
-Fixed bug related to objective banners
-Fixed bug causing text to disappear when a satellite is placed
-Replaced UI icons with asset pack icons
-Crafting screen now tells you if you have a satellite in your inventory and what it is
-Fixed canvas sort order bugs
-Fixed toolbar tool swapping bugs
-Fixed bugs related to tutorial prompts

Old
---------------------------------------------------------------------------------------------
-New intro scroll when entering the game
-New offscreen indicator points in direction of the space station when the station is off screen
-New ship health bar
-New UI icons for tools, items and HUD
-Added interface for repairing major narrative objects on the ship (Antenna, Batteries, Solar Array and Signal Processor)
-Added Goo Glue button to crafting screen
-Added additional tutorial prompts
-Added item description text to all items
-Added audio play/stop buttons to codex to revisit previously unlocked audiologs
-Colour coded text on objectives (ie for resource colours)
-General poptext and feedback additions
-Fixed toolbar selection bug
-Fixed bug causing comm range warnings to get stuck
-Fixed bug causing gas cloud post processing effects to not fade away
===================

SOUND
===================
Beta
---------------------------------------------------------------------------------------------
-Continuing improvements on SFX, doppler effect and dialogue
-New mix
-Adding additional SFX to UI

Old
---------------------------------------------------------------------------------------------
-Added in audio logs which play when narrative events are completed
-Added doppler effect for near-miss projectiles
-Added Octo personality chirps
-Added multiple new sound effects, mixed levels and adjusted music
===================

ART
===================
Beta
---------------------------------------------------------------------------------------------
-Improved lighting via Light Probe Group
-Added reflection group
-Adjusted salvage reflectivity and bloom to make salvage shiny when it catches the light
-Added gas cloud VFX

-More salvage models
-Added thruster VFX
-Added rock shields around station
-Replaced unfinished station with modular asset pack
-Replaced UI icons with icon asset pack

Old
---------------------------------------------------------------------------------------------
-Octo rig complete
-Added idle animation (in-game) and movement animation (not in-game) to Octo
-Added more debris model types
-Added rock shield model
-Added planet texture
-Added UI icons
-Fixed and updated skybox
===================

DEBUG
===================
Old
---------------------------------------------------------------------------------------------
-Add 50 resources of any type using drop-down menu
-Kill Octo
-View how much fuel Octo has (in numbers)
-UI screen and codeside framework set up.
===================