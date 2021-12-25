import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
import { getAuth, initializeAuth, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.6.1/firebase-auth.js";


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
const auth = getAuth();


$(document).ready(function(){
    console.log("state = unknown (until the callback is invoked)")

    auth.onAuthStateChanged(user => {
        if (user) {
            console.log("state = definitely signed in");
            $("#navSignIn").html("");
            $("#navProfile").html(`<a href="profile.html">PROFILE</a>`);
        }
        else {
            console.log("state = definitely signed out");
            $("#navSignIn").html(`<a href="SignIn.html">SIGN IN</a>`);
            $("#navProfile").html(``);
        }
    })
})