using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHeavyChantSpellbook : Collectable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private HeavyChant heavyChantPrefab;

  protected override void Collect()
  {
    LearnHeavyChant();
  }

  private void LearnHeavyChant()
  {
    if( _playerMotor.GetComponent<HeavyChantController>() != null )
    {
      _playerMotor.GetComponent<HeavyChantController>().LearnHeavyChant( heavyChantPrefab );

      GameObject heavyChantIcon = GameObject.Find( "HeavyChantIcon" );
      GameObject heavyChantIconDarken = GameObject.Find( "HeavyChantIconDarken" );

      Image heavyChantIconImage = heavyChantIcon.GetComponent<Image>();
      Image heavyChantIconDarkenImage = heavyChantIconDarken.GetComponent<Image>();

      heavyChantIconImage.enabled = true;
      heavyChantIconDarkenImage.enabled = true;
    }
  }
}
