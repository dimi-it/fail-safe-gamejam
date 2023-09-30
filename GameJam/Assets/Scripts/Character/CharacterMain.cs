using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMain : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    private CharacterMovement _characterMovement;
    public CharacterData CharacterData => _characterData;

    public void Start()
    {
        _characterMovement = this.GetComponent<CharacterMovement>();
    }

    public void OnPortalEnter(Portal exitPortal)
    {
        _characterData = exitPortal.CharacterModifier;
        _characterMovement.OnPortalEnter(exitPortal);
    }
    
    public void OnPortalExit()
    {
        _characterMovement.OnPortalExit();
    }
}
