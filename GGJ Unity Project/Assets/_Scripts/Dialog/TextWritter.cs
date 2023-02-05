using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
// Writes text to a textbox in a visual novel style
public class TextWriter
{
    private string _text;

    private IEnumerator _internalEnumerator;

    private TMP_Text _textBox;

    private bool _isComplete = false;

    private bool _isRunning = false;

    public bool IsComplete => _isComplete;

    public bool IsRunning => _isRunning;

    public TextWriter(string text, TMP_Text textBox)
    {
        _text = text;
        _textBox = textBox;
        _textBox.text = "<color=#00000000>" + _text; 
        //_textBox.text = System.String.Empty;
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
    public IEnumerator GetTextEnumerator()
    {
        _internalEnumerator = MakeInternalEnumerator();

        while (_internalEnumerator.MoveNext())
        {
            yield return _internalEnumerator.Current;
        }
    }

    //PRIVATE USE ONLY
    private IEnumerator MakeInternalEnumerator()
    {
        _isRunning = true;

        for (int c = 0; c < _text.Length; c++)
        {
            // Looks for pause notation e.g. [0.5]
            if (_text[c] == '[')
            {
                int escapeStartIndex = c;
                int escapeCharacterCount = 0;

                escapeCharacterCount++;
                string waitForSeconds = System.String.Empty;
                while (_text[escapeStartIndex + escapeCharacterCount] != ']' && escapeStartIndex + escapeCharacterCount < _text.Length)
                {
                    waitForSeconds += _text[escapeStartIndex + escapeCharacterCount];
                    escapeCharacterCount++;
                }
                escapeCharacterCount++;

                if ((escapeStartIndex > 0 && _text[escapeStartIndex - 1] == ' ') &&
                    (escapeStartIndex + escapeCharacterCount < _text.Length && _text[escapeStartIndex + escapeCharacterCount] == ' '))
                {
                    escapeCharacterCount++;
                }

                _text = _text.Remove(escapeStartIndex, escapeCharacterCount);

                if (System.Single.TryParse(waitForSeconds, out float result))
                {
                    yield return new WaitForSeconds(result * 0.001f);
                }
                else
                {
                    Debug.LogWarning($"Dialog encountered an error with pausing near character {c}. Found \"{waitForSeconds}\"");
                }
            }

            //handles Rich Text
            if (c < _text.Length && _text[c] == '<')
            {
                do
                {
                    c++;
                } while (_text[c] != '>' && c < _text.Length);
                c++;
            }

            if (c + 1 <= _text.Length)
            {
                _textBox.text = _text.Insert(c + 1, "<color=#00000000>");
                yield return new WaitForSeconds(Settings.CharactersPerSecond);
            }
            //handles pauses at the end of text
            else
            {
                yield return null;
            }
        }
        _isRunning = false;
        _isComplete = true;
    }
}
