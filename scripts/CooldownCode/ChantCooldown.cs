using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChantCooldown : MonoBehaviour
{
  [ Header( "Quick Chant Settings" ) ]
  [ SerializeField ] private Image quickChantImage;
  [ SerializeField ] private Image quickChantImageOverlay;
  [ SerializeField ] private float quickChantCooldown = 2.5f;
  private bool isQuickChantCooldown = false;

  [ Header( "Heavy Chant Settings" ) ]
  [ SerializeField ] private Image heavyChantImage;
  [ SerializeField ] private Image heavyChantImageOverlay;
  [ SerializeField ] private float heavyChantCooldown = 7.5f;
  private bool isHeavyChantCooldown = false;

  [ Header( "Melee Chant Settings" ) ]
  [ SerializeField ] private Image meleeChantImage;
  [ SerializeField ] private Image meleeChantImageOverlay;
  [ SerializeField ] private float meleeChantCooldown = 0.625f;
  private bool isMeleeChantCooldown = false;

  private void Start()
  {
    quickChantImageOverlay.fillAmount = 0;
    quickChantImage.enabled = false;
    quickChantImageOverlay.enabled = false;

    heavyChantImageOverlay.fillAmount = 0;
    heavyChantImage.enabled = false;
    heavyChantImageOverlay.enabled = false;

    meleeChantImageOverlay.fillAmount = 0;
    meleeChantImage.enabled = false;
    meleeChantImageOverlay.enabled = false;
  }

  private void Update()
  {
    QuickChantCD();
    HeavyChantCD();
    MeleeChantCD();
  }

  private void QuickChantCD()
  {
    if( Input.GetKeyDown( KeyCode.Q ) && !isQuickChantCooldown )
    {
      isQuickChantCooldown = true;
      quickChantImageOverlay.fillAmount = 1;
    }

    if( isQuickChantCooldown )
    {
      quickChantImageOverlay.fillAmount -= 1 / quickChantCooldown * Time.deltaTime;

      if( quickChantImageOverlay.fillAmount <= 0 )
      {
        quickChantImageOverlay.fillAmount = 0;
        isQuickChantCooldown = false;
      }
    }
  }

  private void HeavyChantCD()
  {
    if( Input.GetKeyDown( KeyCode.E ) && !isHeavyChantCooldown )
    {
      isHeavyChantCooldown = true;
      heavyChantImageOverlay.fillAmount = 1;
    }

    if( isHeavyChantCooldown )
    {
      heavyChantImageOverlay.fillAmount -= 1 / heavyChantCooldown * Time.deltaTime;

      if( heavyChantImageOverlay.fillAmount <= 0 )
      {
        heavyChantImageOverlay.fillAmount = 0;
        isHeavyChantCooldown = false;
      }
    }
  }

  private void MeleeChantCD()
  {
    if( Input.GetKeyDown( KeyCode.C ) && !isMeleeChantCooldown )
    {
      isMeleeChantCooldown = true;
      meleeChantImageOverlay.fillAmount = 1;
    }

    if( isMeleeChantCooldown )
    {
      meleeChantImageOverlay.fillAmount -= 1 / meleeChantCooldown * Time.deltaTime;

      if( meleeChantImageOverlay.fillAmount <= 0 )
      {
        meleeChantImageOverlay.fillAmount = 0;
        isMeleeChantCooldown = false;
      }
    }
  }
}
