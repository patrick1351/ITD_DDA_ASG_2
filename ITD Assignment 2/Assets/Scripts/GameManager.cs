using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshPro time;
    float speedrunTime;
    public bool gamePaused;
    public bool gameEnd;

    [Header("Campfire")]
    public GameObject campfireStarting;
    public GameObject campfireBuilding;
    BuildingObject campfireBuildingScript;

    private void Start()
    {
        campfireStarting.SetActive(false);
        campfireBuilding.SetActive(true);
        campfireBuildingScript = campfireBuilding.GetComponent<BuildingObject>();
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
    }
}