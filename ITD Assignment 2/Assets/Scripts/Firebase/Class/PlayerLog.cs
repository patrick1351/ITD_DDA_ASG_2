using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerLog
{
    public bool speedrun;
    public string speedrunTime;
    public int speedrunSeconds;
    public bool finished;

    public PlayerLog(){
        
    }
    
    public PlayerLog(bool speedrun, string time, int seconds, bool finish)
    {
        this.speedrun = speedrun;
        this.speedrunTime = time;
        this.finished = finish;
        this.speedrunSeconds = seconds;
    }
}
