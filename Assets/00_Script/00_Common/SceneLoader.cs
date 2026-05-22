using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void LoadMainScene()
    {
        LoadScene(SceneName.MainScene);
    }

    public void LoadTitleScene()
    {
        LoadScene(SceneName.TitleScene);
    }
}

public static class SceneName
{
    public const string TitleScene = "Title";
    public const string MainScene = "SampleScene";
}
