using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Custom/CharacterData")]
public class CharacterData : ScriptableObject
{
    public int health;
    public int speed;
    public int range;
    public int rateOfFire;
    public int damagePerBullet;
    public GameObject projectile;
    public Vector3 projectileSpawnPoint;
    public Sprite roleLogo;
    public GameObject characterMesh;
    public string tag;
}
