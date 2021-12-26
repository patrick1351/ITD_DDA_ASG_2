
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
import { getAuth, initializeAuth, signInWithEmailAndPassword, setPersistence, } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-auth.js";

const firebaseConfig = {
    apiKey: "AIzaSyDumiCO3iKa4reBNhqJRzbj3bwhUMa3EIY",
    authDomain: "itd-dda-demo-firebase.firebaseapp.com",
    databaseURL: "https://itd-dda-demo-firebase-default-rtdb.asia-southeast1.firebasedatabase.app",
    projectId: "itd-dda-demo-firebase",
    storageBucket: "itd-dda-demo-firebase.appspot.com",
    messagingSenderId: "393469966642",
    appId: "1:393469966642:web:238b7c5658112ebfa1ff2b",
    measurementId: "G-M7MBVQKL85"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const auth = getAuth(app);

$(document).ready(function(){

    $("#submitSignIn").click(function(event){
        console.log("Testing");
        var email = $("#email").val();
        var password = $("#password").val();
        console.log( "Email is: " + email + " Password: " + password );

        //Log out player if there is a player logged in
        if(auth.currentUser != null){
            auth.signOut().then(function() {
                console.log('Signed Out');
              }, function(error) {
                console.error('Sign Out Error', error);
              });
        }


        SignInUser(email, password);
        event.preventDefault();
    });

    $("#test").click(function(event){
        console.log(auth.currentUser);
    })

    function SignInUser(email, password){
        console.log("Signing in User");
        signInWithEmailAndPassword(auth, email, password)
        .then((userCredential) => {
            // Signed in 
            const user = userCredential.user;
            console.log("Successful Signed In of " + email)
            console.log(auth.currentUser);
            //console.log("created user ... " + userCredential.toJson());
            window.location.href = "profile.html";
          })
          .catch((error) => {
            const errorCode = error.code;
            const errorMessage = error.message;
          });
    }
});


