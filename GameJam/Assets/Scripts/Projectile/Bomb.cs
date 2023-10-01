using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeRange;
    [SerializeField] private float _colliderRadius;
    private ProjectileMain _projectileMain;
    private GameObject _owner;
    private Rigidbody _rigidbody;
    private SphereCollider _sphereCollider;
    
    private void Start()
    {
        _projectileMain = this.GetComponent<ProjectileMain>();
        _owner = _projectileMain.Owner;
        _rigidbody = this.GetComponent<Rigidbody>();
        _sphereCollider = this.GetComponent<SphereCollider>();
        _rigidbody.velocity = _speed * (this.transform.forward + new Vector3(0, 0.8f, 0).normalized);
        Invoke(nameof(OnTimerElapsed), _timeRange);
        _sphereCollider.isTrigger = false;
    }
    
    // private void OnTriggerExit(Collider other)
    // {
    //     this.gameObject.GetComponent<Collider>().isTrigger = false;
    // }

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.GetInstanceID() == _owner.GetInstanceID())
        {
            return;
        }
        Destroy(_rigidbody);
    }

    private void OnTimerElapsed()
    {
        Debug.Log("EXPLODEEEEE");
        _sphereCollider.isTrigger = true;
        _sphereCollider.radius = _colliderRadius;
        Invoke(nameof(OnDestroyAfterTime), 0.03f);
    }

    private void OnDestroyAfterTime()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        Invoke(nameof(OnExploded), 1.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (other.gameObject.GetInstanceID() == _owner.GetInstanceID())
            return;
        other.gameObject.GetComponent<CharacterHealt>().
            DecreaseLife(_projectileMain.Damage);
    }

    private void OnExploded() 
    {
        Destroy(this.gameObject);
    }
}
