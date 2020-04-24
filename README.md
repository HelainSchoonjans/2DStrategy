# 2D strategy game

A game based on this [Udemy course](https://www.udemy.com/course/the-ultimate-guide-to-making-a-2d-strategy-game-in-unity).

## Skipped steps & issues
- creating trees
- the highlighting is not working as expected. To investigate.
- at the moment I had to increase the move speed because the game elements are larger than shown in the video. How to scale things nicely, and ensure the walkable tiles are symmetrically picked?

## Troubleshooting
- by adding a box collider I could make my clicks on the characters detected. I also had to center the box collider on the characters; there was an offset.
- the Obstacle layer had to be created and the character layer was set to obstacle.
- there was an offset, now corrected on the Exclamation warrior. The idle animation was changed.