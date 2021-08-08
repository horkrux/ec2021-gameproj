using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickGameOver()
    {
        SceneManager.LoadScene("MainMenu");
        //SceneManager.LoadScene("Cutscene1", LoadSceneMode.Additive);
    }
}
