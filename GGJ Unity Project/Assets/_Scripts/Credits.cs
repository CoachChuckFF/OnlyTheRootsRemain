using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    [SerializeField]
    private Button m_BackButton;

    private void Awake()
    {
        m_BackButton.onClick.AddListener(LoadTitleScreen);
    }

    private void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
