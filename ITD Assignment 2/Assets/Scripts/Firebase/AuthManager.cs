using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{
    //Firebase Reference
    Firebase.Auth.FirebaseAuth auth;
    public DatabaseReference dbReference;

    //For signing up
    [SerializeField] TMP_InputField emailInputUp; //The Up at the end stands for sign up
    [SerializeField] TMP_InputField nameInputUp;
    [SerializeField] TMP_InputField passwordInputUp;
    public string region = "asia";

    //For signing in
    [SerializeField] TMP_InputField emailInputIn; //The Up at the end stands for sign up
    [SerializeField] TMP_InputField passwordInputIn;

    //For toggling the UI
    [SerializeField] GameObject CreateUI;
    [SerializeField] GameObject SignInUI;
    [SerializeField] GameObject MainMenuUI;

    //Buttons
    [SerializeField] Button signUpButton;
    [SerializeField] Button signInButton;
    [SerializeField] Button forgetPasswordButton;
    [SerializeField] Button signOutButton;

    public void Awake()
    {
        //Get the firebase reference on awake
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //For Reading the dropdown box value
    public void HandleInputData(int val)
    {
        if (val == 0)
        {
            region = "asia";
        }
        if (val ==1)
        {
            region = "africa";
        }
        if (val == 2)
        {
            region = "australia";
        }
        if (val == 3)
        {
            region = "europe";
        }
        if (val == 4)
        {
            region = "northAmerica";
        }
        if (val == 5)
        {
            region = "southAmerica";
        }
        Debug.Log(region);
    }

    //Sign Up New Player----------------------------------------------------------------------------------------------------------------------
    public async void SignUpNewPlayer()
    {
        //For removing long white space characters
        string email = emailInputUp.text.Trim();
        string password = passwordInputUp.text.Trim();
        FirebaseUser newUser = await SignUpNewPlayerOnly(email, password);

        //For removing the white spaces and replacing the space with the _ for cleaner data
        string name = nameInputUp.text.Trim();
        name = name.Replace(" ", "_");

        if (newUser != null)
        {
            await CreateNewPlayer(newUser.UserId, name, newUser.Email, region);
            CheckIfCreateTask();
            await UpdateUserDisplayName(name);
            CreateUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }
    }

    public void CheckIfCreateTask()
    {
        dbReference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry there was an error while trying to retrieve the team information");
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerSnapshot = task.Result;
                if (playerSnapshot.Exists)
                {
                    int playerCount = Convert.ToInt32(playerSnapshot.Child("players").ChildrenCount);
                    //If this is the first player, create a brand new task board count
                    if(playerCount == 1)
                    {
                        Debug.Log("This function is supposed to be called");
                        UserTasks newTaskBoard = new UserTasks(0, 0, 0, 0);
                        dbReference.Child("tasks").SetRawJsonValueAsync(newTaskBoard.TaskToJSON());
                    }
                }
            }
        });
    }

    public async Task<FirebaseUser> SignUpNewPlayerOnly(string email, string password)
    {
        FirebaseUser newUser = null;

        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.Log("Create user has failed");
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId, newUser.Email);
            }

        });
        return newUser;
    }

    //For updating the User's display name in authentication and checking it in the console log
    public async Task UpdateUserDisplayName(string name)
    {
        if (auth.CurrentUser != null)
        {
            //Create new user profile to update the display name
            UserProfile profile = new UserProfile
            {
                DisplayName = name
            };
            await auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Update player profile async encountered an error");
                    return;
                }
                if (task.IsCanceled)
                {
                    Debug.LogError("Update player profile async was cancelled");
                    return;
                }

                Debug.Log("User profile has been updated!");
                Debug.LogFormat("Checking current user display name from auth {0}", GetCurrentPlayerName());
            });
        }
    }

    public string GetCurrentPlayerName()
    {
        string currentPlayerName = auth.CurrentUser.DisplayName;

        currentPlayerName = currentPlayerName.Replace("_", " ");
        return currentPlayerName;
    }

    //For creating the User Class Data
    public async Task CreateNewPlayer(string uuid, string name, string email, string region)
    {
        Players newUser = new Players(name, email, region, 0);
        Debug.LogFormat("Player details : {0}", newUser.PrintUser());//Refer back the the print user function in the user class script

        //For creating the path root/player/$uuid     $uuid is a key
        await dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newUser.PlayerToJSON());
    }

    //For Signing in the player-----------------------------------------------------------------------------------------------------------------
    //For checking if the user exists in the database and if yes, sign in new user
    public void SignInUser()
    {
        string email = emailInputIn.text.Trim();
        string password = passwordInputIn.text.Trim();

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                FirebaseUser currentPlayer = task.Result; ;
                Debug.LogFormat("Welcome to You Survive {0} :: {1}", currentPlayer.UserId, currentPlayer.Email);
                SignInUI.SetActive(false);
                MainMenuUI.SetActive(true);

            }
        });
    }

    //For pressing the Forget Password button-------------------------------------------------------------------------------------------------
    //For Reseting the password of a user's account
    public void ForgetPassword()
    {
        string email = emailInputIn.text.Trim();
        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("There was an error sending a password rest, ERROR : " + task.Exception);
                return;
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Forget password email sent successfully ...");
            }
        });
    }

    //For Signing Out User---------------------------------------------------------------------------------------------------------------
    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            Debug.LogFormat("Auth user {0} {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);
            auth.SignOut();
            MainMenuUI.SetActive(false);
            SignInUI.SetActive(true);
        }
    }
}
