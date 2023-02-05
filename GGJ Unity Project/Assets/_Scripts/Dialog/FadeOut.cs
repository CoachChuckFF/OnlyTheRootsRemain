using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Events;

public class FadeOut : MonoBehaviour
{
    [SerializeField]
    private Image m_Image;

    public void Fade(UnityAction callback)
    {
        StartCoroutine(FadeOutCoroutine(callback));
    }

    private IEnumerator FadeOutCoroutine(UnityAction callback)
    {
        Color color = m_Image.color;
        while (color.a < 1)
        {
            color.a += Time.deltaTime / Settings.FadeTime;
            m_Image.color = color;
            yield return null;
        }

        callback?.Invoke();
    }
}
