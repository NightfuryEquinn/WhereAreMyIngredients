using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealth : Collectable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] protected int addAmount = 25;

  protected override void Collect()
  {
    AddLife( addAmount );
  }

  private void AddLife( int addAmount )
  {
    if( _playerMotor.GetComponent<PlayerHealth>() == null )
    {
      return;
    }

    PlayerHealth playerHealth = _playerMotor.GetComponent<PlayerHealth>();
    if( playerHealth.CurrentLifes < playerHealth.MaxLifes )
    {
      playerHealth.AddLife( addAmount );
    }
  }
}
