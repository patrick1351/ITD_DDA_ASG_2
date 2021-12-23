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
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        UpdateLeaderboardUI();
    }

    public async void UpdateLeaderboardUI()
    {
        var leaderboardList = await firebaseManager.GetLeaderboard(5);
        int rankCounter = 1;

        foreach (Transform item in tableContent)
        {
            Destroy(item.gameObject);
        }

        foreach (Leaderboard leaderboard in leaderboardList)
        {
            GameObject entry = Instantiate(rowPrefab, tableContent);
            TextMeshProUGUI[] leaderboardDetails = entry.GetComponentsInChildren<TextMeshProUGUI>();

            //For the rank number
            leaderboardDetails[0].text = rankCounter.ToString();

            //For the username
            leaderboardDetails[1].text = leaderboard.userName;

            //For the speedrun seconds
            leaderboardDetails[3].text = leaderboard.speedRunSeconds.ToString();

            rankCounter++;
        }
    }
}

