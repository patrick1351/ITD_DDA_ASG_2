using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    bool published;

    [Header("Timer")]
    public TextMeshPro time;
    float speedrunTime;
    public bool gamePaused;
    public bool gameEnd;

    [Header("Campfire")]
    public GameObject campfireStarting;
    public GameObject campfireBuilding;
    public GameObject tentBuilding;
    Campfire campfireScript;
    BuildingObject campfireBuildingScript;
    BuildingObject tentBuildingScript;

    [Header("For Firebase")]
    [SerializeField] PlayerLog playerLogScript;

    public List<string> objectsName = new List<string>();
    public List<BuildingObject> buildObjects = new List<BuildingObject>();

    public bool speedrun;
    [SerializeField] string speedrunTimeString;
    [SerializeField] bool finished;
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
        if (!gamePaused || !gameEnd)
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
            if(tentBuildingScript.completedBuilding && campfireScript.fireStarted)
            {
                Debug.Log("<color=darkblue>Night has come</color>");
                if (!published)
                {
                    ToPlayerLog();
                }
                //Ask if they wanna switch to quiz
                //Switch Scene
            }
        }
    }
    
    public void Check()
    {
        //Looping all objects
        for (int i = 0; i < objectsName.Count; i++)
        {
            //looping all names
            for (int x = 0; x < objectsName.Count; x++)
            {
                //If the object matches
                if(buildObjects[i].objectName == objectsName[x])
                {
                    //Ensue the object exist in the dictionary
                    if (taskCompleted.ContainsKey(buildObjects[i].objectName))
                    { 
                        //Set the correct bool for the objects
                        taskCompleted[objectsName[x]] = buildObjects[i].completedBuilding;
                    }
                }
            }
        }
    }

    void ToPlayerLog()
    {
        playerLogScript = new PlayerLog(speedrun, speedrunTimeString, finished);
        published = true;
    }
}