# Slime Prefab
This prefab represents a basic Slime enemy in Unity. It includes essential components for movement, animation, and collision detection.
## Components
1. Slime Script
Controls the Slime's behavior, including movement and interactions with other objects.
Attach the Slime script to handle specific actions such as chasing or attacking the player.
Customize variables in the script to adjust the Slime’s speed, attack range, damage, and detection range.
2. Sprite Renderer
Displays the Slime's sprite.
Customize the Sprite Renderer component to change the Slime's appearance.
Ensure that the sorting layer and order in layer are set appropriately to display correctly in your game's environment.
3. Animator
Controls the Slime's animations, such as idle, move, attack, and die.
Attach an Animator Controller with states and transitions for each animation.
Adjust the Animator's parameters to trigger animations through the Slime script (e.g., isMoving, isAttacking, isDead).
4. Rigidbody 2D
Manages the Slime's physics-based movement.
Set Body Type to Dynamic to allow interactions with the environment.
Customize Mass, Gravity Scale, and other properties to suit the desired physics behavior.
5. Circle Collider 2D
Provides a collision boundary around the Slime.
Set the Radius to match the size of the Slime sprite for accurate collision detection.
## Setup Instructions
Drag the Slime prefab into the scene.
Ensure the Slime script is configured with appropriate settings for movement and attack mechanics.
Assign an Animator Controller with relevant animations to the Animator component.
Adjust the Circle Collider 2D to ensure accurate collision detection with other game objects.
Test the Slime in play mode to confirm behavior and animations.
## Requirements
Unity 2022.3.42f1 or later
### Author 
Mikayla Winant
## Spritesheet author
https://chiecola.itch.io/momo-mama-slime 

