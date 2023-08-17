using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackController : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private ComboAttack comboAttack;
  [ SerializeField ] private Transform hiddenHolder;

  [ Header( "Spell Settings" ) ]
  [ SerializeField] private float attackDelay = 1.1f;

  public PlayerControls PlayerControls { get; set; }

  private Animator _animator;

  private ComboAttack _comboAttackLearn;

  private int _comboAttackAnimatorParam = Animator.StringToHash( "Combo" );

  public bool isComboAttacking = false;

  private void Start()
  {
    _animator = GetComponent<Animator>();
    PlayerControls = GetComponent<PlayerControls>();
    LearnComboAttack( comboAttack );
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.K ) && !isComboAttacking )
    {
      isComboAttacking = true;

      _animator.SetBool( _comboAttackAnimatorParam, true );
      
      StartCoroutine( WaitAndStopAnim( 0f ) );
    }
  }

  public void ComboShoot()
  {
    if ( _comboAttackLearn != null )
    {
      _comboAttackLearn.Shoot();
    }
  }

  public void LearnComboAttack( ComboAttack comboAttack )
  {
    if ( _comboAttackLearn == null )
    {
      _comboAttackLearn = Instantiate( comboAttack, hiddenHolder.position, Quaternion.identity );

      _comboAttackLearn.ComboAttackController = this;

      _comboAttackLearn.transform.SetParent( hiddenHolder );
    }
  }

  private IEnumerator WaitAndStopAnim( float delayTime )
  {
    SoundManager.instance.PlaySound( AudioLibrary.instance.LightAttack1Clip );

    //Stops Animation
    yield return new WaitForSeconds( 0.25f );

    _animator.SetBool( _comboAttackAnimatorParam, false );

    SoundManager.instance.PlaySound( AudioLibrary.instance.LightAttack2Clip );

    StartCoroutine( WaitAndAllowAttack( 0.65f ) );
  }

  private IEnumerator WaitAndAllowAttack( float attackDelay )
  {
    //Attack delay
    yield return new WaitForSeconds( attackDelay );

    SoundManager.instance.PlaySound( AudioLibrary.instance.LightAttack3Clip );

    isComboAttacking = false;
  }
}
