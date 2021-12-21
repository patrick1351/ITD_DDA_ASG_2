using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerLog
{
    public bool speedrun;
    public string speedrunTime;
    public int quizScore;
    public bool finished;

    public PlayerLog(){
        
    }
    
    public PlayerLog(bool speedrun, string time, bool finish)
    {
        this.speedrun = speedrun;
        this.speedrunTime = time;
        this.finished = finish;
    }
}
