using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public void OnClickReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene("Cutscene1", LoadSceneMode.Additive);
    }

    public void OnClickReturnToGame()
    {
        gameObject.SetActive(false);
    }
}
