# Runner
Created on Unity 2019.4.9f1

![icon](https://raw.githubusercontent.com/RomanDAnoshin/Runner/master/Assets/Sprites/Icon.png)

### The rules are simple:
* The character runs along an endless road and collects coins.
* The character is moved in three lanes.
* The character dies if he collides with a barricade.
* When a character dies, a restart window appears. And the player's progress is saved in his profile.

### Game
* The game relies on player progress.
* The player's progress is saved. Progress can be reset from the main menu.

### Road
* Generation of the road along the difficulty curve.
* Difficulty curve can be changed in the editor. The main requirement: the difficulty value should not be less than 0 or more than 100. (There will be no technical problems, just such a construction.)
* Road block prefabs can be added in any order to a special field in editor. Main requirement: your new block prefabs must have a difficulty value and a value for number of coins player CAN collect.
* The road speed can be changed from the editor. Also, the speed of the road increases linearly as the character picks up coins.
* The buffer capacity for road blocks can be changed from the editor.

### Character
*  The character's running speed, as well as the subsequent increase in running speed (depending on the coins collected by the player) can be changed in the editor.
*  The speed of turning and moving from lane to lane also increases with the player's progress in collecting coins. They can also be changed in the editor.

### Camera
* The camera follows the character constantly. The script itself is added to the camera when the game is generated.
