using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarIcon : MonoBehaviour
{
    int id;
    int maxHp;
    int currentHp;
    InGameUI inGameUi;
    List<GameObject> hpBlocks = new();
    private void OnEnable()
    {
        CharacterMain.OnPortalEnterEvent += CharacterMain_OnPortalEnterEvent;
        CharacterHealt.OnDecreaseLife += CharacterHealt_OnDecreaseLife;
        CharacterHealt.OnDeath += CharacterHealt_OnDeath;
    }



    private void OnDisable()
    {
        CharacterMain.OnPortalEnterEvent -= CharacterMain_OnPortalEnterEvent;
        CharacterHealt.OnDecreaseLife -= CharacterHealt_OnDecreaseLife;
        CharacterHealt.OnDeath -= CharacterHealt_OnDeath;
    }
    private void CharacterMain_OnPortalEnterEvent(int id, CharacterData characterData)
    {
        if (this.id == id)
        {
            maxHp = characterData.healt;
            currentHp = maxHp;
            UpdateHp();
        }
    }
    private void CharacterHealt_OnDeath(int id)
    {
        if (this.id == id)
        {
            currentHp = 0;
            maxHp = 0;
            UpdateHp();
        }
    }
    private void CharacterHealt_OnDecreaseLife(int id, float hp)
    {
        if (this.id == id)
        {
            currentHp = (int)hp;
            UpdateHp();
        }
 
    }
    void UpdateHp()
    {
        foreach (GameObject block in hpBlocks)
        {
            Destroy(block);
        }
        int i;
        for (i = 0; i < currentHp; i++)
        {
            GameObject newHp = Instantiate(inGameUi.hpFull, transform);
            newHp.transform.position += Vector3.right * inGameUi.xOffset * i;
            hpBlocks.Add(newHp);
        }
        for (; i < maxHp; i++)
        {
            GameObject newHp = Instantiate(inGameUi.hpEmpty, transform);
            newHp.transform.position += Vector3.right * inGameUi.xOffset * i;
            hpBlocks.Add(newHp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inGameUi = FindAnyObjectByType<InGameUI>();
        id = GetComponentInParent<PlayerBar>().Id;
        maxHp = inGameUi.startHp;
        currentHp = maxHp;
        UpdateHp();
    }
}
