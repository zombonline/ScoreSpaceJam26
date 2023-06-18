using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    [SerializeField] TextMeshProUGUI scoreText;

    public void UpdateScore(int val)
    {
        score += val;
        scoreText.text = score.ToString("00");
    }

}
