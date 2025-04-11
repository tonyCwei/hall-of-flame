# Hall of Flame

Source Code of Hall of Flame with Unity

[![itch.io Version](https://img.shields.io/badge/Download%20on-itch.io-FA5C5C.svg?logoWidth=150)](https://frigidough.itch.io/hall-of-flame)  


## Highlights

### Map Generation

Map generation is handled by the [BoardManager](Assets/Scripts/GameManager/BoardManager.cs). 
In each level, the system randomly generates the following elements:

- **Tiles**: forming the base ground of the map.
- **Enemies**: placed at random valid positions.
- **Spikes (Hazards)**: scattered throughout the map. 

<p align="center">
  <img src="gifs/Tile.gif" width="100%">
  <br>
  <em>Map Generation</em>
</p>