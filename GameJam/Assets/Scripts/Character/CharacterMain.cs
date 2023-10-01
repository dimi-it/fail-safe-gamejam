using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMain : MonoBehaviour
{
    public int ID;
    public static event Action<int, CharacterData> OnPortalEnterEvent;
    [SerializeField] private CharacterData _characterData;
    private CharacterData _oldCharacterData;
    private CharacterMovement _characterMovement;
    private CharacterShooting _characterShooting;
    private CharacterHealt _characterHealth;
    public CharacterData CharacterData => _characterData;
    public void Start()
    {
        _characterMovement = this.GetComponent<CharacterMovement>();
        _characterShooting = this.GetComponent<CharacterShooting>();
        _characterHealth = this.GetComponent<CharacterHealt>();
        _oldCharacterData = _characterData;
    }

    public void OnPortalEnter(Portal exitPortal)
    {
        _characterData = exitPortal.CharacterModifier;
        if (!_characterData.tag.Equals(_oldCharacterData.tag))
        {
            foreach (Transform childTransform in transform.GetComponentsInChildren<Transform>(true))
            {
                if (childTransform.gameObject.CompareTag(_characterData.tag))
                {
                    childTransform.gameObject.SetActive(true);
                }
                else if (childTransform.gameObject.CompareTag(_oldCharacterData.tag))
                {
                    childTransform.gameObject.SetActive(false);
                }
            }
        }
        _characterMovement.OnPortalEnter(exitPortal);
        _characterShooting.OnPortalEnter();
        if (!_characterData.tag.Equals(_oldCharacterData.tag))
        {
            _characterHealth.OnPortalEnter();
            OnPortalEnterEvent?.Invoke(ID, _characterData);
        }

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

        _oldCharacterData = _characterData;
    }
}
