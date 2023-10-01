using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectionUI : MonoBehaviour
{
    public List<Image> selectionIcons;
    public List<Image> selectionIconsEnabled;
    public void PlayerJoined()
    {
        Image icon = selectionIcons[GameManager.PlayerCount - 1];
        icon.gameObject.SetActive(false);
        selectionIconsEnabled[GameManager.PlayerCount - 1].gameObject.SetActive(true);
        selectionIconsEnabled[GameManager.PlayerCount - 1].SetNativeSize();
    }
}
