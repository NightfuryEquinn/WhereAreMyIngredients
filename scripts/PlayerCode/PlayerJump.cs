using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerJump : PlayerStates
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float jumpHeight = 4f;
  [ SerializeField ] private int maxJumps = 2;

  public float JumpHeight { get; set; }
  public float InitialJumpHeight => jumpHeight;

  // Return how many jumps left
  public int jumpsLeft { get; set; }

  private int _jumpAnimatorParam = Animator.StringToHash( "Jump" );
  private int _dJumpAnimatorParam = Animator.StringToHash( "DoubleJump" );
  private int _fallAnimatorParam = Animator.StringToHash( "Fall" );

  protected override void InitState()
  {
    base.InitState();
    jumpsLeft = maxJumps;
    JumpHeight = jumpHeight;
  }

  public override void ExecuteState()
  {
    if( _playerControls.Conditions.isCollidingBelow && _playerControls.Force.y == 0f )
    {
      jumpsLeft = maxJumps;
      _playerControls.Conditions.isJumping = false;
    }
  }

  protected override void GetInput()
  {
    if( Input.GetKeyDown( KeyCode.W ) || Input.GetKeyDown( KeyCode.Space ) )
    {
      Jump();
    }
  }

  private void Jump()
  {
    if( !canJump() )
    {
      return;
    }

    if( jumpsLeft == 0 )
    {
      return;
    }

    jumpsLeft -= 1;

    float jumpForce = Mathf.Sqrt( JumpHeight * 2f * Mathf.Abs( _playerControls.Gravity ) );
    _playerControls.SetVerticalForce( jumpForce );

    _playerControls.Conditions.isJumping = true;

    SoundManager.instance.PlaySound( AudioLibrary.instance.JumpClip );
  }

  private bool canJump()
  {
    if( !_playerControls.Conditions.isCollidingBelow && jumpsLeft <= 0 )
    {
      return false;
    }

    if( _playerControls.Conditions.isCollidingBelow && jumpsLeft <= 0 )
    {
      return false;
    }

    return true;
  }

  public override void SetAnimation()
  {
    // Jump
    _animator.SetBool( _jumpAnimatorParam, _playerControls.Conditions.isJumping && !_playerControls.Conditions.isCollidingBelow && jumpsLeft > 0 && !_playerControls.Conditions.isFalling );

    // Double Jump
    _animator.SetBool( _dJumpAnimatorParam, _playerControls.Conditions.isJumping && !_playerControls.Conditions.isCollidingBelow && jumpsLeft == 0 && !_playerControls.Conditions.isFalling );

    // Fall
    _animator.SetBool( _fallAnimatorParam, _playerControls.Conditions.isFalling && _playerControls.Conditions.isJumping && !_playerControls.Conditions.isCollidingBelow );
  }

  private void JumpResponse( float jump )
  {
    _playerControls.SetVerticalForce( Mathf.Sqrt( 2f * jump * Mathf.Abs( _playerControls.Gravity ) ) );
  }

  private void OnEnable()
  {
    Jumper.OnJump += JumpResponse;
  }

  private void OnDisable()
  {
    Jumper.OnJump -= JumpResponse;
  }
}
