# DroneTest [![Build status](https://ci.appveyor.com/api/projects/status/9jyhov2wx3o5e68v?svg=true)](https://ci.appveyor.com/project/LuizAdolphs/dronetest)

The purpose of this software is to interpret a string command and transform into a X, Y position following a set of rules. 

## Algorithm Explanation

To achieve such result, it was chosen a state machine pattern to validate every rule and build a structure with all needed information to retrieve X and Y position of the drone. 