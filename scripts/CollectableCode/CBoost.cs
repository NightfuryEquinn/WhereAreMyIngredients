using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBoost : Collectable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float boostSpeed = 25f;
  [ SerializeField ] private float boostTime = 5f;

  private PlayerMovement _playerMovement;

  protected override void Collect()
  {
    ApplyMovement();
  }

  // Apply movement bonus
  private void ApplyMovement()
  {
    _playerMovement = _playerMotor.GetComponent<PlayerMovement>(); 
    if( _playerMovement != null )
    {
      StartCoroutine( IEBoost() );
    }
  }

  private IEnumerator IEBoost()
  {
    _playerMovement.Speed = boostSpeed;
    yield return new WaitForSeconds( boostTime );

    _playerMovement.Speed = _playerMovement.InitialSpeed;
  }
}
