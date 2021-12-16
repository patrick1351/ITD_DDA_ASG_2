using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    //Firebase Reference
    Firebase.Auth.FirebaseAuth auth;
    public DatabaseReference dbReference;

    //For signing up
    [SerializeField] InputField emailInputUp; //The Up at the end stands for sign up
    [SerializeField] InputField nameInputUp;
    [SerializeField] InputField passwordInputUp;
    //Radio/ Dropbox for choosing the region will be added later

    //For signing in
    [SerializeField] InputField emailInputIn; //The Up at the end stands for sign up
    [SerializeField] InputField passwordInputIn;

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

    //Sign Up New Player
    public async void SignUpNewPlayer()
    {
        string email = emailInputUp.text.Trim();
        string password = passwordInputUp.text.Trim();
        FirebaseUser newUser = await SignUpNewPlayerOnly(email, password);

        //For removing the white spaces and replacing the space with the _ for cleaner data
        string name = nameInputUp.text.Trim();
        name = name.Replace(" ", "_");

        if (newUser != null)
        {
            //await CreateNewUser(newUser.UserId, username, username, newUser.Email, team);
            //await UpdatePlayerDisplayName(username);
            CreateUI.SetActive(false);
            MainMenuUI.SetActive(true);
        }
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

    //For creating the User Class Data
    public async Task CreateNewUser(string uuid, string name, string email, string region)
    {
        players newUser = new players(name, email, region, 0);
        Debug.LogFormat("Player details : {0}", newUser.PrintUser());//Refer back the the print user function in the user class script

        //For creating the path root/player/$uuid     $uuid is a key
        await dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newUser.UserToJSON());
    }

    //For Signing in the player
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

    //For pressing the Forget Password button
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

    //For Signing Out User
    public void SignOutUser()
    {
        if (auth.CurrentUser != null)
        {
            Debug.LogFormat("Auth user {0} {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);
            auth.SignOut();
            //After Signing the user out, redirect them back to the starting page
            SceneManager.LoadScene("Sign In Page");
        }
    }
}
