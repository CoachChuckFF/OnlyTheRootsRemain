using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the Class that interfaces with the StoryNode JSONs
[System.Serializable]
public struct StoryNode
{
    [SerializeField]
    private string id;

    [SerializeField]
    private string[] exposition;

    [SerializeField]
    private string[] conversation;

    [SerializeField]
    private string nextFile;

    [System.NonSerialized]
    private ConversationNode[] _conversationNodes;

    public string ID => id;
    public string[] Exposition => exposition;
    public ConversationNode[] ConversationNodes => _conversationNodes;
    //public string NextFile => nextFile;

    public string GetNextFile()
    {
        string ret = nextFile;
        if (nextFile != null)
        {
            int fileTypeStartIndex = nextFile.IndexOf(".json");
            if (fileTypeStartIndex > 0)
            {
                ret = nextFile.Remove(fileTypeStartIndex);
            }
        }

        return ret;
    }

    /// <summary>
    /// deserializes and returns a StoryNode from the file path
    /// </summary>
    /// <param name="path">Relative to Resources</param>
    /// <returns></returns>
    public static StoryNode FromJSONFile(string path)
    {
        //prunes file type if exists
        int fileTypeStartIndex = path.IndexOf(".json");
        if (fileTypeStartIndex > 0)
        {
            path = path.Remove(fileTypeStartIndex);
        }

        // loading in the text asset from resources Folder
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        // deserializing node from text asset
        StoryNode node = JsonUtility.FromJson<StoryNode>(textAsset.text);

        //resolving conversations
        node._conversationNodes = new ConversationNode[node.conversation.Length];

        for (int i = 0; i < node.conversation.Length; i++)
        {
            node._conversationNodes[i] = ConversationNode.FromStoryNodeString(node.conversation[i]);
        }

        return node;
    }
}
