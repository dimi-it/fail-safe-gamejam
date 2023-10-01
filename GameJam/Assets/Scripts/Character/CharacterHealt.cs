using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealt : MonoBehaviour
{
    [SerializeField] private float _healt;
    private CharacterMain _characterMain;
    public static event Action<int, float> OnDecreaseLife;
    public static event Action<int> OnDeath;

    private void Start()
    {
        _characterMain = this.GetComponent<CharacterMain>();
        _healt = _characterMain.CharacterData.health;
    }

    public void DecreaseLife(float damage)
    {
        Debug.Log($"{damage}");
        _healt -= damage;
        if (_healt < 0)
        {
            _healt = 0;
        }
        if (_healt == 0)
        {
            OnDeath?.Invoke(_characterMain.ID);
            gameObject.SetActive(false);
        }
        else
        {
            OnDecreaseLife?.Invoke(_characterMain.ID, _healt);
        }
    }

    public void OnPortalEnter()
    {
        _healt = _characterMain.CharacterData.health;
    }
    public void Kill()
    {
        OnDeath?.Invoke(_characterMain.ID);
        gameObject.SetActive(false);
    }
}
