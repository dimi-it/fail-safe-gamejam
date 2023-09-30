using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    MainMenuUi menuUi;
    InGameUI inGameUi;
    PlayerIndicators playerUi;
    PlayerSelectionUI playerSelectionUI;
    void Start()
    {
        menuUi = FindAnyObjectByType<MainMenuUi>(FindObjectsInactive.Include);
        menuUi.gameObject.SetActive(true);
        inGameUi = FindAnyObjectByType<InGameUI>(FindObjectsInactive.Include);
        inGameUi.gameObject.SetActive(false);
        playerUi = FindAnyObjectByType<PlayerIndicators>(FindObjectsInactive.Include);
        playerUi.gameObject.SetActive(false);
        playerSelectionUI = FindAnyObjectByType<PlayerSelectionUI>(FindObjectsInactive.Include);
        playerSelectionUI.gameObject.SetActive(false);
    }
}
