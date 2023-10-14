using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarIcon : MonoBehaviour
{
    public bool Invert;
    int id;
    public int maxHp;
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
            currentHp = characterData.health;
            UpdateHp();
        }
    }
    private void CharacterHealt_OnDeath(int id)
    {
        if (this.id == id)
        {
            currentHp = 0;
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
            newHp.transform.position += Vector3.right * inGameUi.xOffset * (Invert? -i : i);
            if (Invert)
            {
                newHp.GetComponent<RectTransform>().localScale = (new Vector3(-1, 1, 1));
            }
            hpBlocks.Add(newHp);
        }
        for (; i < maxHp; i++)
        {
            GameObject newHp = Instantiate(inGameUi.hpEmpty, transform);
            newHp.transform.position += Vector3.right * inGameUi.xOffset * (Invert ? -i : i);
            if (Invert)
            {
                newHp.GetComponent<RectTransform>().localScale = (new Vector3(-1, 1, 1));
            }
            hpBlocks.Add(newHp);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        inGameUi = FindAnyObjectByType<InGameUI>();
        id = GetComponentInParent<PlayerBar>().Id;
        currentHp = inGameUi.startHp;
        UpdateHp();
    }
}
