# Academy2021Assignment

The script hierarchy:

Game.cs

Handles the spawning of objects. Includes definitions for the Kolor enum, and a dictionary for converting the Kolor enum to an actual RGB value. Handles the camera movement. Holds the players stars.




Ball.cs

The player.




Star.cs

The star. Spawns in with an obstacle, and an offset to suit that obstacle.




ColorSwitcher.cs

The colorSwicher. Tells the ball to change to a random color.




Obstacle.cs

Moves and/or rotates. Has attached child obstacleTriggers. Despawns if already passed by the player.




ObstacleTrigger.cs

The actual trigger that the ball hits. Parented to an obstacle.




Future ideas:

It would be interesting to spawn colorSwitchers inside obstacles. I have functionality 

Also, it would be in spirit of the original to spawn "double" obstacles when the game gets harder. I would do this by (depending on the height of the ball or received stars), spawning in another obstacle on top of the initial one, with a scale slightly higher and with a inversed rotation.