using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private GameObject[] playerLifes;

  // Updates the player lifes
  private void OnPlayerLifes( int currentLifes )
  {
    for ( int i = 0; i < playerLifes.Length; i++ )
    {
      if ( i < currentLifes )
      {
        playerLifes[i].gameObject.SetActive( true );
      }
      else
      {
        playerLifes[i].gameObject.SetActive( false );
      }
    }
  }

  private void OnEnable()
  {
    PlayerHealth.OnLifesChanged += OnPlayerLifes;
  }

  private void OnDisable()
  {
    PlayerHealth.OnLifesChanged -= OnPlayerLifes;
  }
}
