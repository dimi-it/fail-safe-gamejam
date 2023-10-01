using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    public List<Sprite> winImages;
    public Image winRole;
    public Image winImage;
    private void OnEnable()
    {
        GameManager.OnWin += GameManager_OnWin;
    }
    private void OnDisable()
    {
        GameManager.OnWin -= GameManager_OnWin;
    }
    private void GameManager_OnWin(CharacterData obj, int id)
    {
        winRole.sprite = obj.sprWin;
        winImage.sprite = winImages[id];
    }
}
