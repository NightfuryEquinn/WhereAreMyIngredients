using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
  private void Collision( Collider2D objectCollided )
  {
    if( objectCollided.GetComponent<PlayerHealth>() != null )
    {
      int damage = Projectile.ProjectileDamage;
      objectCollided.GetComponent<PlayerHealth>().LoseLife( damage );
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
