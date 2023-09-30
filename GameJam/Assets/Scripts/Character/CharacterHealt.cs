using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealt : MonoBehaviour
{
    [SerializeField] private float _healt;
    private CharacterMain _characterMain;

    private void Start()
    {
        _characterMain = this.GetComponent<CharacterMain>();
        _healt = _characterMain.CharacterData.healt;
    }

    public void DecreaseLife(float damage)
    {
        Debug.Log($"{damage}");
        _healt -= damage;
    }

}
