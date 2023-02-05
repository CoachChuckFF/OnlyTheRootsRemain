using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_Clip;

    private AudioSource _source;

    private void Awake()
    {
        _source = this.GetComponent<AudioSource>();
        _source.loop = true;
        _source.volume = 0f;
        _source.clip = m_Clip;
        _source.Play();
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(true));
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(FadeCoroutine(false));
    }

    private IEnumerator FadeCoroutine(bool fadeIn)
    {
        while ((fadeIn && _source.volume < 1)
            || (!fadeIn && _source.volume > 0))
        {
            float delta = Time.deltaTime / Settings.FadeTime;
            _source.volume += fadeIn ? delta : -delta;
            yield return null;
        }
    }


}
