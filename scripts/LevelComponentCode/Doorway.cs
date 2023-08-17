using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour
{
  public static Action<int> OnLevelCompleted;

  [ Header( "Settings" ) ]
  [ SerializeField ] private int levelIndex;

  private void OnTriggerStay2D( Collider2D other )
  {
    if( other.gameObject.CompareTag( "Player" ) )
    {
      if( Input.GetKeyDown( KeyCode.Return ) || Input.GetKeyUp( KeyCode.Return ) )
      {
        if( levelIndex < 4 )
        {
          OnLevelCompleted?.Invoke( levelIndex );

          SoundManager.instance.PlayMusic( levelIndex + 1 );
        }
        else
        {
          Debug.Log( "The End" );
        }
      }
    }
  }
}
