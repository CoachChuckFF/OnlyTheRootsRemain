export interface Exposition {
    text: string,
    minStrength: number,
}

export interface ConversationChoice {
    choiceText: string,
    rootImpact: number, //Plus or Minus
    nextConversationId: string | null, // Null terminates flow, goes to decision
}

export interface Conversation {
    id: string,
    playerName: string, //p1, p2, narrator
    exposition: string,
    choice: ConversationChoice[],
}

export interface LifeDecision {
    exposition: string,
    choice: LifeDecisionChoice[],
}

export interface LifeDecisionChoice {
    choice: string,
    nextNodeId: string,
}

export interface StoryNode {
    id: string,
    exposition: Exposition[],           // Start here
    startingConversation: Conversation, // Then here
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