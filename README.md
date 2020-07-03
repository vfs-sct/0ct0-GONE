0ct0-GONE M3
---------------------------------------------------

This is the Milestone Three submission for 0CT0-GONE.

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
In this iteration, Octo has two objectives to complete to demonstrate how narrative events will work.

Octo will begin with low fuel and must approach the space station and press & hold F to refill it. Refilling Octo's fuel completes objective 1.

Press 1 to equip the salvager tool and fly toward space debris. When you are close enough that it becomes highlighted, click the debris to harvest it using the salvager.
You can see in the bottom left-hand corner which tool is currently equipped. Salvage 100 iron to complete objective two. This currently wins the game.

Note: While not yet tied to objectives, Octo can return to the ship to craft objects. To craft, Octo must return to the base ship and open the crafting menu.
When you are close enough to the ship to craft, tooltips will be displayed. Press C to open the crafting menu.

V can also be pressed at any time to open the inventory screen and manage the debris that Octo is currently carrying.
===================

BUGS/WARNINGS
===================

===================


In this build:

GAMEPLAY
===================
M3
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
-Removed targetting. Hovering a resource now directly enables salvaging.
-Crafting rework. Complicated tier categories have now been replaced with basic components, advanced components, upgrades and craftable satellites.
-Added random asteroid storm encounter.
-Added new refuelling objective to introduce player to concept of refuelling.


Old
---------------------------------------------------------------------------------------------
-Added Crafting screen for converting collected resources into items.
-Movement system. The player can move in six degrees of freedom, including rotation.
-Tool framework. Player can choose between the salvage tool and goo glue (repair glue).
-Crafting. After gathering resources, players can craft basic materials when near the space station.
-Narrative progression. Game now has objectives implemented that can be completed to win the game.
-Fuel. Players must use fuel to move. They will lose if fuel runs out. Fuel can be regained at the space station.
===================

UI
===================
M3
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

Old
---------------------------------------------------------------------------------------------
-Added Crafting screen for converting collected resources into items.
-Added Look Inversion and Look Sensitivity options, saved using PlayerPrefs.
-Tooltips now display when approaching a space station or highlighting an object.
-Added pop up text when resources are added to the inventory.
-WIP HUD widget for displaying what tool is equipped.
-Targeting now visually highlights objects using a shader.
-Narrative audio log text added to Codex.
-Mouse hides when in gameplay, and reappears when in menus.
-Fade in from black when switching scenes.
-Bug in scrollbars fixed.
-Basic UI Framework. All menu screens are navigable and the player can close the application.
-Start Game takes player into gameplay scene.
-Pause Menu, Options, Back, Controls, Exit Game implemented and functional.
-Functional Gamma slider.
-Functional Sound sliders.
-Functional Codex menu that generates buttons/page text based off entries added codeside.
-Credit screen that generates credits codeside.
-Debug Menu
===================

SOUND
===================
M3
---------------------------------------------------------------------------------------------
-New preload scene for managing sound between scenes
-New main theme music
-Added in-game area triggers for different music tracks
-Added new sound effects
-Sound sliders begin at correct levels
-Fixed thruster sound bug
-Added VO button with WIP voice log audio to codex

Old
---------------------------------------------------------------------------------------------
-Thruster sounds when Octo is moving.
-"Bonk" sound on collision with objects.
-Warning alarm when entering a gas cloud.
-Placeholder menu sounds.
-Distance-based space station ambience.
-Main menu music.
-Sound sliders appropriately receiving settings from Wwise channels.
-Solved Wwise bug caused by scriptable object framework.
-WWise integration completed.
-Stubbed in WIP button sounds. Sounds will be replaced but currently demonstrate functionality.
-Separate busses exist for Master, Music, SFX and Dialogue.
===================

ART
===================
M3
---------------------------------------------------------------------------------------------
-Added basic lighting to the scene
-Added Post Processing
	-Bloom/Motion Blur/Depth of Field/Ambient Occlusion used for regular gameplay
	-Grain/Chromatic Aberration/Color Grading used for effects (gas clouds, death, etc)
-Added temp matte materials to objects, including Space Station and debris
-Added salvage model variants

Old
---------------------------------------------------------------------------------------------
-Final Octo model.
-Space debris.
-WIP gas clouds.
-Broken and fixed state satellite models (not implemented in game).
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