# Enemy Generation
Enemy generation happens before a round starts.  
According to the current level, skillpoints are given with the following taken into calculation  
- Current level
- Levels after level 10  
- Levels after level 25  
- Levels after level 50  
- Levels after level 75  
- Levels after level 100  

A skillpoint is used for advancing the health, speed and amount of enemies in a level.  

Detailed formula in `CurrentLevelHolder.cs`.

