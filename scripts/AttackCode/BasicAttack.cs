using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private Projectile projectilePrefab;
  [ SerializeField ] private Transform firePoint;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float msBetweenShots = 1f;

  public BasicAttackController BasicAttackController { get; set; }

  public void Shoot()
  {
    Projectile projectile = Instantiate( projectilePrefab, firePoint.position, Quaternion.identity );
    projectile.BasicAttackLearn = this;
  }
}
