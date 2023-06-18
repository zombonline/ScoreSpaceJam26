using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard : MonoBehaviour
{
    int leaderBoardID = 15230;
    [SerializeField] RectTransform[] leaderBoardEntries;
    public void SubmitScore(int score)
    {
        StartCoroutine(SubmitScoreCoroutine(score));
    }

    public void FetchHighScores()
    {
        StartCoroutine(FetchTopHighScoresCoroutine());  
    }
    IEnumerator SubmitScoreCoroutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderBoardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done ==false);
    }

    public IEnumerator FetchTopHighScoresCoroutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreListMain(leaderBoardID, 5, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Succesffuly fetched scores");
                for (int i = 0; i < leaderBoardEntries.Length; i++)
                {
                    if(i >= response.items.Length)
                    {
                        return;
                    }
                    if (response.items[i].player.name != "")
                    {
                        leaderBoardEntries[i].Find("Name").GetComponent<TextMeshProUGUI>().text = response.items[i].player.name;
                    }
                    else
                    {
                        leaderBoardEntries[i].Find("Name").GetComponent<TextMeshProUGUI>().text = response.items[i].player.id.ToString();
                    }
                    leaderBoardEntries[i].Find("Score").GetComponent<TextMeshProUGUI>().text = response.items[i].score.ToString();

                }
                done = true;
            }
            else
            {
                Debug.Log("Failed: " + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }


}
