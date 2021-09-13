# Lords of Automata

## Objective

The objective of the game is to create a prosperous kingdom. To do that, you have to manage your resources and most important your people. If you lose all your people is game over!

## Vikings Raid

Each turn that passes a Viking Raid is coming close to happen, to defend against the Vikings you must create an military army with the same amount of vikings that are coming to raid you. If you do not manage to mach the Vikings army you will lose 2 People for each Viking that remains after combating with your army.

For instance, if there are 35 Vikings coming to raid you and you only have 30 Military the Vikings will kill all your military and another 10 People from your kingdom.

The game will only consider a game over after your turn end and you have less than 1 People remaining, so use your cards wisely!

## Progression

Each end your your turn your progression will be measure using these values:

| Resource | Points |
| -------- | ------ |
| Food     | 1      |
| Gold     | 1.5    |
| Military | 2      |
| People   | 3      |
| Stone    | 1      |
| Wood     | 1      |

The game only shows your highest progression so far. After the mach is over your will be able to check the graph of the entire game

And along if how much resources you gain that turn (displayed in parenthesis along with how much resources you currently have)

## Terrains

Each building can only be placed in some terrain, and if a terrain changes and that building couldn't be built there, it will be destroy after 3 turns, so be careful with your buildings and control the map for your benefit!

Each Terrain has a set of rules that considers all it's north, south, west and east neighbors. And each turn the terrain will try to fit all theses rules, so be careful!

### Grassland

- Will turn into a Mountain if there is a Desert and a Forest neighbors adjacent from teach other
- Will turn into a Desert if all the neighbors are Desert
- Will turn into Forest if there is a River and a Forest in the neighborhood
- Will turn into a River if all other neighbors are Rivers

### Desert

- Will turn into a Swamp if all neighbors are River
- Will turn into a Grassland if at least 3 neighbors are Grasslands

### Forest

- Will turn into a Grassland if there is no River in the neighborhood
- Will turn into a Swamp if at least 2 neighbors are Rivers
- Will turn into a Desert if all the neighbors are Desert

### River

- Will turn into a Desert if all the neighbors are Desert
- Will turn into a Grassland if there is no River in the neighborhood

### Swamp

- Will turn into a Forest if there is less than 2 Rivers in the neighborhood
- Will turn into a River if all the neighbors are Swamps
- Will turn into a Desert if all the neighbors are Desert

### Mountain

- Will turn into a Desert if all the neighbors are Desert

## Cards

### Buildings

Each building will provide resources per turn, but it can also cost some resources, so be careful of each building you are placing.

Some buildings require specific terrains to be placed on, so if you have the necessary resources and you still can't place the building is probably because that building cannot be built on that terrain

### Effects

To use a effect select the desired card an click in any tile in the map. That will

### Source Code

- https://github.com/geldoronie/the-azure-lux-game-jam-2021

### Free Resources Used

- https://game-icons.net/
- https://www.serpentsoundstudios.com/royalty-free-music/celtic-fantasy