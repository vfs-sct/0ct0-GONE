0ct0-GONE M1
---------------------------------------------------

This is the Milestone One submission for 0CT0-GONE.

Josh Paquette			Jesse Rougeau
Evan Landry			Kristin Ruff-Frederickson
Andrew Icardi			Roger Crusafon-Pont

CONTROLS
===================
W/S - Move forward/back
A/D - Move left/right
Space/Ctrl - Move up/down
Mouse - Look around
Scroll - Zoom
Esc - Pause/Unpause/Close Menu
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

BUG: Sound is loaded along with it's respective banks, however, decides to only play after tabbing in and out of the program. 
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
