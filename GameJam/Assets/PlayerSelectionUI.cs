using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectionUI : MonoBehaviour
{
    public List<Image> selectionIcons;
    int playerCount;

    public void PlayerJoined()
    {
        if (playerCount == 4)
        {
            SceneManager.LoadScene("MattiaSampleScene", LoadSceneMode.Additive);
            InGameUI inGameUI = FindAnyObjectByType<InGameUI>(FindObjectsInactive.Include);
            inGameUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        playerCount++;
        Image icon = selectionIcons[playerCount - 1];
        Color c = icon.color;
        c.a = 1;
        icon.color = c;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerJoined();
        }
    }
}
