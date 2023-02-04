using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class Dialog
{
    private DialogController _controller;

    private string _text;

    private IEnumerator _internalEnumerator;

    private bool _isComplete = false;

    private bool _isRunning = false;

    public bool IsComplete => _isComplete;

    public bool IsRunning => _isRunning;

    public Dialog(string text, DialogController parent)
    {
        _text = text;
        _controller = parent;
    }

    public void Skip()
    {
        while (_internalEnumerator.MoveNext()) ;
    }

    /// <summary>
    /// Creates an enumerator that can be run as a coroutine to display this dialogs text
    /// </summary>
    /// <param name="textBox">The text box to display text to</param>
    /// <returns></returns>
    public IEnumerator GetTextEnumerator(TMP_Text textBox)
    {
        _internalEnumerator = MakeInternalEnumerator(textBox);

        while (_internalEnumerator.MoveNext())
        {
            yield return _internalEnumerator.Current;
        }
    }

    //PRIVATE USE ONLY
    private IEnumerator MakeInternalEnumerator(TMP_Text textBox)
    {
        textBox.text = System.String.Empty;
        _isRunning = true;

        for (int c = 0; c < _text.Length; c++)
        {
            // Looks for pause notation e.g. [0.5]
            if (_text[c] == '[')
            {
                int escapeStartIndex = c;

                c++;
                string waitForSeconds = System.String.Empty;
                while (_text[c] != ']' && c < _text.Length)
                {
                    waitForSeconds += _text[c];
                    c++;
                }
                c++;

                if (_text[c] == ' ' && _text[escapeStartIndex - 1] == ' ')
                {
                    c++;
                }

                if (System.Single.TryParse(waitForSeconds, out float result))
                {
                    yield return new WaitForSeconds(result);
                }
                else
                {
                    Debug.LogWarning($"Dialog encountered an error with pausing near character {c}. Found \"{waitForSeconds}\"");
                }
            }

            textBox.text += _text[c];

            yield return new WaitForSeconds(_controller.SecondsPerCharacter);
        }
        _isRunning = false;
        _isComplete = true;
    }
}

//[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
//public class Dialog : ScriptableObject
//{
//    [SerializeField]
//    private float m_SecondsPerCharacter;

//    [SerializeField]
//    private string _characterName;

//    [SerializeField, TextArea]
//    private string _text;

//    [System.NonSerialized]
//    private IEnumerator _internalEnumerator;

//    [System.NonSerialized]
//    private bool _isComplete = false;

//    [System.NonSerialized]
//    private bool _isRunning = false;

//    public string CharacterName => _characterName;

//    public bool IsComplete => _isComplete;

//    public bool IsRunning => _isRunning;

//    public void Skip()
//    {
//        while (_internalEnumerator.MoveNext());
//    }

//    /// <summary>
//    /// Creates an enumerator that can be run as a coroutine to display this dialogs text
//    /// </summary>
//    /// <param name="textBox">The text box to display text to</param>
//    /// <returns></returns>
//    public IEnumerator GetTextEnumerator(TMP_Text textBox)
//    {
//        _internalEnumerator = MakeInternalEnumerator(textBox);

//        while (_internalEnumerator.MoveNext())
//        {
//            yield return _internalEnumerator.Current;
//        }
//    }

//    //PRIVATE USE ONLY
//    private IEnumerator MakeInternalEnumerator(TMP_Text textBox)
//    {
//        textBox.text = System.String.Empty;
//        _isRunning = true;

//        for (int c = 0; c < _text.Length; c++)
//        {
//            // Looks for pause notation e.g. [0.5]
//            if (_text[c] == '[')
//            {
//                int escapeStartIndex = c;

//                c++;
//                string waitForSeconds = System.String.Empty;
//                while (_text[c] != ']' && c < _text.Length)
//                {
//                    waitForSeconds += _text[c];
//                    c++;
//                }
//                c++;

//                if (_text[c] == ' ' && _text[escapeStartIndex - 1] == ' ')
//                {
//                    c++;
//                }

//                if (System.Single.TryParse(waitForSeconds, out float result))
//                {
//                    yield return new WaitForSeconds(result);
//                }
//                else
//                {
//                    Debug.LogWarning($"Dialog: {this.name}, encountered an error with pausing near character {c}. Found \"{waitForSeconds}\"");
//                }
//            }

//            textBox.text += _text[c];

//            yield return new WaitForSeconds(m_SecondsPerCharacter);
//        }
//        _isRunning = false;
//        _isComplete = true;
//    }
//}
