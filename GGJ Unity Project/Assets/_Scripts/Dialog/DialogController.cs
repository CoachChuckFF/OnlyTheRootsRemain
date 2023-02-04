using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_TextBox;

    [SerializeField]
    private Dialog _currentDialog;

    private void Start()
    {
        //TEST: this is test code
        StartCoroutine(_currentDialog.GetTextEnumerator(m_TextBox));
    }

}
