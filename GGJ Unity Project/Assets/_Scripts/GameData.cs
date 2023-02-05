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

    private void Start()
    {
        SceneManager.LoadScene(m_StoryID, LoadSceneMode.Additive);
    }

    public void NextScene(string storyID)
    {
        SceneManager.UnloadSceneAsync(m_StoryID);
        m_StoryID = storyID;
        SceneManager.LoadScene(storyID, LoadSceneMode.Additive);
    }
}
