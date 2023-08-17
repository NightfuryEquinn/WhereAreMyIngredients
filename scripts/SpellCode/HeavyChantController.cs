using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyChantController : MonoBehaviour
{
  [ Header( "Setting" ) ]
  [ SerializeField ] private HeavyChant heavyChant;
  [ SerializeField ] private Transform hiddenHolder;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float delayTime = 2f;

  public PlayerControls PlayerControls { get; set; }

  private HeavyChant _heavyChantLearn;

  private bool isCasting;
  private float castProgress;

  private void Start()
  {
    PlayerControls = GetComponent<PlayerControls>();
  }

  private void Update()
  {
    if( Input.GetKeyDown( KeyCode.E ) && !isCasting && _heavyChantLearn )
    {
      StartCoroutine( IEHeavyChant() );
    }
  }

  private void Shoot()
  {
    if ( _heavyChantLearn != null )
    {
      _heavyChantLearn.Shoot();
    }
  }

  public void LearnHeavyChant( HeavyChant heavyChant )
  {
    if( _heavyChantLearn == null )
    {
      _heavyChantLearn = Instantiate( heavyChant, hiddenHolder.position, Quaternion.identity );

      _heavyChantLearn.HeavyChantController = this;

      _heavyChantLearn.transform.SetParent( hiddenHolder );

      GameObject heavyChantModel = GameObject.Find( "HeavyChantModel" );
      SpriteRenderer spriteModel = heavyChantModel.GetComponent<SpriteRenderer>();

      spriteModel.enabled = false;
    }
  }

  private IEnumerator IEHeavyChant()
  {
    isCasting = true;
    SoundManager.instance.PlaySound( AudioLibrary.instance.HeavyChantClip );

    while( castProgress < delayTime )
    {
      yield return null;
      castProgress += Time.deltaTime;
    }

    Shoot();

    isCasting = false;
    castProgress = 0f;
  }
}
