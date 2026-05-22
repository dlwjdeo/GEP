using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Title : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel; 
    
    public void StartGame()
    {
        SceneLoader.Instance.LoadMainScene();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}
