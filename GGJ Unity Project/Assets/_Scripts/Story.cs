using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField]
    private AudioController m_AudioController;

    [SerializeField]
    private DialogController m_DialogBox;

    [SerializeField]
    private Exposition m_Exposition;

    [SerializeField]
    private FadeOut m_FadeOut;

    private StoryNode _storyNode;

    private StoryState _storyState = StoryState.Loading;

    private int _nextIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        _storyState = StoryState.Loading;
        _storyNode = StoryNode.FromJSONFile($"DialogJSONs/{GameData.Instance.StoryID}");
        MixerController.Instance.ChangeMusicVolume(_storyNode.MainMusicPercentage, 2f);
        m_AudioController.FadeIn();
        m_DialogBox.OnContinue.AddListener(MoveNext);
        m_Exposition.OnContinue.AddListener(MoveNext);

        _storyState = StoryState.Exposition;
        MoveNext();
    }

    private void MoveNext()
    {
        switch (_storyState)
        {
            case StoryState.Loading:

                break;

            case StoryState.Exposition:
                if (_nextIndex < _storyNode.Exposition.Length)
                {
                    m_Exposition.StartExposition(_storyNode.Exposition[_nextIndex]);
                }
                else
                {
                    _storyState = StoryState.FadingIn;
                    _nextIndex = 0;
                    m_DialogBox.SetDialog(_storyNode.ConversationNodes[_nextIndex]);

                    //Counteracts the itteration at the end of the method
                    _nextIndex--;

                    m_Exposition.Fade(() =>
                    {
                        _storyState = StoryState.Conversation;
                        MoveNext();
                    });
                }

                break;

            case StoryState.FadingIn:
                //RETURN
                return;

            case StoryState.Conversation:
                if (_nextIndex < _storyNode.ConversationNodes.Length)
                {
                    m_DialogBox.SetDialog(_storyNode.ConversationNodes[_nextIndex]);
                    m_DialogBox.StartDialog();
                }
                else
                {
                    _storyState = StoryState.FadingOut;
                    m_AudioController.FadeOut();
                    m_FadeOut.Fade(Exit);
                }

                break;

            case StoryState.FadingOut:
                //RETURN
                return;

            default:
                //RETURN
                return;
        }

        _nextIndex++;
    }

    private void Exit()
    {
        string nextFile = _storyNode.GetNextFile();
        GameData.Instance.NextScene(nextFile);
    }

    private enum StoryState
    {
        Loading,
        Exposition,
        FadingIn,
        Conversation,
        FadingOut
    }
}
