using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;

    public CharacterData CharacterData => _characterData;
}
