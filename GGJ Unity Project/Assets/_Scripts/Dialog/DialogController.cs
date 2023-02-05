using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_NameText;

    [SerializeField]
    private TMP_Text m_TextBox;

    [SerializeField]
    private List<Character> m_Characters;

    private TextWriter _currentTextWriter;

    public bool IsStarted { get; private set; } = false;

    public bool IsComplete => _currentTextWriter != null ? _currentTextWriter.IsComplete : false;

    public UnityEvent OnContinue;

    public void SetDialog(ConversationNode conversation)
    {
        m_NameText.text = conversation.Character.ToString();
        foreach (Character character in m_Characters)
        {
            character.SetTalking(character.ConversationCharacter == conversation.Character);
        }
        _currentTextWriter = new TextWriter(conversation.Text, m_TextBox);
    }

    public void StartDialog()
    {
        if (_currentTextWriter != null)
        {
            StartCoroutine(_currentTextWriter.GetTextEnumerator());
            IsStarted = true;
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currentTextWriter != null && IsStarted && Time.timeScale == 1)
        {
            if (_currentTextWriter.IsComplete)
            {
                OnContinue?.Invoke();
            }
            else
            {
                _currentTextWriter.Skip();
                StopAllCoroutines();
            }
        }
    }

}
