
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
import { getAuth, initializeAuth, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.6.1/firebase-auth.js";
import { getDatabase, ref, child, get, set, orderByChild } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-database.js"


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
const user = auth.currentUser;
const db = getDatabase();
const dbref = ref(db);
//const playerProfile = red(db, "players/" + auth.currentUser.userID);

$(document).ready(function(){
    console.log("state = unknown (until the callback is invoked)")

    auth.onAuthStateChanged(user => {
        if (user) {
            console.log("state = definitely signed in")
            const uid = user.uid;
            console.log(uid);
            GetPlayer(uid);
            GetLeaderbaord(uid);
            GetQuiz(uid);
        }
        else {
            console.log("state = definitely signed out")
        }
    })

    $("#signOut").click(function(event){
        event.preventDefault();
        auth.onAuthStateChanged(user => {
            //log out player
            if (user) {
                console.log("Signing Out")
                auth.signOut().then(function() {
                    console.log('Signed Out');
                    window.location.href = "index.html"
                }, function(error) {
                    console.error('Sign Out Error', error);
                });
            }
            else {
                console.log("Nothing to sign out boys")
            }
        })
    })
    
    function GetLeaderbaord(uid){
        
        get(child(dbref, `leaderboard/${uid}`)).then((snapshot) => {
            
            if (snapshot.exists()) {
                console.log(snapshot.val());
                $("#speedRunTime").text(snapshot.val().speedRunTime)
            } else {
                console.log("No data available");
                $("#speedRunTime").css("font-size", "20px")
                $("#speedRunTime").text("You have not played a game yet");
            }
        }).catch((error) => {
            console.error(error);
        });
    }
    
    function GetPlayer(uid){
        
        get(child(dbref, `players/${uid}`)).then((snapshot) => {
    
            if (snapshot.exists()) {
                console.log(snapshot.val());
                $("#username").text(snapshot.val().name)
                $("#region").text(snapshot.val().region.toUpperCase())
            } else {
              console.log("No data available");
            }
        }).catch((error) => {
        console.error(error);
        });
    }
    
    function GetQuiz(uid){
        
        get(child(dbref, `quizScore/${uid}`)).then((snapshot) => {
    
        if (snapshot.exists()) {
            console.log(snapshot.val());
            $("#quizScore").text(snapshot.val() + "/5")
        } else {
            console.log("No data available");
        }
        }).catch((error) => {
        console.error(error);
        });
    }

});


