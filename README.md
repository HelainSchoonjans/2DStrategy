# 2D strategy game

A game based on this [Udemy course](https://www.udemy.com/course/the-ultimate-guide-to-making-a-2d-strategy-game-in-unity).

## Skipped steps & issues
- creating trees
- the highlighting makes the tiles blend with the background. How to make the more visible?
- at the moment I had to increase the tile speed because the game elements are larger than shown in the video. How to scale things nicely, and ensure the walkable tiles are symmetrically picked?
- when a character moves, he should avoid obstacles.
- It would be nice to be able to move the camera and zoom.

## Troubleshooting
- by adding a box collider I could make my clicks on the characters detected. I also had to center the box collider on the characters; there was an offset.
- the Obstacle layer had to be created and the character layer was set to obstacle.
- there was an offset, now corrected on the Exclamation warrior. The idle animation was changed.
- walkable tiles were invisible. Changing the transparency of the walkable highlight solved the issue.
- movement bugs; character can go too far.
    - position.x instead of position.y was used in the code.
- the animation of the meditator is now too low
 	- I ended up recreating his animation.
- after having moved, the character can't be selected again. Moving indeed sets z to 0 instead of -1. Either I should use other methods to decide which objects are above the others, or I shoulf ensure z keeps it's value.
    - moving characters were set to z 0, in front of other elements.