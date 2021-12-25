// https://firebase.google.com/docs/web/setup#available-libraries
//imported libraries 
import { initializeApp } from "https://www.gstatic.com/firebasejs/9.6.1/firebase-app.js";
import { getAuth, initializeAuth, createUserWithEmailAndPassword, signInWithEmailAndPassword} from "https://www.gstatic.com/firebasejs/9.5.0/firebase-auth.js";
import { getDatabase, ref, child, get, set, onValue, orderByChild } from "https://www.gstatic.com/firebasejs/9.5.0/firebase-database.js";

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