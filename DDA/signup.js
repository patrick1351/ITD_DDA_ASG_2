import {getAuth, createUserWithEmailAndPassword, signOut,onAuthStateChanged, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-auth.js";
import {get, set, getDatabase, ref  } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-analytics.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries
const db = getDatabase(app);
const analytics = getAnalytics(app);
const playerRef = ref(db, "players");
const auth = getAuth();

getPlayerData();
function getPlayerData(){
    get(playerRef).then((snapshot) => {//retrieve a snapshot of the data using a callback
    if (snapshot.exists()) {//if the data exist
        try {
            //let's do something about it
            var content = "";
            snapshot.forEach((childSnapshot) => {
            console.log("GetPlayerData: childkey " + childSnapshot.key);
            });
        }catch(error){
            console.log("Error getPlayerData" + error);
        }
        }
    });
}//end getPlayerData


//-----------------------------------------We need to decide which to use-----------------------------
//retrieve element from form
var frmCreateUser = document.getElementById("submitSignup");

//we create a button listener to listen when someone clicks
frmCreateUser.addEventListener("submit", function(event){
    event.preventDefault();
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var username = document.getElementById("username").value;
    createUser(email, password, username);
    console.log("email" + email + "password" + password);
});

//create a new user based on email n password into Auth service
//user will get signed in
//userCredential is an object that gets
function createUser(email, password, username){
    console.log("creating the user");
    createUserWithEmailAndPassword(auth, email, password) .then((userCredential)=>{
        //signedin
        const user = userCredential.user;
        setUserSignupData(userCredential.userID, email, username)
        console.log("created user ... " + userCredential.toJSON());
        console.log("User is now signed in ");
    }).catch((error)=>{
        const errorCode = error.code;
        const errorMessage = error.message;
        console.log(`ErrorCode: ${errorCode} -> Message: ${errorMessage}`);
    });
}

function setUserSignupData(userID, name, email){
    set(ref(db, 'players/' + userId), {
        username: name,
        email: email,
      });
}