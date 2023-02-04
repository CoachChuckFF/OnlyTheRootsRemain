export enum modifier {
    none = "NA",
    shakey = "Sh",
    small = "s",
    large = "l",
}

export enum Character {
    Sapling = "S",
    Oak = "O",
    Narrator = "N"
}

export interface ConversationNode {
    character: Character, //Sapling, Oak, Narrator
    modifier: modifier, // shakey | color
    text: string,
}

export interface StoryNode {
    id: string,
    exposition: string[],              // Start here
    conversation: ConversationNode[],  // In order
    nextFile: string | null,
}
