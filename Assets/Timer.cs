using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float startSeconds;
    [SerializeField] TextMeshProUGUI timerText;
    float time, mins, secs;

    private void Awake()
    {
        time = startSeconds;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        mins = Mathf.FloorToInt(time / 60);
        secs = Mathf.FloorToInt(time % 60);
        timerText.text = mins.ToString("00") + ":" + secs.ToString("00");
    }
}
