using System.Collections;
using System.Collections.Generic;

public class PlayerLog
{
    public bool speedrun;
    public string speedunTime;
    public int quizScore;
    public bool finished;

    public PlayerLog(){
        
    }
    
    public PlayerLog(bool speedrun, string time, int score, bool finish)
    {
        this.speedrun = speedrun;
        this.speedunTime = time;
        this.quizScore = score;
        this.finished = finish;
    }
}
