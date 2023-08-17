using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChant : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private Projectile projectilePrefab;
  [ SerializeField ] private Transform firePoint;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float msBetweenShots = 500f;

  public MeleeChantController MeleeChantController { get; set; }

  public ChantCooldown ChantCooldown { get; set; }

  private float nextShotTime;

  public void Shoot()
  {
    if( Time.time > nextShotTime )
    {
      nextShotTime = Time.time + msBetweenShots / 1000f;
      Projectile projectile = Instantiate( projectilePrefab, firePoint.position, Quaternion.identity );
      projectile.MeleeChantLearn = this;

      SoundManager.instance.PlaySound( AudioLibrary.instance.MeleeChantClip );
    }
  }
}
