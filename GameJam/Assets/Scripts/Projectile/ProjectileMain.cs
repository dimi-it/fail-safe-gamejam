using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMain : MonoBehaviour
{
    public float Damage { get; private set; }
    public GameObject Owner { get; private set; }

    public void SetProjectile(float damage, GameObject owner)
    {
        Damage = damage;
        Owner = owner;
    }
}
