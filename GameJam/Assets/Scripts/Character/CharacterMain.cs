using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMain : MonoBehaviour
{
    public int ID;
    public static event Action<int, CharacterData> OnPortalEnterEvent;
    [SerializeField] private CharacterData _characterData;
    private CharacterMovement _characterMovement;
    private CharacterShooting _characterShooting;
    private CharacterHealt _characterHealt;
    public CharacterData CharacterData => _characterData;
    public void Start()
    {
        _characterMovement = this.GetComponent<CharacterMovement>();
        _characterShooting = this.GetComponent<CharacterShooting>();
        _characterHealt = this.GetComponent<CharacterHealt>();

    }

    public void OnPortalEnter(Portal exitPortal)
    {
        _characterData = exitPortal.CharacterModifier;
        _characterMovement.OnPortalEnter(exitPortal);
        _characterShooting.OnPortalEnter();
        _characterHealt.OnPortalEnter();
        if (_characterData.rateOfFire > 0)
        {
            _characterShooting.enabled = true;
        }
        else if (_characterData.rateOfFire == 0)
        {
            _characterShooting.enabled = false;
        }
        OnPortalEnterEvent?.Invoke(ID, _characterData);
       
    }
    
    public void OnPortalExit()
    {
        _characterMovement.OnPortalExit();
    }
}
