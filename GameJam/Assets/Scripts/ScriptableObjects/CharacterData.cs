using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Custom/CharacterData")]
public class CharacterData : ScriptableObject
{
    public int health;
    public float speed;
    public int range;
    public float rateOfFire;
    public int damagePerBullet;
    public GameObject projectile;
    public Vector3 projectileSpawnPoint;
    public Sprite roleLogo;
    public GameObject characterMesh;
    public string tag;
    public Sprite sprWin;
}
