import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
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
const db = getDatabase();
const dbref = ref(db);
const dbLeaderboardRef = ref(db, 'leaderboard');


$(document).ready(function(){
    
    Leaderboard();

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

})