using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ConfigTank", order = 1)]

public class ConfigTankSO : ScriptableObject
{
    [Header("Tank")]
    public float attackSpeed;
    public float bulletSpeed;

    [Header("Bullet")]
    public float maxDamage = 100f;
    public float maxLifeTime = 2f;
    public float explosionRadius = 5f; 
}
