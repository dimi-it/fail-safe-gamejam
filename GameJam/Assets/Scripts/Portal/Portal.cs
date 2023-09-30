using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private bool _isA;
    [SerializeField] private CharacterData _characterModifier;
    public int Id => _id;
    public bool IsA => _isA;
    public CharacterData CharacterModifier => _characterModifier;
    
    private PortalManager _portalManager;

    private void Start()
    {
        _portalManager = FindObjectOfType<PortalManager>();
    }

    private void OnTriggerEnter(Collider characterCollider)
    {
        Debug.Log($"Enter {_isA.ToString()}");
        characterCollider.gameObject.GetComponent<CharacterMain>()
            .OnPortalEnter(_portalManager.GetOther(this));
    }

    private void OnTriggerExit(Collider characterCollider)
    {
        Debug.Log($"Exit {_isA.ToString()}");
        characterCollider.gameObject.GetComponent<CharacterMain>()
            .OnPortalExit();
    }
}