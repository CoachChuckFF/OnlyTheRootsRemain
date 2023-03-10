using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Events;

public class Exposition : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_TextBox;

    private TextWriter _textWriter;

    public bool IsComplete => _textWriter != null ? _textWriter.IsComplete : false;

    public UnityEvent OnContinue;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsComplete)
        {
            OnContinue?.Invoke();
        }
    }

    private IEnumerator FadeCoroutine(UnityAction callback)
    {
        CanvasGroup renderer = this.GetComponent<CanvasGroup>();

        while (renderer.alpha > 0)
        {
            renderer.alpha -= Time.deltaTime / Settings.FadeTime;
            yield return null;
        }
        this.gameObject.SetActive(false);

        callback?.Invoke();
    }

    public void StartExposition(string text)
    {
        _textWriter = new TextWriter(text, m_TextBox);
        StartCoroutine(_textWriter.GetTextEnumerator());
    }

    public void Fade(UnityAction callback)
    {
        StartCoroutine(FadeCoroutine(callback));
    }
}
