using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrefsController : MonoBehaviour
{
    public const string SFX_VOL = "sfx volume";

    public const string MUS_VOL = "music volume";

    public readonly string[] HIGH_SCORES = { "h1", "h2", "h3", "h4", "h5" };
    public readonly string[] HIGH_SCORE_NAMES = { "n1", "n2", "n3", "n4", "n5" };

    public void SetSFXVolume(float val)
    {
        if(val < 0) { val = 0; }
        if(val > 1) { val = 1; }
        PlayerPrefs.SetFloat(SFX_VOL, val);
        
    }
    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOL, 0);
    }
    public void SetMusicVolume(float val)
    {
        if (val < 0) { val = 0; }
        if (val > 1) { val = 1; }
        PlayerPrefs.SetFloat(MUS_VOL, val);
    }
    public float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUS_VOL, 1);
    }
    public void SetHighScore(int index, int scoreVal, string nameVal)
    {
        //for loop to shuffle highscores down the list
        for(int i = HIGH_SCORES.Length-1 ;i >= index ;i--)
        {
            if ((i + 1) < HIGH_SCORES.Length)
            {
                PlayerPrefs.SetInt(HIGH_SCORES[i + 1], PlayerPrefs.GetInt(HIGH_SCORES[i]));
                PlayerPrefs.SetString(HIGH_SCORE_NAMES[i+1], PlayerPrefs.GetString(HIGH_SCORE_NAMES[i]));
            }
        }
        //assign new high score
        PlayerPrefs.SetInt(HIGH_SCORES[index], scoreVal);
        PlayerPrefs.SetString(HIGH_SCORE_NAMES[index], name);
    }
    public int GetHighScore(int index)
    {
        return PlayerPrefs.GetInt(HIGH_SCORES[index], 0);
    }
}
