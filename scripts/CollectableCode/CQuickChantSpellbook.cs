using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CQuickChantSpellbook : Collectable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private QuickChant quickChantPrefab;

  protected override void Collect()
  {
    LearnQuickChant();
  }

  private void LearnQuickChant()
  {
    if( _playerMotor.GetComponent<QuickChantController>() != null )
    {
      _playerMotor.GetComponent<QuickChantController>().LearnQuickChant( quickChantPrefab );

      GameObject quickChantIcon = GameObject.Find( "QuickChantIcon" );
      GameObject quickChantIconDarken = GameObject.Find( "QuickChantIconDarken" );

      Image quickChantIconImage = quickChantIcon.GetComponent<Image>();
      Image quickChantIconDarkenImage = quickChantIconDarken.GetComponent<Image>();

      quickChantIconImage.enabled = true;
      quickChantIconDarkenImage.enabled = true;
    }
  }
}
