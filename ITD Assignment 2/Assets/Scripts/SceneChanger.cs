using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public FirebaseManager fbScript;
    public GameManager gmScript;
    public SpeedrunCheck spScript;

    private void Start()
    {
        gmScript = FindObjectOfType<GameManager>();
    }

    public void startGame(bool speedrun)
    {
        if (speedrun)
        {
            spScript.speedunTime = speedrun;
        } else
        {
            spScript.speedunTime = false;
        }
        fbScript.AddCurrentGame();
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Hand enter
        if(other.gameObject.tag == "hand")
        {
            if(gmScript != null)
            {
                //Game is finished and has not been published before
                if (gmScript.finished && !gmScript.published)
                {
                    gmScript.ToLeaderboard();
                    gmScript.ToPlayerLog();  
                    
                }
            } 
            else
            {
                Debug.Log("--------------------<color=red>GM not found to end game</color>-------------------------------");
            }
        }
    }
}
