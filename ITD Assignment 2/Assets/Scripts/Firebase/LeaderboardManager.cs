using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public FirebaseManager firebaseManager;
    public GameObject rowPrefab;
    public Transform tableContent;


    public async void UpdateLeaderboardUI()
    {
        //Debug.Log("This function is being called but idk why the data is not being displayed");
        var leaderboardList = await firebaseManager.GetLeaderboard(3);
        int rankCounter = 1;

        foreach (Transform item in tableContent)
        {
            Destroy(item.gameObject);
        }
        //Debug.Log("Rows are supposed to be instantiate");
        //Debug.Log(leaderboardList);
        foreach (Leaderboard leaderboard in leaderboardList)
        {
            if(leaderboard == null)
            {
                Debug.Log("<color=red>Something is wrong</color>");
            } else
            {
                Debug.Log("<color=red>Something is working?</color>");
            }
            //Debug.Log(leaderboard);
            //Debug.Log("Is this function being called?");
            GameObject entry = Instantiate(rowPrefab, tableContent);
            TextMeshProUGUI[] leaderboardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();

            //For the rank number
            leaderboardDetails[0].text = rankCounter.ToString();

            //For the username
            leaderboardDetails[1].text = leaderboard.userName;

            //For the speedrun seconds
            leaderboardDetails[2].text = leaderboard.speedRunTime;

            rankCounter++;
        }
    }
}

