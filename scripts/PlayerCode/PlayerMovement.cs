using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : PlayerStates
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float speed = 12.5f;
  [ SerializeField ] private float walkSFXInterval = 1f;
  
  [ Header( "Damage from Special Floor to Player Interval" ) ]
  [ SerializeField ] private float mitigateDamageInterval = 2f;

  public float Speed { get; set; }
  public float InitialSpeed => speed;

  private float _falseTime = 0f;
  private float _walkFalseTime = 0f;

  private float _horizontalMovement;
  private float _movement;

  private int _idleAnimatorParam = Animator.StringToHash( "Idle" );
  private int _runAnimatorParam = Animator.StringToHash( "Run" );

  protected override void InitState()
  {
    base.InitState();
    Speed = speed;
  }

  public override void ExecuteState()
  {
    MovePlayer();

    _falseTime += Time.deltaTime;
    while( _falseTime >= mitigateDamageInterval )
    {
      _playerHealth.LoseLife( _playerControls.MitigateDamage );
      _falseTime -= mitigateDamageInterval; 
    }
  }

  // Move player
  private void MovePlayer()
  {
    if( Mathf.Abs( _horizontalMovement ) > 0.1f )
    {
      _movement = _horizontalMovement;

      if( !_playerControls.Conditions.isJumping )
      {
        _walkFalseTime += Time.deltaTime;
        while( _walkFalseTime >= walkSFXInterval )
        {
          SoundManager.instance.PlaySound( AudioLibrary.instance.WalkClip );
          _walkFalseTime -= walkSFXInterval;
        }
      }
    }
    else
    {
      _movement = 0f;
    }

    float moveSpeed = _movement * Speed;
    moveSpeed = EvaluateFriction( moveSpeed );
    moveSpeed = EvaluateReducedSpeed( moveSpeed );

    _playerControls.SetHorizontalForce( moveSpeed );
  }

  // Initialize internal movement direction
  protected override void GetInput()
  {
    _horizontalMovement = _horizontalInput;
  }

  public override void SetAnimation()
  {
    _animator.SetBool( _idleAnimatorParam, _horizontalMovement == 0 && _playerControls.Conditions.isCollidingBelow );
    _animator.SetBool( _runAnimatorParam, Mathf.Abs( _horizontalInput ) > 0.1f && _playerControls.Conditions.isCollidingBelow );
  }

  private float EvaluateFriction( float moveSpeed )
  {
    if( _playerControls.Friction > 0 )
    {
      moveSpeed = Mathf.Lerp( _playerControls.Force.x, moveSpeed, Time.deltaTime * 10f * _playerControls.Friction );
    }

    return moveSpeed;
  }

  private float EvaluateReducedSpeed( float moveSpeed )
  {
    if( _playerControls.ReducedSpeed > 0 )
    {
      moveSpeed *= _playerControls.ReducedSpeed;
    }

    return moveSpeed;
  }
}
