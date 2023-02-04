using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    [SerializeField]
    private string m_StoryID;

    [SerializeField]
    private DialogController m_DialogBox;

    private StoryNode _storyNode;

    private int _conversationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _storyNode = StoryNode.FromJSONFile($"DialogJSONs/{m_StoryID}");
        m_DialogBox.OnDialogComplete.AddListener(MoveToNextConversation);

        m_DialogBox.StartDialog(_storyNode.ConversationNodes[_conversationIndex]);
    }

    private void MoveToNextConversation()
    {
        _conversationIndex++;
        if (_conversationIndex < _storyNode.ConversationNodes.Length)
        {
            m_DialogBox.StartDialog(_storyNode.ConversationNodes[_conversationIndex]);
        }
        else
        {
            //TODO: add move to next story node
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
