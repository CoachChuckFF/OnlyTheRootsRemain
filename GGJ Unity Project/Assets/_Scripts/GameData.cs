using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [SerializeField]
    private string m_StoryID;

    public string StoryID => m_StoryID;

    public static GameData Instance;

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

    public void NextScene(string storyID)
    {
        m_StoryID = storyID;
        SceneManager.LoadScene("StoryScene");
    }
}
