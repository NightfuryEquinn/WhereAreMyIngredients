using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMeleeChantSpellbook : Collectable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private MeleeChant meleeChantPrefab;

  protected override void Collect()
  {
    LearnMeleeChant();
  }

  private void LearnMeleeChant()
  {
    if( _playerMotor.GetComponent<MeleeChantController>() != null )
    {
      _playerMotor.GetComponent<MeleeChantController>().LearnMeleeChant( meleeChantPrefab );

      GameObject meleeChantIcon = GameObject.Find( "MeleeChantIcon" );
      GameObject meleeChantIconDarken = GameObject.Find( "MeleeChantIconDarken" );

      Image meleeChantIconImage = meleeChantIcon.GetComponent<Image>();
      Image meleeChantIconDarkenImage = meleeChantIconDarken.GetComponent<Image>();

      meleeChantIconImage.enabled = true;
      meleeChantIconDarkenImage.enabled = true;
    }
  }
}
