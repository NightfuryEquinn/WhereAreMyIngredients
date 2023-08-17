using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyComboAttack : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private Projectile projectilePrefab;
  [ SerializeField ] private Transform firePoint;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float msBetweenShots = 1f;

  public HeavyComboAttackController HeavyComboAttackController { get; set; }

  public void Shoot()
  {
    Projectile projectile = Instantiate( projectilePrefab, firePoint.position, Quaternion.identity );
    projectile.HeavyComboAttackLearn = this;
  }
}
