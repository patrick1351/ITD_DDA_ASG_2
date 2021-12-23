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
    // Start is called before the first frame update
    void Start()
    {
        //UpdateLeaderboardUI();
    }


    public async void UpdateLeaderboardUI()
    {
        var leaderboardList = await firebaseManager.GetLeaderboard(3);
        int rankCounter = 1;

        foreach (Transform item in tableContent)
        {
            Destroy(item.gameObject);
        }
        foreach (Leaderboard leaderboard in leaderboardList)
        {
            Debug.Log("Is this function being called?");
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

