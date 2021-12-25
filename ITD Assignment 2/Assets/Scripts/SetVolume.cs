using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixerBGM;
    public AudioMixer mixerEffects;

    public void SetLevelBGM(float sliderValue)
    {
        mixerBGM.SetFloat("BGM Music Vol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelEffects(float sliderValue)
    {
        mixerEffects.SetFloat("Effects Music Vol", Mathf.Log10(sliderValue) * 20);
    }
}
