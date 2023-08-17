using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackController : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private BasicAttack basicAttack;
  [ SerializeField ] private Transform hiddenHolder;

  [ Header( "Spell Settings" ) ]
  [ SerializeField ] private float attackDelay = 0.4f;

  public PlayerControls PlayerControls { get; set; }

  private Animator _animator;

  private BasicAttack _basicAttackLearn;

  private int _basicAttackAnimatorParam = Animator.StringToHash( "Basic_Attack" );

  public bool isBasicAttacking = false;

  private void Start()
  {
    _animator = GetComponent<Animator>();
    PlayerControls = GetComponent<PlayerControls>();
    LearnBasicAttack( basicAttack );
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.J ) && !isBasicAttacking )
    {
      isBasicAttacking = true;
      _animator.SetBool( _basicAttackAnimatorParam, true );
      StartCoroutine( WaitAndStopAnim( 0f ) );
    }
  }

  public void BasicShoot()
  {
    if ( _basicAttackLearn != null )
    {
      _basicAttackLearn.Shoot();

      SoundManager.instance.PlaySound( AudioLibrary.instance.LightAttack1Clip );
    }
  }

  public void LearnBasicAttack( BasicAttack basicAttack )
  {
    if ( _basicAttackLearn == null )
    {
      _basicAttackLearn = Instantiate( basicAttack, hiddenHolder.position, Quaternion.identity );

      _basicAttackLearn.BasicAttackController = this;

      _basicAttackLearn.transform.SetParent( hiddenHolder );
    }
  }

  private IEnumerator WaitAndStopAnim( float delayTime )
  {
    //Stops Animation
    yield return new WaitForSeconds( 0.1f );

    _animator.SetBool( _basicAttackAnimatorParam, false );
    StartCoroutine( WaitAndAllowAttack( attackDelay ) );
  }

  private IEnumerator WaitAndAllowAttack( float attackDelay )
  {
    //Attack delay
    yield return new WaitForSeconds( attackDelay );

    isBasicAttacking = false;
  }
}
