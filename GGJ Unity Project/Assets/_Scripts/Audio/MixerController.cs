using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField]
    private AudioMixer m_Mixer;

    //[SerializeField]
    //private AudioMixerGroup m_Music;

    //[SerializeField]
    //private AudioMixerGroup m_Ambient;

    public static MixerController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeMasterVolume(float newPercentage)
    {
        m_Mixer.SetFloat("Master Volume", (Mathf.Sqrt(newPercentage) - 1) * 80);
    }

    public void ChangeMusicVolume(float newPercentage, float time = 0)
    {
        if (time == 0)
        {
            m_Mixer.SetFloat("Music Volume", (Mathf.Sqrt(newPercentage) - 1) * 80);
        }
        else
        {
            StartCoroutine(FadeMusic(newPercentage, time));
        }
    }

    public float GetMusicVolume()
    {
        float ret = 0;
        if (m_Mixer.GetFloat("Music Volume", out float value))
        {
            ret = Mathf.Pow((value / 80) + 1, 2);
        }

        return ret;
    }

    private IEnumerator FadeMusic(float newPercentage, float time)
    {
        float currentVolume = GetMusicVolume();
        float totalDelta = newPercentage - currentVolume;

        while ((totalDelta > 0 && currentVolume < newPercentage) ||
            (totalDelta < 0 && currentVolume > newPercentage))
        {
            currentVolume += Time.deltaTime * totalDelta / time;
            ChangeMusicVolume(currentVolume);
            yield return null;
        }
    }

}
