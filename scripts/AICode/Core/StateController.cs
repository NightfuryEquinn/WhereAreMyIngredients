using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
  [ Header( "State" ) ]
  [ SerializeField ] private AIState currentState;
  [ SerializeField ] private AIState remainState;

  [ Header( "Shooting" ) ]
  [ SerializeField ] private Transform firePoint;

  // Reference of the Path
  public Path RefPath { get; set; }

  private Vector3 _radiusStartPosition;
  private float _detectionRadius;
  private bool _playerDetected;

  // Reference of the Player
  public PlayerMotor Target { get; set; }

  public ObjectPooler Pooler { get; set; }

  public Transform FirePoint => firePoint;

  private void Start()
  {
    RefPath = GetComponent<Path>();
    Pooler = GetComponent<ObjectPooler>();
  }

  private void Update()
  {
    currentState.RunState( this );
  }

  // Update our state to a new one
  public void TransitionToState( AIState newState )
  {
    if( newState != remainState )
    {
      currentState = newState;
    }
  }

  // Create a test line to visualize the ray casting
  public void DebugRay( float rayLength, Vector3 startPosition, Vector3 direction, bool playerDetected )
  {
    Debug.DrawLine( startPosition, startPosition + direction * rayLength, playerDetected ? Color.green : Color.red );
  }

  // Get the detection circle data
  public void SetRadiusDetectionValues( float radius, Vector3 startPosition, bool playerDetection )
  {
    _detectionRadius = radius;
    _radiusStartPosition = startPosition;
    _playerDetected = playerDetection;
  }

  private void OnDrawGizmos()
  {
    if( _detectionRadius > 0 )
    {
      Gizmos.color = _playerDetected ? Color.green : Color.red;
      Gizmos.DrawWireSphere( _radiusStartPosition, _detectionRadius );
    }
  }
}