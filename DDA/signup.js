import {getAuth, createUserWithEmailAndPassword, signOut,onAuthStateChanged, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-auth.js";
import {get, set, getDatabase, ref  } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-analytics.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries
const db = getDatabase();
const analytics = getAnalytics();
const playerRef = ref(db, "players");
const auth = getAuth();


//we create a button listener to listen when someone clicks
$("#submitSignup").click(function(event){
    event.preventDefault();
    //Get the value from the input field
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var username = document.getElementById("username").value;
    //Transfer the value to the createUser function
    createUser(email, password, username);
    console.log("email: " + email + "password: " + password + "Username: " + username);
});

//create a new user based on email n password into Auth service
//user will get signed in
//userCredential is an object that gets
function createUser(email, password, username){
    console.log("creating the user");
    createUserWithEmailAndPassword(auth, email, password).then((userCredential)=>{
        //signedin
        const user = userCredential.user;
        setUserSignupData(user.uid, username, email)
        console.log("Signed Up");
        //console.log("created user ... " + userCredential.stringify());
        console.log("User is now signed in ");
    }).catch((error)=>{
        const errorCode = error.code;
        const errorMessage = error.message;
        console.log(`ErrorCode: ${errorCode} -> Message: ${errorMessage}`);
    });
}

function setUserSignupData(userID, name, email){
    set(ref(db, 'players/' + userID), {
        username: name,
        email: email,
      });
}

//Reading the Current User's info
$("#test").click(function(){
    console.log("Viewing")
    viewPlayerData()
});


function viewPlayerData() {
    console.log(`userid is: ${auth.currentUser.uid}`)
    get(ref(db, 'players/' + auth.currentUser.uid)).then((snapshot) => {
        console.log(snapshot.child("username").val());
        if(snapshot.exists()){
            try{
                console.log(snapshot.val())
                $("#displayUsername").text(snapshot.child("username").val());
                $("#displayEmail").text(snapshot.child("email").val());
            }catch(error){
                console.log("Error getPlayerData" + error);
            }
        } else {
            console.log("no data");
        }
    });
} 

//For Signing In the User
//Btn for Signing In
$("#signInBtn").click(function(){
    var email = document.getElementById("emailSignIn").value;
    var password = document.getElementById("passwordSignIn").value;
    signInUser(email,password);
})

function signInUser(email,password){
    console.log("Logging the User");
    signInWithEmailAndPassword(auth, email,password).then((userCredential)=>{
        const user = userCredential.user;
        console.log("This log in is successful")
    }).catch((error)=>{
        console.log("There is trouble logging in the user" + error);
    });
}

//For Logging Out the USer
//Btn for Logging Out the User
$("#logOutBtn").click(function(){
    logOutUser();
})

function logOutUser(){
    signOut(auth).then(() =>{
        console.log("Logging Out of the user is successful")
    }).catch((error)=>{
        console.log("There is trouble signing out the user" + error);
    })
}