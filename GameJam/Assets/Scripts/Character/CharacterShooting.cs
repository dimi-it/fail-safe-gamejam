using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterShooting : MonoBehaviour
{
    private CharacterMain _characterMain;
    private float _rateOfFire;
    private int _damage;
    private float _damagePerBullet;
    private GameObject _projectile;
    private GameObject _projectileSpawnPoint;
    private float _timer;
    private float _timerEnd;
    Animator anim;
    private void Start()
    {
        _characterMain = this.GetComponent<CharacterMain>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void OnPortalEnter()
    {
        _rateOfFire = _characterMain.CharacterData.rateOfFire;
        _damagePerBullet = _characterMain.CharacterData.damagePerBullet;
        _projectile = _characterMain.CharacterData.projectile;
        //_projectileSpawnPoint = GetComponentInChildren<BulletSpawn>().gameObject;
        _timerEnd = 1 / (float)_rateOfFire;
        anim = GetComponentInChildren<Animator>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed || _rateOfFire == 0 || _timer < _timerEnd)
        {
            return;
        }
        anim.SetTrigger("Shoot");
        _timer = 0;
        Debug.Log("SHOOT");
        //Debug.Log(_projectileSpawnPoint);
        GameObject projectileMain = Instantiate(
            _projectile,
            GetComponentInChildren<BulletSpawn>().transform.position, 
            transform.GetChild(0).transform.rotation
            );
        projectileMain.GetComponent<ProjectileMain>().SetProjectile(_damagePerBullet, this.gameObject);
    }
}
