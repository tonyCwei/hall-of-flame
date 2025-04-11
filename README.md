# Hall of Flame

Source Code of Hall of Flame with Unity. The game was made in 4 days in April, 2023.

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

### Enemies

<p align="center">
  <img src="gifs/Archer.gif" width="48%">
  <img src="gifs/Archer U.gif" width="48%">
  <br>
  <em>Archer Base Unit</em> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <em>Archer Advanced Unit</em>
</p>

<p align="center">
  <img src="gifs/Shield.gif" width="48%">
  <img src="gifs/Shield U.gif" width="48%">
  <br>
  <em>Shield Base Unit</em> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <em>Shield Advanced Unit</em>
</p>

<p align="center">
  <img src="gifs/Sword.gif" width="48%">
  <br>
  <em>Sword</em>
</p>

### Shop

An **Upgrade Shop** appears every 5 levels. 
Players can spend coins earned from defeating enemies to purchase upgrades, enhancing their stats. 

<p align="center">
  <img src="gifs/Shop.gif" width="100%">
  <br>
  <em>Map Generation</em>
</p>