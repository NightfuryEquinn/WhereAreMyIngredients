using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CJump : Collectable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float boostJumpHeight = 6f;
  [ SerializeField ] private float boostTime = 5f;
  
  private PlayerJump _playerJump;

  protected override void Collect()
  {
    ApplyHeight();
  }

  private void ApplyHeight()
  {
    _playerJump = _playerMotor.GetComponent<PlayerJump>();
    if( _playerJump != null )
    {
      StartCoroutine( IEBoostHeight() );
    }
  }

  private IEnumerator IEBoostHeight()
  {
    _playerJump.JumpHeight = boostJumpHeight;
    yield return new WaitForSeconds( boostTime );

    _playerJump.JumpHeight = _playerJump.InitialJumpHeight;
  }
}
