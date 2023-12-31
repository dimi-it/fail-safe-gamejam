using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealt : MonoBehaviour
{
    [SerializeField] private float _healt;
    [SerializeField] private float _deathDuration = 2.5f;
    private CharacterMain _characterMain;
    public static event Action<int, float> OnDecreaseLife;
    public static event Action<int> OnDeath;
    private Animator anim;
    private void Start()
    {
        _characterMain = this.GetComponent<CharacterMain>();
        _healt = _characterMain.CharacterData.health;
        anim = GetComponentInChildren<Animator>();

    }

    public void DecreaseLife(float damage)
    {
        Debug.Log($"{damage}");
        _healt -= damage;
        anim.SetTrigger("Hit");
        if (_healt < 0)
        {
            _healt = 0;
        }
        if (_healt == 0)
        {
            anim.SetTrigger("Death");
            Invoke(nameof(Kill), _deathDuration);
        }
        else
        {
            OnDecreaseLife?.Invoke(_characterMain.ID, _healt);
        }
    }

    public void OnPortalEnter()
    {
        _healt = _characterMain.CharacterData.health;
        anim = GetComponentInChildren<Animator>();

    }
    public void Kill()
    {
        OnDeath?.Invoke(_characterMain.ID);
        gameObject.SetActive(false);
    }
}
