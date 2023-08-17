using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
  protected PlayerControls _playerControls;
  protected PlayerHealth _playerHealth;
  protected Animator _animator;
  protected float _horizontalInput;
  protected float _verticalInput;

  // The virtual keyword allows derived classes to override the method with their own implementation
  protected virtual void Start()
  {
    InitState();
  }

  protected virtual void InitState()
  {
    _playerHealth = GetComponent<PlayerHealth>();
    _playerControls = GetComponent<PlayerControls>();
    _animator = GetComponent<Animator>();
  }

  // Override to create the state logic
  public virtual void ExecuteState()
  {
    if( GameManager.instance.GameState == GameManager.GameStates.LevelCompleted )
    {
      return;
    }
  }

  // Gets the normal input
  public virtual void LocalInput()
  {
    if( GameManager.instance.GameState == GameManager.GameStates.LevelCompleted )
    {
      return;
    }
    
    _horizontalInput = Input.GetAxisRaw("Horizontal");
    _verticalInput = Input.GetAxisRaw("Vertical");

    GetInput();
  }

  // Override to support other inputs
  protected virtual void GetInput()
  {

  }

  public virtual void SetAnimation()
  {
    
  }
}
