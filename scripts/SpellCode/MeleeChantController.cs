using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChantController : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private MeleeChant meleeChant;
  [ SerializeField ] private Transform hiddenHolder;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float delayTime = 1f;

  public PlayerControls PlayerControls { get; set; }

  private MeleeChant _meleeChantLearn;

  private bool isCasting;
  private float castProgress;

  private void Start()
  {
    PlayerControls = GetComponent<PlayerControls>();
  }

  private void Update()
  {
    if( Input.GetKeyDown( KeyCode.C ) && !isCasting && _meleeChantLearn )
    {
      StartCoroutine( IEMeleeChant() );
    }
  }

  private void Shoot()
  {
    if( _meleeChantLearn != null )
    {
      _meleeChantLearn.Shoot();
    }
  }

  public void LearnMeleeChant( MeleeChant meleeChant )
  {
    if( _meleeChantLearn == null )
    {
      _meleeChantLearn = Instantiate( meleeChant, hiddenHolder.position, Quaternion.identity );
      
      _meleeChantLearn.MeleeChantController = this;
      
      _meleeChantLearn.transform.SetParent( hiddenHolder );

      GameObject meleeChantModel = GameObject.Find( "MeleeChantModel" );
      SpriteRenderer spriteModel = meleeChantModel.GetComponent<SpriteRenderer>();

      spriteModel.enabled = false;
    }
  }

  private IEnumerator IEMeleeChant()
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
