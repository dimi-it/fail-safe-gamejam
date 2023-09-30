using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerCount;
    PlayerSelectionUI selectionUI;
    public void PlayerJoined() 
    {
        PlayerCount++;

    }
}
