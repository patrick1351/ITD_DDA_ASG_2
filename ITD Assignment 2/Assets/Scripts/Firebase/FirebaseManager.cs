using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        InitiateFirebase();
    }

    private void Start()
    {
        Debug.LogFormat("-------------------<Color=red>{0}</Color>-----------------------", auth.CurrentUser.UserId);
    }

    // Update is called once per frame
    public void InitiateFirebase()
    {
        dbLeaderboardReference = FirebaseDatabase.DefaultInstance.GetReference("leaderboard");
        dbQuizScoreReference = FirebaseDatabase.DefaultInstance.GetReference("quizScore");
        dbTaskReference = FirebaseDatabase.DefaultInstance.GetReference("tasks");
        dbPlayerReference = FirebaseDatabase.DefaultInstance.GetReference("players/" + auth.CurrentUser.UserId);
        //dbPlayerLogsReference = FirebaseDatabase.DefaultInstance.GetReference("playerLogs/" + auth.CurrentUser.UserId);
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
                dbTaskReference.Child("axe").SetValueAsync(campFireNumber.ToString());
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
                dbTaskReference.Child("axe").SetValueAsync(tentNumber.ToString());
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
                dbTaskReference.Child("axe").SetValueAsync(treeNumber.ToString());
            }
        });
    }

    //Writing Quiz Score--------------------------------------------------------------------------------------------------------------------
    public void WriteQuizScore(int quizScore)
    {
        string currentUUID = auth.CurrentUser.UserId;
        dbQuizScoreReference.Child(currentUUID).SetValueAsync(quizScore.ToString());
    }

    //Writing the Leaderboard Score----------------------------------------------------------------------------------------------------------
    public void WriteLeaderboardData(int speedRunSeconds)
    {
        Leaderboard newData = new Leaderboard(auth.CurrentUser.DisplayName, speedRunSeconds);
        dbLeaderboardReference.Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(newData.LeaderboardToJSON());
    }

    //For Showing the Leaderboard data
    public async Task<List<Leaderboard>>GetLeaderboard(int limit = 5)
    {
        Query leaderboardQuery = dbLeaderboardReference.OrderByChild("highScore").LimitToLast(limit);

        List<Leaderboard> leaderboardList = new List<Leaderboard>();
        await dbLeaderboardReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error getting the leaderboard data from the database. : Error" + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    //int rankCounter = 1;
                    foreach (DataSnapshot i in snapshot.Children)
                    {
                        Leaderboard leaderboard = JsonUtility.FromJson<Leaderboard>(i.GetRawJsonValue());

                        leaderboardList.Add(leaderboard);

                        //Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Score{2}, rankcounter",rankCounter, leaderboard.userName, leaderboard.highScore);
                    }

                    //Change the list to descending order since the data was sorted in ascending order before; It shows the lowest score first
                    leaderboardList.Reverse();

                    //For each leaderboard object inside the list
                    /*foreach(Leaderboard leaderboard in leaderboardList)
                    {
                        //Debug.LogFormat("Leaderboard: Rank {0} Playername {1} Score{2}, rankcounter", rankCounter, leaderboard.userName, leaderboard.highScore);
                        rankCounter++;
                    }*/
                }
            }
        });
        return leaderboardList;
    }

    //Writing PlayerLog----------------------------------------------------------------------------------------------------------
    public void WritePlayerLog(PlayerLog log, TaskCompleted taskCom)
    {
        Debug.Log("--------------------<color=red>WRITING PLAYER LOGS</color>-------------------------------");
        dbPlayerReference.Child("currentGame").GetValueAsync().ContinueWithOnMainThread(task =>
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
}
