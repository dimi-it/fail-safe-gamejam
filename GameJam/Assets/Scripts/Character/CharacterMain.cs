using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMain : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [FormerlySerializedAs("damage")] [SerializeField] private int _damage;
    private CharacterMovement _characterMovement;
    private CharacterShooting _characterShooting;
    public CharacterData CharacterData => _characterData;
    public int Damage => _damage;
    public void Start()
    {
        _characterMovement = this.GetComponent<CharacterMovement>();
        _characterShooting = this.GetComponent<CharacterShooting>();
    }

    public void OnPortalEnter(Portal exitPortal)
    {
        _characterData = exitPortal.CharacterModifier;
        _characterMovement.OnPortalEnter(exitPortal);
        _characterShooting.OnPortalEnter();
        if (_characterData.rateOfFire > 0)
        {
            _characterShooting.enabled = true;
        }
        else if (_characterData.rateOfFire == 0)
        {
            _characterShooting.enabled = false;
        }
    }
    
    public void OnPortalExit()
    {
        _characterMovement.OnPortalExit();
    }
}
