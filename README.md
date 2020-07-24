0ct0-GONE ALPHA
---------------------------------------------------

This is the Alpha submission for 0CT0-GONE.

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
2 - Goo Glue (repair glue)
3 - Claw Tool
4 - Satellite Placer
Left Click - Use equipped tool
V - Open Inventory
C - Open crafting menu when near space station
F - Refuel Octo when near space station
Esc - Pause/Unpause/Close Menu
F1 - Debug menu
====================

INSTRUCTIONS
===================
In this iteration, all narrative objectives are implemented and can be played through. In-game tutorial prompts should guide the player through completing them.

Not currently communicated in this build is that ship health is now vital to completing the game, and Octo will have to periodically repair damage sustained by
the ship during storms using the Repair tool. The Repair tool requires Goo Glue to operate, which can be refilled using Silicon in the Crafting screen.
===================

BUGS/WARNINGS
===================
-Previous iterations of the game included a look inversion bug that would disable the ability to look up and down. This has since been fixed, but if you've
opened a version of the game on your PC before, you may have persistent player preferences that retain the issue. This can be fixed by opening the Options
screen and toggling look inversion.
-There is a bug in the salvage event that will cause it to autocomplete if the player deposits any amount of salvage in the station.
-There is a UI bug that causes the objective banner to get stuck if the player refuels before the first banner has fully faded away. This is because the new
banner is trying to lerp in at the same rate the old banner is trying to lerp out.
-There is a progress bar that pops up while salvaging, but it does not properly update to reflect salvage progress.
===================


In this build:

GAMEPLAY
===================
Alpha
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

Old
---------------------------------------------------------------------------------------------
-New inventory system!
	-Octo's inventory is now made up of a "chunk" system
	-Octo can carry up to 10 "chunks" of each type of raw resource
	-The debris types that Octo can salvage can be 1-3 chunks large, so the player must plan how to fit the
	 the most debris pieces into their inventory
	-Octo's inventory can be opened and managed using the new Inventory screen (V)
-Salvage in Octo's inventory is transferred to the space station's storage automatically when the crafting menu is opened
	-Tabs for salvage chunks and crafted items in Octo's inventory
-5 basic resources. Iron, Silicon, Aluminium, Osmium and Titanium.
-New placeable satellites
	-Place a refuelling satellite in a gas cloud to harvest fuel for Octo's thrusters
-Comm range
	-Range becomes visible and pops a warning as Octo nears its edge
	-Leaving comm range results in death
===================

UI
===================
Alpha
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

Old
---------------------------------------------------------------------------------------------
-Codex entries are locked by default and unlocked as the player completes narrative objectives
-New objective banners appear, telling the player when they have completed an objective and what their next objective is
-Added Inventory Screen (V)
	-Hover over chunks to see info on the salvage
	-Click to remove salvage from inventory and free space
	-Click items tab to view Octo's crafted items
-Reworked Inventory HUD Widget
	-No longer displays numbers; uses inventory "chunk" system described in Gameplay changes
	-Chunks are visually represented as a pie chart and colour-coded by resource
-Updated Crafting Screen
	-Now includes display showing the amount of each resource stored in the space station.
-New death effect
	-Game over screen fades instead of appearing suddenly
	-Post processing is used to dull saturation/add static-y grain/fade in a vignette
-New gas cloud effect
	-Flying into a gas cloud triggers a pop up warning and uses post-processing to tint the screen the colour of the cloud
-Specific shader colours assigned to each resource, so when items are scanned they are easily distinguished
-New distance element showing the distance a hovered item is from the player in meters
-New pop up error text when trying to salvage a resource that you're already full on
-New warning pop up when exiting comm range
-Adjusted sorting layer order on canvases
-Adjusting colours and visuals on all UI
-Added tutorial prompts (not yet activated in game)
===================

SOUND
===================
Alpha
---------------------------------------------------------------------------------------------
-Added in audio logs which play when narrative events are completed
-Added doppler effect for near-miss projectiles
-Added Octo personality chirps
-Added multiple new sound effects, mixed levels and adjusted music

Old
---------------------------------------------------------------------------------------------
-New preload scene for managing sound between scenes
-New main theme music
-Added in-game area triggers for different music tracks
-Added new sound effects
-Sound sliders begin at correct levels
-Fixed thruster sound bug
-Added VO button with WIP voice log audio to codex
===================

ART
===================
Alpha
---------------------------------------------------------------------------------------------
-Octo rig complete
-Added idle animation (in-game) and movement animation (not in-game) to Octo
-Added more debris model types
-Added rock shield model
-Added planet texture
-Added UI icons
-Fixed and updated skybox

Old
---------------------------------------------------------------------------------------------
-Added basic lighting to the scene
-Added Post Processing
	-Bloom/Motion Blur/Depth of Field/Ambient Occlusion used for regular gameplay
	-Grain/Chromatic Aberration/Color Grading used for effects (gas clouds, death, etc)
-Added temp matte materials to objects, including Space Station and debris
-Added salvage model variants
-Added Octo texturing
-Hub area space ship.
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