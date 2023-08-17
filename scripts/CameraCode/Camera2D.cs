using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
  [ Header( "Follow" ) ]
  [ SerializeField ] private bool horizFollow = true;
  [ SerializeField ] private bool vertFollow = true;
  [ SerializeField ] public bool followPlayer = true;

  [ Header( "Horizontal" ) ]
  [ SerializeField ] [ Range( 0, 1 ) ] private float horizInfluence = 1f;
  [ SerializeField ] private float horizOffset = 0f;
  [ SerializeField ] private float horizSmooth = 7.5f;

  [ Header( "Vertical" ) ]
  [ SerializeField ] [ Range( 0, 1 ) ] private float vertInfluence = 1f;
  [ SerializeField ] private float vertOffset = 0f;
  [ SerializeField ] private float vertSmooth = 7.5f;

  [ Header( "Camera Self Movement" ) ]
  [ SerializeField ] public bool enableCameraMovement = false;
  [ SerializeField ] public Vector2 cameraMovementDirection = Vector2.zero;
  [ SerializeField ] public float cameraMoveSpeed = 5f;

  [ Header( "Camera Change Direction" ) ]
  [ SerializeField ] public bool enableDirectionChange = false;
  [ SerializeField ] public List<Vector2> newCameraMovementDirections = new List<Vector2>();
  [ SerializeField ] public bool useLastDirection = true;
  [ SerializeField ] public float directionChangeDelay = 5f;
  
  [ Header( "Out of Camera Execution" ) ]
  [ SerializeField ] public bool enablePlayerKill = true;
  [ SerializeField ] public float timeDelayToExecute = 2f;

  public PlayerMotor Target { get; set; }

  // Position of the target
  public Vector3 targetPosition { get; set; }

  // Reference of the target position known by the camera
  public Vector3 cameraTargetPosition { get; set; }

  private float _targetHorizSmoothFollow;
  private float _targetVertSmoothFollow;

  // Reference of player out of camera
  private float timeSinceOutOfCamera = 0f;

  // Reference of camera direction change
  private float timeSinceDirectionChange = 0f;
  private int directionIndex = 0;

  private void Update() 
  {
    moveCamera();

    if( enablePlayerKill && Target != null )
    {
      CheckPlayerDistance();
    }

    if( enableDirectionChange )
    {
      UpdateCameraDirection();
    }
  }

  // Update the camera either to use last vector or loop
  private void UpdateCameraDirection()
  {
    if( timeSinceDirectionChange >= directionChangeDelay )
    {
      timeSinceDirectionChange = 0f;

      if( useLastDirection )
      {
        directionIndex = newCameraMovementDirections.Count - 1;
      }
      else
      {
        directionIndex = ( directionIndex + 1 ) % newCameraMovementDirections.Count;
      }

      cameraMovementDirection = newCameraMovementDirections[ directionIndex ];
    }
    else
    {
      timeSinceDirectionChange += Time.deltaTime;
    }
  }

  // Check if player is beyond the camera distance
  private void CheckPlayerDistance()
  {
    if( !IsPlayerInCameraView( Target.gameObject ) )
    {
      if( timeSinceOutOfCamera >= timeDelayToExecute )
      {
        PlayerHealth playerHealth = Target.GetComponent<PlayerHealth>();
        playerHealth.KillPlayer();
      }
      else
      {
        timeSinceOutOfCamera += Time.deltaTime;
      }
    }
    else
    {
      timeSinceOutOfCamera = 0f;
    }
  }

  // Check if player is in the camera
  private bool IsPlayerInCameraView( GameObject player )
  {
    Vector3 viewportPos = Camera.main.WorldToViewportPoint( player.transform.position );
    return( viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1 );
  }

  // Move the camera
  private void moveCamera()
  {
    if ( Target == null )
    {
      return; 
    }
    
    if( followPlayer )
    {
      // Calculate position
      targetPosition = getTargetPosition( Target );
      cameraTargetPosition = new Vector3( targetPosition.x, targetPosition.y, 0f );

      // Follow on selected axis
      float xPos = horizFollow ? cameraTargetPosition.x : transform.localPosition.x;
      float yPos = vertFollow ? cameraTargetPosition.y : transform.localPosition.y;

      // Set offset
      cameraTargetPosition += new Vector3( horizFollow ? horizOffset : 0f, vertFollow ? vertOffset : 0f, 0f );

      // Set smooth value
      _targetHorizSmoothFollow = Mathf.Lerp( _targetHorizSmoothFollow, cameraTargetPosition.x, horizSmooth * Time.deltaTime );
      _targetVertSmoothFollow = Mathf.Lerp( _targetVertSmoothFollow, cameraTargetPosition.y, vertSmooth * Time.deltaTime );

      // Get direction towards target position
      float xDir = _targetHorizSmoothFollow - transform.localPosition.x;
      float yDir = _targetVertSmoothFollow - transform.localPosition.y;

      Vector3 deltaDir = new Vector3( xDir, yDir, 0f );

      // New position
      Vector3 newCameraPosition = transform.localPosition + deltaDir;

      // Apply new position
      transform.localPosition = new Vector3( newCameraPosition.x, newCameraPosition.y, transform.localPosition.z );
    }
    
    if( !followPlayer && enableCameraMovement )
    {
      Vector3 cameraMove = new Vector3( cameraMovementDirection.x, cameraMovementDirection.y, 0f ) * cameraMoveSpeed * Time.deltaTime;
      transform.localPosition += cameraMove;
    }
  }

  // Returns the position of our target
  private Vector3 getTargetPosition( PlayerMotor player )
  {
    if( player != null )
    {
      float xPos = 0f;
      float yPos = 0f;

      xPos += ( player.transform.position.x + horizOffset ) * horizInfluence;
      yPos += ( player.transform.position.y + vertOffset ) * vertInfluence;

      Vector3 positionTarget = new Vector3( xPos, yPos, transform.position.z );

      return positionTarget;
    }

    return transform.position;
  }

  // Centers camera in the target position
  private void centerOnTarget( PlayerMotor player )
  {
    Target = player;

    Vector3 targetPos = getTargetPosition( Target );

    _targetHorizSmoothFollow = targetPos.x;
    _targetVertSmoothFollow = targetPos.y;

    transform.localPosition = targetPos;
  }

  // Reset the target reference
  private void stopFollow( PlayerMotor player )
  {
    Target = null;
  }

  // Get target reference and center camera
  public void startFollowing( PlayerMotor player )
  {
    Target = player;
    centerOnTarget( Target );
  }

  private void onDrawGizmos()
  {
    Gizmos.color = Color.blue;
    Vector3 camPosition = new Vector3( transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 2f );

    Gizmos.DrawWireSphere( camPosition, 0.5f );
  }
  
  private void OnEnable() 
  {
    LevelManager.OnPlayerSpawn += centerOnTarget;

    PlayerHealth.OnDeath += stopFollow;
    PlayerHealth.OnRevive += startFollowing;
  }

  private void OnDisable() 
  {
    LevelManager.OnPlayerSpawn -= centerOnTarget;

    PlayerHealth.OnDeath -= stopFollow;
    PlayerHealth.OnRevive -= startFollowing;
  }
}
