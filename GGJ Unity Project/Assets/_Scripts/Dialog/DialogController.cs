using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.EventSystems;

public class DialogController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private TMP_Text m_NameText;

    [SerializeField]
    private TMP_Text m_TextBox;

    [SerializeField]
    private Dialog _currentDialog;

    //TEST
    [SerializeField]
    private Dialog _nextDialog;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (_currentDialog != null)
        {
            if (_currentDialog.IsComplete && _nextDialog != null)
            {
                StartDialog(_nextDialog);
                _nextDialog = null;
            }
            else
            {
                _currentDialog.Skip();
                StopAllCoroutines();
            }
        }
    }

    private void Start()
    {
        StartDialog(_currentDialog);
    }

    private void StartDialog(Dialog dialog)
    {
        _currentDialog = dialog;
        _currentDialog.Initialize();
        m_NameText.text = _currentDialog.CharacterName;
        StartCoroutine(_currentDialog.GetTextEnumerator(m_TextBox));
    }

}
