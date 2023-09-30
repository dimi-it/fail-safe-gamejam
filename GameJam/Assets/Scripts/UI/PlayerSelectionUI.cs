using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSelectionUI : MonoBehaviour
{
    public List<Image> selectionIcons;
    public void PlayerJoined()
    {
        Image icon = selectionIcons[GameManager.PlayerCount - 1];
        Color c = icon.color;
        c.a = 1;
        icon.color = c;
    }
}
