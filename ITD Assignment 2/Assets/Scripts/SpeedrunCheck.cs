using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedrunCheck : MonoBehaviour
{
    public bool speedunTime;

    private static SpeedrunCheck speedrun;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (speedrun == null)
        {
            speedrun = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
