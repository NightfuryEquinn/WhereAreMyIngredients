using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float speed = 15f;
  [ SerializeField ] private int projectileDamage = 1;
  [ SerializeField ] private float maxRange = 10f;

  // References of spellbook
  public QuickChant QuickChantLearn { get; set; }
  public HeavyChant HeavyChantLearn { get; set; }
  public MeleeChant MeleeChantLearn { get; set; }

  // References of attack
  public BasicAttack BasicAttackLearn { get; set; }
  public ComboAttack ComboAttackLearn { get; set; }
  public HeavyComboAttack HeavyComboAttackLearn { get; set; }

  // Returns the shoot direction
  public Vector3 ShootDirection => _shootDirection;

  // Controls the speed of this projectile
  public float Speed { get; set; }

  // Controls the damage of this projectile
  public static int ProjectileDamage { get; set; }

  private Vector3 _shootDirection;
  private Vector3 _firePosition;

  private void Awake()
  {
    Speed = speed;
    ProjectileDamage = projectileDamage;
  }

  private void Start()
  {
    if( QuickChantLearn != null )
    {
      if( !QuickChantLearn.QuickChantController.PlayerControls.FacingRight )
      {
        _shootDirection = Vector3.left;

        transform.Rotate( Vector3.up, 180f );

        _shootDirection = - _shootDirection;
      }
      else
      {
        _shootDirection = Vector3.right;
      }
    }
    
    if( HeavyChantLearn != null )
    {
      if( !HeavyChantLearn.HeavyChantController.PlayerControls.FacingRight )
      {
        _shootDirection = Vector3.left;

        transform.Rotate( Vector3.up, 180f );

        _shootDirection = - _shootDirection;
      }
      else
      {
        _shootDirection = Vector3.right;
      }
    }

    if( MeleeChantLearn != null )
    {
      if( !MeleeChantLearn.MeleeChantController.PlayerControls.FacingRight )
      {
        _shootDirection = Vector3.left;

        transform.Rotate( Vector3.up, 180f );

        _shootDirection = - _shootDirection;
      }
      else
      {
        _shootDirection = Vector3.right;
      }
    }

    if (BasicAttackLearn != null)
    {
      if (!BasicAttackLearn.BasicAttackController.PlayerControls.FacingRight)
      {
        _shootDirection = Vector3.left;

        transform.Rotate(Vector3.up, 180f);

        _shootDirection = -_shootDirection;
      }
      else
      {
        _shootDirection = Vector3.right;
      }
    }

    if (ComboAttackLearn != null)
    {
      if (!ComboAttackLearn.ComboAttackController.PlayerControls.FacingRight)
      {
        _shootDirection = Vector3.left;

        transform.Rotate(Vector3.up, 180f);

        _shootDirection = -_shootDirection;
      }
      else
      {
        _shootDirection = Vector3.right;
      }
    }

    if (HeavyComboAttackLearn != null)
    {
      if (!HeavyComboAttackLearn.HeavyComboAttackController.PlayerControls.FacingRight)
      {
        _shootDirection = Vector3.left;

        transform.Rotate(Vector3.up, 180f);

        _shootDirection = -_shootDirection;
      }
      else
      {
        _shootDirection = Vector3.right;
      }
    }

    _firePosition = transform.position;
  }

  private void Update()
  {
    if ( Vector3.Distance( _firePosition, transform.position ) > maxRange )
    {
      this.gameObject.SetActive( false );
    }

    transform.Translate( _shootDirection * Speed * Time.deltaTime );
  }

  // Set the projectile direction
  public void SetDirection( Vector3 newDirection )
  {
    _shootDirection = newDirection;
  }

  // Enable projectile speed and damage
  public void EnableProjectile()
  {
    Speed = speed;
    ProjectileDamage = projectileDamage;
  }

  // Disable projectile speed and damage
  public void DisableProjectile()
  {
    Speed = 0f;
    ProjectileDamage = 0;
  }
}
