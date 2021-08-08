using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenuPrompt : MonoBehaviour
{
   
    public void OnClickNo()
    {
        gameObject.SetActive(false);
    }

    public void OnClickYes()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene("Cutscene1", LoadSceneMode.Additive);
    }
}
