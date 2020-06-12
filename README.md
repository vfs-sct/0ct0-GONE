0ct0-GONE M2
---------------------------------------------------

This is the Milestone One submission for 0CT0-GONE.

Josh Paquette			Jesse Rougeau
Evan Landry			Kristin Ruff-Frederickson
Andrew Icardi			Roger Crusafon-Pont

CONTROLS
===================
W/S - Move forward/back
A/D - Move left/right
Q/E - Rotate character left/right
Space/Ctrl - Move up/down
Mouse - Look around
Esc - Pause/Unpause/Close Menu
Tab or Right-Click - Target object
1 - Equip Salvager Tool
2 - Equip Goo Glue (repair glue)
F1 - Debug menu
====================

In this build:

GAMEPLAY
===================
-Movement system. The player can move in six degrees of freedom.
-Tool framework. The system for swapping tools has been set up and implemented and is awaiting tool scriptable objects to be slotted in.
-Resource economy.
===================

UI
===================
M2
---------------------------------------------------------------------------------------------
-Added Crafting screen for converting collected resources into items.
-Added Look Inversion and Look Sensitivity options, saved using PlayerPrefs.
-Added pop up text when resources are added to the inventory.
-WIP HUD widget for displaying what tool is equipped.
-Targeting now visually highlights objects using a shader.
-Narrative audio log text added to Codex.
-Mouse hides when in gameplay, and reappears when in menus.
-Fade in from black when switching scenes.
-Bug in scrollbars fixed.

M1
---------------------------------------------------------------------------------------------
-Basic UI Framework. All menu screens are navigable and the player can close the application.
-Start Game takes player into gameplay scene.
-Pause Menu, Options, Back, Controls, Exit Game implemented and functional.
-Functional Gamma slider.
-Functional Sound sliders.
	-Note: There is no music or dialogue implemented, so MUSIC and DIALOGUE sliders will not appear to affect anything.
-Functional Codex menu that generates buttons/page text based off entries added codeside.
	-Originally slotted for M2. M2 will now focus on adding a lock/unlock system.
-Credit screen that generates credits codeside.
-Debug Menu
	-Note: There is currently no gameplay to debug, but functions exist to quickly slot in stats for read-only text or create buttons, ie, for spawning objects
===================

SOUND
===================
-WWise integration completed.
-Stubbed in WIP button sounds. Sounds will be replaced but currently demonstrate functionality.
-Separate busses exist for Master, Music, SFX and Dialogue.
===================

ART
===================
-Temp Octo model.
-Hub area space ship.
===================

DEBUG
===================
-UI screen and codeside framework set up.
	-Note: While the screen can be opened, the debug menu has no functionality currently.
===================
