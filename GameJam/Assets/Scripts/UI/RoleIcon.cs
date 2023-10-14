using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoleIcon : MonoBehaviour
{
    int id;
    Image image;
    InGameUI inGameUI;
    private void OnEnable()
    {
        CharacterMain.OnPortalEnterEvent += CharacterMain_OnPortalEnterEvent;
        CharacterHealt.OnDeath += CharacterHealt_OnDeath;
    }

    private void OnDisable()
    {
        CharacterMain.OnPortalEnterEvent -= CharacterMain_OnPortalEnterEvent;
        CharacterHealt.OnDeath -= CharacterHealt_OnDeath;
    }
    private void CharacterMain_OnPortalEnterEvent(int id, CharacterData characterData)
    {
        if (this.id == id)
        {
            image.sprite = characterData.roleLogo;
        }
    }
    private void CharacterHealt_OnDeath(int id)
    {
        if (this.id == id)
        {
            image.sprite = inGameUI.deathSprite;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        id = GetComponentInParent<PlayerBar>().Id;
        inGameUI = FindAnyObjectByType<InGameUI>();
        image.sprite = inGameUI.startSprite;
    }
}
