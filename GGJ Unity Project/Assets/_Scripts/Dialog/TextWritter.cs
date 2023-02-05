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
        _textBox.text = System.String.Empty;
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

            _textBox.text += _text[c];

            yield return new WaitForSeconds(Settings.CharactersPerSecond);
        }
        _isRunning = false;
        _isComplete = true;
    }
}
