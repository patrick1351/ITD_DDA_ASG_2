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
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var username = document.getElementById("username").value;
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

//Reading logged in info
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
