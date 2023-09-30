using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    MainMenuUi mainMenuUi;
    PlayerSelectionUI playerSelectionUI;
    PlayerIndicators playerUi;
    private void Start()
    {
        mainMenuUi = FindAnyObjectByType<MainMenuUi>(FindObjectsInactive.Include);
        playerUi = FindAnyObjectByType<PlayerIndicators>(FindObjectsInactive.Include);
        playerSelectionUI = FindAnyObjectByType<PlayerSelectionUI>(FindObjectsInactive.Include);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("PlayerSelection", LoadSceneMode.Additive);
        mainMenuUi.gameObject.SetActive(false);
        playerUi.gameObject.SetActive(true);
        playerSelectionUI.gameObject.SetActive(true);
    }
}
