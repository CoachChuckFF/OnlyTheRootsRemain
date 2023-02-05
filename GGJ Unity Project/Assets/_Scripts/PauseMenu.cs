using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Menu;

    [SerializeField]
    private Button m_ContinueButton;

    [SerializeField]
    private Button m_TitleButton;

    void Awake()
    {
        m_Menu.SetActive(false);
        m_ContinueButton.onClick.AddListener(Continue);
        m_TitleButton.onClick.AddListener(ReturnToTitle);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool turnActive = !m_Menu.activeInHierarchy;
            m_Menu.SetActive(turnActive);
            Time.timeScale = turnActive ? 0 : 1;
        }
    }

    private void Continue()
    {
        Time.timeScale = 1;
        m_Menu.SetActive(false);
    }

    private void ReturnToTitle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }
}
