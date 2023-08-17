using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
  protected PlayerMotor _playerMotor;
  protected SpriteRenderer _spriteRenderer;
  protected Collider2D _collider2D;

  private void Start()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    _collider2D = GetComponent<Collider2D>();
  }

  // Contains the logic of collectables
  private void CollectLogic()
  {
    if( !CanBePicked() )
    {
      return;
    }

    SoundManager.instance.PlaySound( AudioLibrary.instance.CollectablesCollectedClip );

    Collect();
    DisableCollectable();
  }

  // Override to add custom collectable behaviour
  protected virtual void Collect()
  {
    
  }

  // Disable the sprite and collider of the collectable
  private void DisableCollectable()
  {
    _collider2D.enabled = false;
    _spriteRenderer.enabled = false;

    StartCoroutine( IERegenerate() );
  }

  // Returns if this collectable can be picked
  // True if is colliding with the player
  private bool CanBePicked()
  {
    return _playerMotor != null;
  }

  private void OnTriggerEnter2D( Collider2D other )
  {
    if( other.GetComponent<PlayerMotor>() != null )
    {
      _playerMotor = other.GetComponent<PlayerMotor>();
      CollectLogic();
    }
  }

  private void OnTriggerExit2D( Collider2D other )
  {
    _playerMotor = null;
  }

  private IEnumerator IERegenerate()
  {
    yield return new WaitForSeconds( 20 );

    _collider2D.enabled = true;
    _spriteRenderer.enabled = true;
  }
}
