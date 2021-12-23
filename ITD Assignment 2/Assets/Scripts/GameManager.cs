using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool published;

    [Header("Timer")]
    public TextMeshPro time;
    /// <summary>
    /// The time in seconds only
    /// </summary>
    float speedrunTime;
    public bool gamePaused;
    SpeedrunCheck spScript;

    [Header("Campfire")]
    public GameObject campfireStarting;
    public GameObject campfireBuilding;
    public GameObject tentBuilding;
    Campfire campfireScript;
    BuildingObject campfireBuildingScript;
    BuildingObject tentBuildingScript;

    [Header("For Firebase")]
    public FirebaseManager firebaseScript;
    [SerializeField] PlayerLog playerLogScript;

    public List<string> objectsName = new List<string>();
    public List<BuildingObject> buildObjects = new List<BuildingObject>();

    public bool speedrun;
    [SerializeField] string speedrunTimeString;
    public bool finished;
    public Dictionary<string, bool> taskCompleted = new Dictionary<string, bool>();

    private void Start()
    {
        //Add needed stuff to the dictionart
        taskCompleted.Add("rope", false);
        taskCompleted.Add("axe", false);
        taskCompleted.Add("chopTree", false);
        taskCompleted.Add("campfire", false);
        taskCompleted.Add("tent", false);

        //Ensure the correct spot is active at start
        campfireStarting.SetActive(false);
        campfireBuilding.SetActive(true);

        //Get the script
        campfireScript = campfireStarting.GetComponent<Campfire>();
        campfireBuildingScript = campfireBuilding.GetComponent<BuildingObject>();
        tentBuildingScript = tentBuilding.GetComponent<BuildingObject>();
        spScript = FindObjectOfType<SpeedrunCheck>();
        if(spScript != null)
        {
            if (spScript.speedunTime)
            {
                time.gameObject.SetActive(true);
            } else
            {
                time.gameObject.SetActive(false);
            }
        }
    }

    void StartSpeedRun()
    {
        speedrunTime += Time.deltaTime;
        float hours = (speedrunTime / 60) / 60;
        float minutes = speedrunTime / 60;
        float seconds = speedrunTime;
        if(seconds > 60)
        {
            seconds = seconds - (minutes * 60);
        }

        if(minutes > 60)
        {
            minutes = minutes - (hours * 60);
        }
        speedrunTimeString = string.Format("{0}:{1}:{2}", (int)hours, (int)minutes, (int)seconds);
        time.text = string.Format("{0}:{1}:{2}", (int)hours, (int)minutes, (int)seconds);
    }

    private void Update()
    {
        if (!gamePaused || !finished)
        {
            StartSpeedRun();
        }

        if (campfireBuildingScript != null && campfireBuildingScript.completedBuilding)
        {
            campfireStarting.SetActive(true);
            campfireBuilding.SetActive(false);
        }

        if (tentBuildingScript != null && campfireBuildingScript != null)
        {
            //Finished building tent and fire started
            if(tentBuildingScript.completedBuilding && campfireScript.fireStarted)
            {
                finished = true;
                Debug.Log("<color=darkblue>Night has come</color>");
            }
        }
    }
    
    /// <summary>
    /// To check if task is completed for the object and set it to the correct bool
    /// </summary>
    public void Check()
    {
        //Looping all objects
        for (int i = 0; i < buildObjects.Count; i++)
        {
            //looping all names
            for (int x = 0; x < objectsName.Count; x++)
            {
                //If the object name matches
                if(buildObjects[i].objectName == objectsName[x])
                {
                    Debug.Log("--------------------<color=red>I hate dic</color>-------------------------------");
                    Debug.Log(objectsName[x] +"    " + taskCompleted[objectsName[x]]);
                    //Set the correct bool for the objects
                    taskCompleted[objectsName[x]] = true;
                }
            }
        }
    }

    public void ToPlayerLog()
    {
        TaskCompleted taskCompletedScript = new TaskCompleted(taskCompleted["rope"], taskCompleted["axe"], taskCompleted["chopTree"], taskCompleted["campfire"], taskCompleted["tent"]);
        playerLogScript = new PlayerLog(speedrun, speedrunTimeString, (int)speedrunTime, finished);
        firebaseScript.WritePlayerLog(playerLogScript, taskCompletedScript);
        published = true;
        SceneManager.LoadScene("Quiz");
    }
}