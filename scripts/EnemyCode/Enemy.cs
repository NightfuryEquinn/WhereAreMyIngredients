using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  [ Header( "Setting" ) ]
  [ SerializeField ] private int health = 10;

  // Destroy enemy if collided with player's collider
  private void Collision( Collider2D objectCollided )
  {
    if( objectCollided.GetComponent<StateController>() != null )
    {
      health -= Projectile.ProjectileDamage;

      if( health <= 0 )
      {
        SoundManager.instance.PlaySound( AudioLibrary.instance.EnemyDeathClip );
        Destroy( objectCollided.gameObject );
      }
    }
  }

  private void OnEnable()
  {
    ProjectilePooler.OnProjectileCollision += Collision;
  }

  private void OnDisable()
  {
    ProjectilePooler.OnProjectileCollision -= Collision;
  }
}
