using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickChantController : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private QuickChant quickChant;
  [ SerializeField ] private Transform hiddenHolder;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float delayTime = 1f;

  public PlayerControls PlayerControls { get; set; }

  private QuickChant _quickChantLearn;

  private bool isCasting;
  private float castProgress;

  private void Start()
  {
    PlayerControls = GetComponent<PlayerControls>();
  }

  private void Update()
  {
    if( Input.GetKeyDown( KeyCode.Q ) && !isCasting && _quickChantLearn )
    {
      StartCoroutine( IEQuickChant() );      
    }
  }

  private void Shoot()
  {
    if( _quickChantLearn != null )
    {
      _quickChantLearn.Shoot();
    }
  }

  public void LearnQuickChant( QuickChant quickChant )
  {
    if( _quickChantLearn == null )
    {
      _quickChantLearn = Instantiate( quickChant, hiddenHolder.position, Quaternion.identity );
      
      _quickChantLearn.QuickChantController = this;
      
      _quickChantLearn.transform.SetParent( hiddenHolder );

      GameObject quickChantModel = GameObject.Find( "QuickChantModel" );
      SpriteRenderer spriteModel = quickChantModel.GetComponent<SpriteRenderer>();

      spriteModel.enabled = false;
    }
  }

  private IEnumerator IEQuickChant()
  {
    isCasting = true;

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
