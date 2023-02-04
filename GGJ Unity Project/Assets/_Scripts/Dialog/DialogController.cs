using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DialogController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private float m_SecondsPerCharacter;

    [SerializeField]
    private TMP_Text m_NameText;

    [SerializeField]
    private TMP_Text m_TextBox;

    private Dialog _currentDialog;

    public UnityEvent OnDialogComplete;

    //[SerializeField]
    //private Dialog _currentDialog;

    ////TEST
    //[SerializeField]
    //private Dialog _nextDialog;

    public float SecondsPerCharacter => m_SecondsPerCharacter;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (_currentDialog != null)
        {
            if (_currentDialog.IsComplete)
            {
                OnDialogComplete?.Invoke();

            }
            else
            {
                _currentDialog.Skip();
                StopAllCoroutines();
            }
        }
    }

    public void StartDialog(ConversationNode conversation)
    {
        m_NameText.text = conversation.Character.ToString();
        _currentDialog = new Dialog(conversation.Text, this);

        StartCoroutine(_currentDialog.GetTextEnumerator(m_TextBox));
    }

}
