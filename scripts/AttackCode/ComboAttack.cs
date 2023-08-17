using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private Projectile projectilePrefab;
  [ SerializeField ] private Transform firePoint;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float msBetweenShots = 1f;

  public ComboAttackController ComboAttackController { get; set; }

  public void Shoot()
  {
    Projectile projectile = Instantiate( projectilePrefab, firePoint.position, Quaternion.identity );
    projectile.ComboAttackLearn = this;
  }
}
