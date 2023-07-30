## Ant Pathfinding Demo
Influenced by all of the AI hype, I thought it would be fun to create a system that had the appearence of a very simple intelligence. 
Something like that old game [Sim Ant](https://en.wikipedia.org/wiki/SimAnt).

#### Design
* A simple implementation of the [State Pattern](https://en.wikipedia.org/wiki/State_pattern). The Ants have two states, idle or pathfinding. 
* Pathfinding with the A* Algorithm using [Introduction to the A* Algorithm from Red Blob Games](https://www.redblobgames.com/pathfinding/a-star/introduction.html).

#### Technology
* [Raylib](https://github.com/raysan5/raylib) the game library 
* [libresprite](https://libresprite.github.io/#!/) for graphics and animations
* [ChatGPT](https://chat.openai.com/) to help with math


## Game
Play as a hungry queen ant on the quest for pizza. Spawn helper ants to feed the colony.

![Queen ant with pizza](Assets/screenshot.png)

[Download win-x64](https://drive.google.com/file/d/1bcsQgn3o6bJdyCFmIwnDGvPmaUucWF8t/view?usp=drive_link) 5.8 MB

### Controls - Mouse only
* Left click: move
* Right click: spawn ant
* ScrollWheel: +/- zoom

### Environment 
* ![Grass](Assets/grass.png) Easy to traverse  
* ![Rocky](Assets/rocky.png) Rougher terrain
* ![Impassable](Assets/impassable.png) Impassable
