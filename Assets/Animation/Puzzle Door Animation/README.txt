Hey Guys, I made a mock example of the door opening system to better display the sequence diagram. The GameManager object only exists to signal the start and the end of a level. In the future, the GameManager will keep track of how many items/objects are on the screen at a given time, the length of the user's play time, etc.

Sequence Diagram - Find Key

The actor in this case is the person playing the game. The actor is the only person that has the ability to trigger events. IE). The actor himself/herself can physically click a door. When this happens, the OnMouseDown() function will be called by the Door itself, and the door will open. The door opening is a response to this synchronous message to the door; this response of course is a visual response, instead of a verbal one.

The Inventory system of the character will be attached to the MainCharacter object rather than the Manager. Even though the manager has a name that may make it seem like it would be good at holding the inventory, I do not think this is best. There is only 1 inventory to consider, that is the user's. If there exists multiple inventories, then the manager may be able to exist.

Sequence Diagram - Library Puzzle

I left out the MainCharacter object as for the idea of him completing the puzzle, no other entites are giving or taking anything from the main character specifically; not like in the previous diagram where he receives inventory. Because it is pure interaction between the actor, and the door/villain, I felt it was best to continue as such.

I hope this clears up the form of my Sequence Diagram. Feel Free to play around with the Unity thing. Let me know your thoughts on the diagram; revisions, thoughts, etc.

Thanks!!!!