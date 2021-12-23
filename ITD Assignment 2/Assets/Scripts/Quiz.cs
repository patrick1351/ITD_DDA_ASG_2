using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;

public class Quiz : MonoBehaviour
{
    public int correctAnswers;
    FirebaseManager firebaseManager;

    public TMP_Text finalScore;


    public void Start()
    {
        //To set the correct answers to 0 at the start
        correctAnswers = 0;
    }

    public void Update()
    {
        //Function to show the score onto the text
        finalScore.text = correctAnswers + "/5";
    }

    //Function used to add the amount of correct answers
    public void addCorrectAnwsers()
    {
        correctAnswers += 1;
    }

    //Function to send the data to the database
    public void sendDataToFBManager()
    {
        firebaseManager.WriteQuizScore(correctAnswers);
    }
}
