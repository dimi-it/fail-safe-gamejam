using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMecha : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeRange;
    private ProjectileMain _projectileMain;
    private GameObject _owner;
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _projectileMain = this.GetComponent<ProjectileMain>();
        _owner = _projectileMain.Owner;
        _rigidbody = this.GetComponent<Rigidbody>();
        _rigidbody.velocity = _speed * this.transform.forward;
        Invoke(nameof(OnTimerElapsed), _timeRange);
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Collider>().isTrigger = false;
    }
    */

    private void OnCollisionEnter(Collision other)
    {
        GameObject otherObj = other.gameObject;
        if (otherObj.GetInstanceID() == _owner.GetInstanceID())
        {
            return;
        }
        if (otherObj.CompareTag("Player"))
        {
            CollideOnPlayer(otherObj);    
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void CollideOnPlayer(GameObject player)
    {
        player.GetComponent<CharacterHealt>().DecreaseLife(_projectileMain.Damage);
        Destroy(this.gameObject);
    }
    
    private void OnTimerElapsed()
    {
        Destroy(this.gameObject);
    }
}
