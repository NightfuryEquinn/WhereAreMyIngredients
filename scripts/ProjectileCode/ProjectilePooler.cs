using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
  // Event called when colliding
  public static Action<Collider2D> OnProjectileCollision;

  [ Header( "Settings" ) ]
  [ SerializeField ] private LayerMask collideWith;

  private Projectile _projectile;

  private void Start()
  {
    _projectile = GetComponent<Projectile>();
  }

  private void Update()
  {
    CheckCollision();
  }

  // Checks for collision in order to call something
  private void CheckCollision()
  {
    RaycastHit2D hit = Physics2D.Raycast( transform.position, _projectile.ShootDirection, _projectile.Speed * Time.deltaTime + 0.2f, collideWith );

    if( hit )
    {
      SoundManager.instance.PlaySound( AudioLibrary.instance.ProjectileCollisionClip );
      
      OnProjectileCollision?.Invoke( hit.collider );
      _projectile.DisableProjectile();
      gameObject.SetActive( false );
    }
  }
}
