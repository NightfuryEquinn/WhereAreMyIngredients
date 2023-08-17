using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyComboAttackController : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private HeavyComboAttack heavyComboAttack;
  [ SerializeField ] private Transform hiddenHolder;

  [ Header( "Spell Settings" ) ]
  [ SerializeField] private float attackDelay = 1.5f;

  public PlayerControls PlayerControls { get; set; }

  private Animator _animator;

  private HeavyComboAttack _heavyComboAttackLearn;

  private int _heavyComboAttackAnimatorParam = Animator.StringToHash( "Heavy_Combo" );

  public bool isHeavyComboAttacking = false;

  private void Start()
  {
    _animator = GetComponent<Animator>();
    PlayerControls = GetComponent<PlayerControls>();
    LearnHeavyComboAttack( heavyComboAttack );
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.L ) && !isHeavyComboAttacking )
    {
      isHeavyComboAttacking = true;
      _animator.SetBool( _heavyComboAttackAnimatorParam, true );
      StartCoroutine( WaitAndStopAnim( 0f ) );
    }
  }

  public void HeavyComboShoot()
  {
    if ( _heavyComboAttackLearn != null )
    {
      _heavyComboAttackLearn.Shoot();
    }
  }

  public void LearnHeavyComboAttack( HeavyComboAttack heavyComboAttack )
  {
    if ( _heavyComboAttackLearn == null )
    {
      _heavyComboAttackLearn = Instantiate( heavyComboAttack, hiddenHolder.position, Quaternion.identity );

      _heavyComboAttackLearn.HeavyComboAttackController = this;

      _heavyComboAttackLearn.transform.SetParent( hiddenHolder );
    }
  }

  private IEnumerator WaitAndStopAnim( float delayTime )
  {
    SoundManager.instance.PlaySound( AudioLibrary.instance.HeavyAttack1Clip );

    //Stops Animation
    yield return new WaitForSeconds( 0.1f );

    _animator.SetBool( _heavyComboAttackAnimatorParam, false );
    StartCoroutine( WaitAndAllowAttack( attackDelay ) );
  }

  private IEnumerator WaitAndAllowAttack( float attackDelay )
  {
    SoundManager.instance.PlaySound( AudioLibrary.instance.HeavyAttack2Clip );

    //Attack delay
    yield return new WaitForSeconds( attackDelay );

    isHeavyComboAttacking = false;
  }

}
