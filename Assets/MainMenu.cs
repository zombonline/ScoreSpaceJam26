using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI musVolDisplayText, sfxVolDisplayText;
    [SerializeField] TMP_InputField playerNameInputField;

    PlayerPrefsController playerPrefsController;

    [SerializeField] RectTransform[] leaderBoardEntries;

    Leaderboard leaderboard;

    [SerializeField] RectTransform offScreen, onScreen;
    [SerializeField] float moveTime = .5f;
    bool movingOut = true;

    private void Awake()
    {
        playerPrefsController = GetComponent<PlayerPrefsController>();
        UpdateVolumeDisplay();
        UpdatePlayerName();
    }
    public void UpdateVolumeDisplay()
    {
        musVolDisplayText.text = Mathf.RoundToInt(playerPrefsController.GetMusicVolume() * 10).ToString();
        sfxVolDisplayText.text = Mathf.RoundToInt(playerPrefsController.GetSFXVolume()*10).ToString();
    }
    public void UpdateSFXVolume(float val)
    {
        playerPrefsController.SetSFXVolume(playerPrefsController.GetSFXVolume() + val);
    }
    public void UpdateMusicVolume(float val)
    {
        playerPrefsController.SetMusicVolume(playerPrefsController.GetMusicVolume() + val);
    }
    public void UpdatePlayerName()
    {
        playerNameInputField.text = PlayerPrefs.GetString("playerName");
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void ToggleMovingOutBool()
    {
        movingOut = false;
    }

    public void MoveOut(RectTransform objectToMove)
    {
        movingOut= true;
        LeanTween.moveLocal(objectToMove.gameObject, offScreen.anchoredPosition, moveTime);
        Invoke(nameof(ToggleMovingOutBool), moveTime);
        
    }
    public void MoveIn(RectTransform objectToMove)
    {
        StartCoroutine(MoveInCoroutine(objectToMove));
    }

    IEnumerator MoveInCoroutine(RectTransform objectToMove)
    {
        if(movingOut)
        {
            yield return new WaitForSeconds(moveTime);
            LeanTween.moveLocal(objectToMove.gameObject, onScreen.anchoredPosition, moveTime);
        }
        else
        {
            yield return new WaitForSeconds(0f);
            LeanTween.moveLocal(objectToMove.gameObject, onScreen.anchoredPosition, moveTime);
        }

    }
}
