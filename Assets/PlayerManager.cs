using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using Unity.VisualScripting;
public class PlayerManager : MonoBehaviour
{
    Leaderboard leaderboard;
    private void Awake()
    {
        leaderboard= GetComponent<Leaderboard>();
    }
    private void Start()
    {
        StartCoroutine(SetupRoutine());
    }
    public void SetPlayerName(System.String value)
    {
        LootLockerSDKManager.SetPlayerName(value, (response) =>
        {
            if (response.success)
            {
                PlayerPrefs.SetString("playerName", value);
                Debug.Log("Succesfully set player name to " + value);

            }
            else
            {
                Debug.Log("Failed to set player name: " + response.Error);
            }
        });
    }
    IEnumerator SetupRoutine()
    {
        yield return LogInCoroutine();
        yield return leaderboard.FetchTopHighScoresCoroutine();
    }

    IEnumerator LogInCoroutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {

            if (response.success)
            {
                Debug.Log("Player logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session.");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
