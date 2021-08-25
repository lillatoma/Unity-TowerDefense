# Setup for the UI in the Unity Editor

## Understanding the screen space
The screen space is set up with free main parts:  
- The left bar  
- The map/game field  
- The right bar  

All of these move with `ScreenObjects` thanks to the `Alignment` script.  

## Pushing the UI with Alignment script
The alignment script with variable `leftBarSize` makes the left bar exactly `leftBarSize` wide in world space.  
The field of the map is pushed by `leftBarSize` to the right. It is always 32x32 sized.  
The right bar is then pushed by `leftBarSize + 32` and occupies the remaining screen-space.  


