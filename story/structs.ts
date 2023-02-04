export interface Exposition {
    text: string,
    minStrength: number,
}

export interface ConversationChoice {
    text: string,
    rootImpact: number, //Plus or Minus
    nextConversationId: string | null, // Null terminates flow, goes to decision
}

export interface Conversation {
    id: string,
    playerName: string, //p1, p2, narrator
    exposition: string,
    choices: ConversationChoice[],
}

export interface LifeDecision {
    exposition: string,
    choices: LifeDecisionChoice[],
}

export interface LifeDecisionChoice {
    text: string,
    nextNodeId: string,
}

export interface StoryNode {
    id: string,
    exposition: Exposition[],           // Start here
    startingConversationId: string, // Then here
    conversationNodes: Conversation[]   // Search by ID
    lifeDecision: LifeDecision,         // Finally here
}


//
//  Life Node:
//  Black Screen, white text in the middle, print the 'exposition' depending on strength
//  Fade in, start with 'startingConversation', update strength according to 'rootImpact', follow the tree until 'nextConversationId' == null
//  Print the 'lifeDecision' exposition, show the options, on choice, load up the 'nextNodeId' 
//  Fade to black, +10 years
//