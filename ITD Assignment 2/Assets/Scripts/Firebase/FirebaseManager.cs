using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
    DatabaseReference dbTaskReference;
    DatabaseReference dbLeaderboardReference;
    DatabaseReference dbQuizScoreReference;
    DatabaseReference dbPlayerReference;
    DatabaseReference dbPlayerLogsReference;
    Firebase.Auth.FirebaseAuth auth;

    // Start is called before the first frame update
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        InitiateFirebase();
        if(auth.CurrentUser != null)
        {
            Debug.LogFormat("-------------------<Color=red>{0}</Color>-----------------------", auth.CurrentUser.UserId);
            LeaderboardManager lbmScript = FindObjectOfType<LeaderboardManager>();
            if (lbmScript != null)
                lbmScript.UpdateLeaderboardUI();
        }
    }

    // Update is called once per frame
    public void InitiateFirebase()
    {
        dbLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
        dbQuizScoreReference = FirebaseDatabase.DefaultInstance.GetReference("quizScore");
        dbTaskReference = FirebaseDatabase.DefaultInstance.GetReference("tasks");
        dbPlayerReference = FirebaseDatabase.DefaultInstance.GetReference("players");
        dbPlayerLogsReference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    //For adding the count in the tasks data in the database--------------------------------------------------------------------------------
    public void addRopeNumber()
    {
        dbTaskReference.Child("rope").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //Get the number from the database
                DataSnapshot snapshot = task.Result;
                //Convert it to int
                int ropeNumber = Convert.ToInt32(snapshot.Value);
                //Add a number to the count
                ropeNumber += 1;
                //Update the value of it back at the database
                dbTaskReference.Child("rope").SetValueAsync(ropeNumber.ToString());
            }
        });
    }

    public void addAxeNumber()
    {
        dbTaskReference.Child("axe").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //Get the number from the database
                DataSnapshot snapshot = task.Result;
                //Convert it to int
                int axeNumber = Convert.ToInt32(snapshot.Value);
                //Add a number to the count
                axeNumber += 1;
                //Update the value of it back at the database
                dbTaskReference.Child("axe").SetValueAsync(axeNumber.ToString());
            }
        });
    }

    public void addCampFireNumber()
    {
        dbTaskReference.Child("campFire").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //Get the number from the database
                DataSnapshot snapshot = task.Result;
                //Convert it to int
                int campFireNumber = Convert.ToInt32(snapshot.Value);
                //Add a number to the count
                campFireNumber += 1;
                //Update the value of it back at the database
                dbTaskReference.Child("campFire").SetValueAsync(campFireNumber.ToString());
            }
        });
    }

    public void addTent()
    {
        dbTaskReference.Child("tent").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //Get the number from the database
                DataSnapshot snapshot = task.Result;
                //Convert it to int
                int tentNumber = Convert.ToInt32(snapshot.Value);
                //Add a number to the count
                tentNumber += 1;
                //Update the value of it back at the database
                dbTaskReference.Child("tent").SetValueAsync(tentNumber.ToString());
            }
        });
    }

    public void addChopTreeNumber()
    {
        dbTaskReference.Child("chopTree").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //Get the number from the database
                DataSnapshot snapshot = task.Result;
                //Convert it to int
                int treeNumber = Convert.ToInt32(snapshot.Value);
                //Add a number to the count
                treeNumber += 1;
                //Update the value of it back at the database
                dbTaskReference.Child("chopTree").SetValueAsync(treeNumber.ToString());
            }
        });
    }


    //Writing Quiz and leaderboard Score--------------------------------------------------------------------------------------------------------------------
    public void WriteQuizScore(int quizScore)
    {
        string currentUUID = auth.CurrentUser.UserId;
        dbQuizScoreReference.Child(currentUUID).SetValueAsync(quizScore.ToString());
    }
    
    public void WriteLeaderboardData(int speedRunSeconds, string speedRunTime)
    {
        dbLeaderboardReference.Child(auth.CurrentUser.UserId).Child("speedRunSeconds").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log("There is error in getting the speedRunSeconds");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot leaderboardDS = task.Result;
                if (leaderboardDS.Exists)
                {
                    Debug.Log(Convert.ToInt32(leaderboardDS.Value));
                    if (speedRunSeconds < Convert.ToInt32(leaderboardDS.Value))
                    {
                        //Debug.Log("Is this function being called?");
                        Leaderboard newData = new Leaderboard(auth.CurrentUser.DisplayName, speedRunSeconds, speedRunTime);
                        dbLeaderboardReference.Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(newData.LeaderboardToJSON());
                    }
                    else
                    {
                        Debug.Log("The highscore was not beaten");
                        return;
                    }
                }
                else
                {
                    Leaderboard newData = new Leaderboard(auth.CurrentUser.DisplayName, speedRunSeconds, speedRunTime);
                    dbLeaderboardReference.Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(newData.LeaderboardToJSON());
                    //Debug.Log("The data does not exist. Therefore we are writing this data.");
                }
            }
        });
    }

    //For Showing the Leaderboard data
    public async Task<List<Leaderboard>>GetLeaderboard(int limit)
    {
        Query leaderboardQuery = dbLeaderboardReference.OrderByChild("speedRunSeconds").LimitToFirst(limit);

        List<Leaderboard> leaderboardList = new List<Leaderboard>();

        await leaderboardQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error getting the leaderboard data from the database. : Error" + task.Exception);
            }
            else if (task.IsCompleted)
            {
                //Debug.Log("Snapshot exists");
                DataSnapshot snapshot = task.Result;
                Debug.Log(snapshot);
                if (snapshot.Exists)
                {
                    foreach (DataSnapshot i in snapshot.Children)
                    {
                        Debug.Log(i);
                        Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(i.GetRawJsonValue());

                        leaderboardList.Add(leaderboard);

                        Debug.LogFormat("Leaderboard Playername {0} Speedruntime{1}", leaderboard.userName, leaderboard.speedRunSeconds);
                    }
                }
            }
        });
        return leaderboardList;
    }


    //Writing PlayerLog----------------------------------------------------------------------------------------------------------
    public void WritePlayerLog(PlayerLog log, TaskCompleted taskCom)
    {
        Debug.Log("--------------------<color=red>WRITING PLAYER LOGS</color>-------------------------------");
        dbPlayerReference.Child(auth.CurrentUser.UserId).Child("currentGame").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("error");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("HELP");
                DataSnapshot snapshot = task.Result;
                int curentPlayerGame = int.Parse(snapshot.GetRawJsonValue());
                string logJson = JsonUtility.ToJson(log, true);
                Debug.Log(logJson);
                Debug.Log(curentPlayerGame);
                string taskJson = JsonUtility.ToJson(taskCom, true);
                dbPlayerLogsReference.Child("playerLogs/" + auth.CurrentUser.UserId + "/game" + curentPlayerGame).SetRawJsonValueAsync(logJson);
                dbPlayerLogsReference.Child("playerLogs/" + auth.CurrentUser.UserId + "/game" + curentPlayerGame +"/taskCompleted").SetRawJsonValueAsync(taskJson);
            }
        });
    }


    //Adding to the current game count--------------------------------------------------------------------------------------------------
    public void AddCurrentGame()
    {
        dbPlayerReference.Child(auth.CurrentUser.UserId).Child("currentGame").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("error");
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Moving");
                DataSnapshot snapshot = task.Result;
                int curentPlayerGame = int.Parse(snapshot.GetRawJsonValue());
                curentPlayerGame += 1;
                dbPlayerReference.Child("currentGame").SetValueAsync(curentPlayerGame);
                SceneManager.LoadScene("Game");
            }
        });
    }
}
