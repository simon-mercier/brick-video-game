<p align="center">
  <h1 align="center">Brick - iOS and Desktop video game</h1>
  <a href="https://www.linkedin.com/in/simon-mercier-372b6219b/">
  <img src="https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555">
  </a>

  <p align="center">
    A fun and unique puzzle game where you have to move a brick to a specific tile
  <br/>
  <br/>
    Try a WebGL version of the game: <br />
    <a href="https://simon-mercier.github.io/brick-video-game/"><strong>View WebGL version!</strong></a> <br />
  </p>
</p>

<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project) 
  * [Built With](#built-with)

<!-- ABOUT THE PROJECT -->
## About The Project

When starting the development of this game, I had been experimenting with Unity3d and C# for about a year and thought it was time to produce my first complete game.

I took some inspiration from an old flash game I used to play as a kid and made a mobile game using the same concept. To make it distinct, I added unique twists to it. Indeed, 6 special tiles were added, each serving their own purpose and adding an extra challenge to the game. 

The goal of the game is to move a brick (with touch and mouse swipes or with the keyboard arrows) from the start tile (red tile) to the end tile (blue tile) at an upright position. The game might seem simple at first, but it is actually quite complex and requires some level of strategy and planning to reach the end tile. 

All the 84 levels guide the player to understand the game’s mechanics and guides them to use those mechanics in upcoming levels.

All new tiles reveal dozens of new and unique challenges, making for an amusing user experience.

I encourage you to try it, it’s fun and free 😊


- Note: Although I made this project in 2018, I recently put it on git and updated it a little to make it presentable.

### Built With
- Unity3d and C#. 
  - I learned Unity3d and C# by myself by making multiple games with my free time.
- The levels were designed using a 20x20 pixel bitmap of different colors representing different tiles types. A class is then responsible to convert
the bitmap image to a playable level.
  - To improve loading performance, a static constructor converts and serializes those pixel bitmaps to .dat files once.



