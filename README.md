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
Space/Shift - Move up/down
Mouse - Look around
Scroll - Zoom
1 - Select scavenger tool
Esc - Pause/Unpause/Close Menu
F1 - Debug menu
====================

In this build:

GAMEPLAY
===================
-Movement system. The player can move in six degrees of freedom.
-Tool framework. The system for swapping tools has been set up and implemented and is awaiting tool scriptable objects to be slotted in.
-Resource economy.
-Loss state. Player loses when Octo runs out of thruster fuel.
-Win state. Gather metal alloy and repair the space ship.
===================

UI
===================
-Basic UI Framework. All menu screens are navigable and the player can close the application.
-Start Game takes player into gameplay scene.
-Pause Menu, Options, Back, Controls, Exit Game implemented and functional.
-Player can return to main menu from gameplay and game over.
-Functional Gamma slider.
	-Saves using PlayerPrefs.
-Functional Sound sliders.
	-Saves using PlayerPrefs.
	-Note: There is no music or dialogue implemented, so MUSIC and DIALOGUE sliders will not appear to affect anything.
-Functional Codex menu that populates buttons/page text based off entries added codeside.
	-Originally slotted for M2. M2 will now focus on adding a lock/unlock system.
-NON-functional look sensitivity slider (has not been hooked up)
-Credit screen that generates credits codeside.
-WIP Inventory panel in player HUD
	-Updates as player collects resources
-WIP Fuel bar in player HUD
	-Updates as player gains/loses fuel for flying
-Objective panel in player HUD
	-Has code to update the player's story objectives, however no objectives currently exist.
-Debug Menu
	-Add fuel and other resources to Octo using a dropdown menu
	-Kill Octo instantly
===================

SOUND
===================
-WWise integration completed.
-Stubbed in WIP button sounds. Sounds will be replaced but currently demonstrate functionality.
-Separate busses exist for Master, Music, SFX and Dialogue.

BUG: Sound is loaded along with its respective banks, however, decides to only play after tabbing in and out of the program. 
===================

ART
===================
-Temp Octo model.
-Hub area space ship.
===================

DEBUG
===================
-Screen and code framework set up. Open with F1
-Add fuel and other resources to Octo using a dropdown menu
-Kill Octo instantly
-View current value of Octo's fuel tank
===================
