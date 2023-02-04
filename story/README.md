# Story Data Info

Structure: Take a look at storyNode.ts 

How a Story Node works:
- Black Screen, white text in the middle, print the 'exposition' depending on strength
- Fade in, start with 'startingConversationId', update strength according to 'rootImpact', follow the tree until 'nextConversationId' == null
- Print the 'lifeDecision' exposition, show the options, on choice, load up the 'nextNodeId' 
- Fade to black, +? years

Each storyNode will be self contained in a `SN-XX-XXX.json` file, where the `SN-XX-XXX` matches the `nextNodeId` of the chosen Life Decision.

