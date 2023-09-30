using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterShooting : MonoBehaviour
{
    private CharacterMain _characterMain;
    private int _rateOfFire;
    private int _damage;
    private float _damagePerBullet;
    private GameObject _projectile;
    private Vector3 _projectileSpawnPoint;
    private float _timer;
    private float _timerEnd;
    private void Start()
    {
        _characterMain = this.GetComponent<CharacterMain>();
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
        _projectileSpawnPoint = _characterMain.CharacterData.projectileSpawnPoint;
        _timerEnd = 1 / (float)_rateOfFire;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed || _rateOfFire == 0 || _timer < _timerEnd)
        {
            return;
        }
        _timer = 0;
        Debug.Log("SHOOT");
        GameObject projectileMain = Instantiate(
            _projectile, 
            transform.position + _projectileSpawnPoint, 
            transform.GetChild(0).transform.rotation
            );
        projectileMain.GetComponent<ProjectileMain>().SetProjectile(_damagePerBullet, this.gameObject);
    }
}
