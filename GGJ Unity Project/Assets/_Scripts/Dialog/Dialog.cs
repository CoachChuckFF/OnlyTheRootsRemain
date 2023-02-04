using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    [SerializeField]
    private float m_SecondsPerCharacter;

    [SerializeField]
    private string _characterName;

    [SerializeField, TextArea]
    private string _text;

    private IEnumerator _internalEnumerator;

    public string CharacterName => _characterName;

    public bool IsComplete { get; private set; }

    public bool IsRunning { get; private set; }

    public void Initialize()
    {
        IsComplete = false;
        IsRunning = false;
    }

    public void Skip()
    {
        while (_internalEnumerator.MoveNext());
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
        IsRunning = true;

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
                    Debug.LogWarning($"Dialog: {this.name}, encountered an error with pausing near character {c}. Found \"{waitForSeconds}\"");
                }
            }

            textBox.text += _text[c];

            yield return new WaitForSeconds(m_SecondsPerCharacter);
        }
        IsRunning = false;
        IsComplete = true;
    }
}
