﻿using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New projectile")]
public class ProjectileId : ScriptableObject
{
    [SerializeField] private string projectileId;
    [SerializeField] private float fireRate;

    public string Value => projectileId;
    public float FireRate => fireRate;
}