using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private Button m_StartButton;

    [SerializeField]
    private Button m_CreditsButton;

    [SerializeField]
    private Button m_ExitButton;

    // Start is called before the first frame update
    void Start()
    {
        m_StartButton.onClick.AddListener(StartGame);
        m_CreditsButton.onClick.AddListener(LoadCredits);
        m_ExitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Persistent");
    }

    private void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
