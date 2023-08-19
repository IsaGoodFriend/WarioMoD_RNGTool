# WarioMoD_RNGTool

## Tool Usage
Each command file contains the list of actions that are influenced by the main RNG in the game, in the order they happen.  Running the "find route" option will allow you to select your file to run and find an optimal route.  Commands start with the command's name, then parenthesis at the end.

```
FileLength(1-12) [File Name Length]
StartNewFile()

GreenChest(Cosmic)
BulletHit(1-10) [Intro Bullets]
RedChest()
```
Above is an example of a small command file.  "FileLength" tells the program to expect RNG values from creating a file name.  The numbers inside the parenthesis let the program know to expect between 1 and 12 name lengths.  The square brackets at the end help the program properly label it's output file so we know what to do to manipulate the RNG.

The next line just tells it that we're starting a new file (the RNG runs differently in the intro cutscenes.)  Next, we pick up the cosmic power up, which is influenced by this RNG.  Next comes "BulletHits".  Each time Cosmic Wario's laser hits most items, particles are created which use this RNG, so we need to take into account every bullet fired.

Afterwards, we pick up a redchest, also influenced.

With this information, we can feed it into the program, and get our route out.

## Misc. Info.
* The elevator in Chapter 2 some how doesn't make Wario's bullets create particles, so the RNG doesn't call there.
* The program expects you to wait half a second before skipping the Cannoli monologue cutscene.  It's a blind 5 frame window to hit the earlist skip, and mashing skip may not always hit the window.  I would recommend skipping right as the camera starts to pan, though it needs testing to know what the exact window is.
* Do not skip the money countdown at the end of levels, as those are also affected by rng and run every frame.  How many digits of money you have at the end matter to the rng too; the program currently expects 4 digits (e.g. $3,692), which shouldn't matter in any%.  Could pose a problem if you want to route 100%.
* Laser bullets despawn off screen, which can be useful for getting specific bullet hits on the upgraded gun.
* Entering the room of chapter 3 (aka the museum entrance) at any point changes the rng.  That means the start of the level, warping there, or reloading the room.

## Command Glossary
* FileLength(n) => How many times we add a character to our file name.  The eraser, back, and accept buttons do not change the RNG seed.
* GreenChest(power) => The green chests found in the game that give Wario a new disguise.
* GreenChestUpgrade(power) => Any upgrade to an existing ability.
* RedChest()/PurpleChest() => Any red or purple chest we grab.
* Chapter2Elevator() => Whenever we enter the elevator room in chapter 2.
* BulletHit(n) => How many times a laser bullet hits a wall.
* UpgradedBulletShots(n) => How many times a laser bullet is fired and ricochets fully.  Each hit counts, so make sure it hits fully.
* SelectLevel(chapterNumber) => Starting a chapter.  Skipping the intro is required.
* CompleteLevel() => Beating the level and seeing the total count down.
* SphinxSimulator(0 or 1) => Entering a sphinx simulator room.  0 is the one first encountered in any%, 1 is the other.
* Sphinx() => Starting the Sphinx "bossfight".
* Chapter3FirstRoom() => Whenever the first room of Chapter 3 is loaded
* Chapter10SpikeRoom() => Whenever the spike crush room is loaded in Chapter 10.  Can probably be used if the crusher resets.
* 
