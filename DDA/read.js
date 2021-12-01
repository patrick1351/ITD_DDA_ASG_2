import {getDatabase, ref, child, get, set, onValue, orderByChild} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";

const db = getDatabase();

const playerRef = ref(db, "players");

//Setup our event listener
var readBtn = document
 .getElementById("btn-read")
 .addEventListener("click", getPlayerData);

//[STEP 4] Setup our player function to display info
function getPlayerData(e) {
    e.preventDefault();
    get(playerRef).then((snapshot) => { //retrieve a snapshot of the data using a callback
        if (snapshot.exists()) {
        //if the data exist
            try {
                //let's do something about it
                var playerContent = document.getElementById("player-content");
                var content = "";
            
                snapshot.forEach((childSnapshot) => {
                    //looping through each snapshot
                    //https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/forEach
                    console.log("User key: " + childSnapshot.key);
                    console.log("Username: " + childSnapshot.child("username").val());
                    content += `<tr>
                    <td>${childSnapshot.child("active").val()}</td>
                    //======= insert your own place to update UI
                    </tr>`;
            });
            //update our table content
            playerContent.innerHTML = content;
            } catch (error) {
                console.log("Error getPlayerData" + error);
        }
        }else{
            //What if no data ?
        }
    });
} 