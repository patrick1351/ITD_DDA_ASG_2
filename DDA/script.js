import {getAuth, createUserWithEmailAndPassword, signOut,onAuthStateChanged, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-auth.js";
import {get, set, getDatabase, ref  } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";
import { getAnalytics } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-analytics.js";
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries
const db = getDatabase(app);
const analytics = getAnalytics(app);
const playerRef = ref(db, "Player");
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
var frmCreateUser = document.getElementById("frmCreateUser");

//we create a button listener to listen when someone clicks
frmCreateUser.addEventListener("submit", function(event){
    event.preventDefault();
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    createUser(email, password);
    console.log("email" + email + "password" + password);
});

//create a new user based on email n password into Auth service
//user will get signed in
//userCredential is an object that gets
function createUser(email, password){
    console.log("creating the user");
    createUserWithEmailAndPassword(auth, email, password)
    .then((userCredential)=>{
        //signedin
        const user = userCredential.user;
        console.log("created user ... " + userCredential.toJSON());
        console.log("User is now signed in ");
    }).catch((error)=>{
        const errorCode = error.code;
        const errorMessage = error.message;
        console.log(`ErrorCode: ${errorCode} -> Message: ${errorMessage}`);
    });
}

let btnSignup = document.getElementById("floatingSubmit"); //signup btn
btnSignup.addEventListener("click", function (e) {
    e.preventDefault();
    let email = document.getElementById("email").value;
    let password = document.getElementById("password").value;

    
    console.log(`Sign-ing up user with ${email} and password ${password}`);
    //[STEP 4: Signup our user]
    signUpUserWithEmailAndPassword(email, password);
});