
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
import { getAuth, initializeAuth, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.6.1/firebase-auth.js";
import { getDatabase, ref, child, get, set, query, orderByChild, limitToLast, onValue, push  } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-database.js"


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
const dbLeaderboardRef = ref(db, 'leaderboard');
//const playerProfile = red(db, "players/" + auth.currentUser.userID);

$(document).ready(function(){

    JSC.Chart('chartDiv', {
        type: 'pie',
        series: [
            {
                points: [
                    {x: 'Apples', y: 50},
                    {x: 'Oranges', y: 42}
                ]
            }
        ]
    });

    console.log("state = unknown (until the callback is invoked)")

    auth.onAuthStateChanged(user => {
        if (user) {
            console.log("state = definitely signed in")
            const uid = user.uid;
            console.log(uid);
            GetPlayer(uid);
            GetLeaderbaord(uid);
            GetQuiz(uid);
            Leaderboard();

            //This is suppose to work with the correct data but idk why it doesnt work
            let data = GetTask();
            ToChart(data);
            console.log(data);
            //GetPlayerLog(uid);
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
    
    function GetTask(){
        let key = [];
        get(child(dbref, `tasks`)).then((snapshot) => {
            
            if (snapshot.exists()) {
                console.log(snapshot.val());
                snapshot.forEach(function(x) {
                    //console.log(x.val());
                    key.push({name: x.key, y: x.val()})
                });
                
            } else {
                console.log("No data available");
            }
            }).catch((error) => {
            console.error(error);
            });
         return [{name: 'Task', points: key}]
    }

    function ToChart(data){
        console.log(data)
        var chart = new JSC.Chart('chartDiv', {
            debug: true,
            type: 'pie',
            series:
            [
                {
                    name: "task",
                    points: [{name: "axe", y: 100},{ name: "tent", y: 10}]
                }
            ]
        });
    }

    async function Leaderboard(){
        const leaderboardPos = query(ref(db, 'leaderboard/'), orderByChild('speedRunSeconds'), limitToLast(5))
        let result = await get(query(leaderboardPos));
        if (result !== null) {
            var playerKey = Object.values(result.val());
            for(var i = 0; i < playerKey.length; i++){
                var pos = i + 1;
                var name = playerKey[i].userName;
                var time = playerKey[i].speedRunTime;
                AddingLeaderboard(pos, name, time)
                console.log(playerKey[i]);
            }
            //console.log(playerKey);
        }
    }

    function AddingLeaderboard(pos, name, time){
        console.log(`Position is: ${pos}, Name is: ${name}, Time is: ${time}`)
        document.getElementById("leaderboard").innerHTML += `
        <div class="leaderboardinformation">
            <div class="userinfo">${pos}</div>
            <div class="userinfo">${name}</div>
            <div class="userinfo">${time}</div>
        </div>
        `;
    }

    function GetPlayerLog(uid){
        get(child(dbref,`playerLogs/${uid}`)).then((snapshot) =>{
            if(snapshot.exists()){
                console.log(snapshot.val());
            } else{
                console.log("No data available");
            }
        }).catch((error) =>{
            console.log(error);
        });
    }

});


