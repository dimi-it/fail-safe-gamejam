using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeRange;
    private float _timer;
    private ProjectileMain _projectileMain;
    private GameObject _owner;

    private void Start()
    {
        _projectileMain = this.GetComponent<ProjectileMain>();
        _owner = _projectileMain.Owner;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _timeRange)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 movement = Vector3.forward * (_speed * Time.deltaTime);
        transform.Translate(movement);
    }

    public void OnTriggerEnter(Collider other)
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
        else if(otherObj.CompareTag(StaticVars.WallTag))
        {
             CollideOnWall(otherObj);
        }
    }

    public void CollideOnPlayer(GameObject player)
    {
        player.GetComponent<CharacterHealt>().DecreaseLife(_projectileMain.Damage);
        Destroy(this.gameObject);
    }
    
    private void CollideOnWall(GameObject otherObj)
    {
        throw new NotImplementedException();
    }
}
