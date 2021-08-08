using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        //if (SceneManager.sceneCount < 2)
         //   SceneManager.LoadScene("Cutscene1", LoadSceneMode.Additive);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
