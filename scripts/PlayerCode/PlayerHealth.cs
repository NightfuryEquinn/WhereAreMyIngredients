using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  public static Action<int> OnLifesChanged;
  public static Action<PlayerMotor> OnDeath;
  public static Action<PlayerMotor> OnRevive;

  [ Header( "Settings" ) ]
  [ SerializeField ] private int lifes = 25;

  public int MaxLifes => _maxLifes;
  public int CurrentLifes => _currentLifes;

  private int _maxLifes;
  private int _currentLifes;

  private void Awake() 
  {
    _maxLifes = lifes;
  }

  private void Start()
  {
    ResetLife();
  }

  public void AddLife( int addAmount )
  {
    _currentLifes += addAmount;

    if ( _currentLifes > _maxLifes )
    {
      _currentLifes = _maxLifes;
    }

    UpdateLifesUI();
  }

  public void LoseLife( int loseAmount )
  {
    _currentLifes -= loseAmount;

    if( loseAmount != 0 )
    {
      SoundManager.instance.PlaySound( AudioLibrary.instance.HurtClip );
    }

    if ( _currentLifes <= 0 )
    {
      _currentLifes = 0;
      
      OnDeath?.Invoke( gameObject.GetComponent<PlayerMotor>() );
      SoundManager.instance.PlaySound( AudioLibrary.instance.DeathClip );
    }

    UpdateLifesUI();
  }

  public void KillPlayer()
  {
    _currentLifes = 0;
    SoundManager.instance.PlaySound( AudioLibrary.instance.DeathClip );

    UpdateLifesUI();

    OnDeath?.Invoke( gameObject.GetComponent<PlayerMotor>() );
  }

  public void ResetLife()
  {
    _currentLifes = lifes;

    UpdateLifesUI();
  }

  public void Revive()
  {
    OnRevive?.Invoke( gameObject.GetComponent<PlayerMotor>() );
  }

  private void UpdateLifesUI()
  {
    OnLifesChanged?.Invoke( _currentLifes );
  }

  private void OnTriggerEnter2D( Collider2D other ) 
  {
    if( other.GetComponent<Damageable>() != null )
    {
      other.GetComponent<Damageable>().Damage( gameObject.GetComponent<PlayerMotor>() );
    }  
  }
}
