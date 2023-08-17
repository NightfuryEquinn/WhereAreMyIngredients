using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float disappearDelay = 2f;
  [ SerializeField ] private float reappearDelay = 3f;

  [ SerializeField ] private Sprite initialFallingPlatform;
  [ SerializeField ] private Sprite hintFallingPlatform;

  private SpriteRenderer spriteRenderer;
  private Collider2D platformCollider;
  private bool hasFallen = false;

  private void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    platformCollider = GetComponent<Collider2D>();
  }

  private void OnTriggerEnter2D( Collider2D other )
  {
    if( other.gameObject.CompareTag( "Player" ) && !hasFallen )
    {
      StartCoroutine( Fall() );
    }
  }

  private IEnumerator Fall()
  {
    hasFallen = true;

    spriteRenderer.sprite = hintFallingPlatform;
    yield return new WaitForSeconds( disappearDelay / 2 ); 

    spriteRenderer.sprite = null;
    platformCollider.enabled = false;
    
    yield return new WaitForSeconds( disappearDelay / 2 ); 
    
    yield return new WaitForSeconds( reappearDelay );

    platformCollider.enabled = true;
    spriteRenderer.sprite = initialFallingPlatform;
    
    hasFallen = false;
  }
}
