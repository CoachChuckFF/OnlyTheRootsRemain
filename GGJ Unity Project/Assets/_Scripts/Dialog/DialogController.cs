using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogController : MonoBehaviour
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

    private void Start()
    {
        //TEST: this is test code
        m_NameText.text = _currentDialog.CharacterName;
        StartCoroutine(_currentDialog.GetTextEnumerator(m_TextBox));
    }

}
