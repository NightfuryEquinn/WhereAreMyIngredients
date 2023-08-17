using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] private float gravity = -80f;
  [ SerializeField ] private float fallMultiplier = 1.75f;
  [ SerializeField ] private float fallClamp = -20f;

  [ Header( "Collisions" ) ]
  [ SerializeField ] private LayerMask collideWith;
  [ SerializeField ] private int verticalRayAmount = 4;
  [ SerializeField ] private int horizontalRayAmount = 4;

  // Properties
  #region Properties

  // Return if player is facing right
  public bool FacingRight { get; set; }

  // Return the gravity value
  public float Gravity => gravity;

  // Return the force applied
  public Vector2 Force => _force;

  // Return the conditions
  public PlayerConditions Conditions => _conditions;

  // Ice Floor
  public float Friction { get; set; }

  // Swamp Floor
  public float ReducedSpeed { get; set; }

  // Damage Floor
  public int MitigateDamage { get; set; }

  #endregion

  // Internal Variables
  #region InternalVariables

  private BoxCollider2D _boxCollider2D;

  // Player Conditions
  private PlayerConditions _conditions;
  private MovingPlatform _movingPlatform;

  // Player Bounds
  private Vector2 _topLeft;
  private Vector2 _topRight;
  private Vector2 _bottomLeft;
  private Vector2 _bottomRight;

  private float _boundsWidth;
  private float _boundsHeight;

  // Gravity
  private float _currentGravity;
  private Vector2 _force;
  private Vector2 _movePosition;

  // Collisions
  private float _skin = 0.05f;

  // Facing Direction
  private float _internalFaceDirection = 1f;
  private float _faceDirection;

  #endregion

  // Start
  private void Start() 
  {
    _boxCollider2D = GetComponent<BoxCollider2D>();

    _conditions = new PlayerConditions();
    _conditions.Reset();
  }

  // Update
  private void Update() 
  {
    ApplyGravity();
    StartMovement();

    EnterPlatformMovement();
    SetRayOrigins();
    GetFaceDirection();
    RotateModel();

    if( FacingRight )
    {
      HorizontalCollision( 1 );
    }
    else
    {
      HorizontalCollision( -1 );
    }

    CollisionBelow();
    CollisionAbove();

    Debug.DrawRay( _topLeft, Vector2.left, Color.cyan );
    Debug.DrawRay( _topRight, Vector2.right, Color.cyan );
    Debug.DrawRay( _bottomLeft, Vector2.left, Color.green );
    Debug.DrawRay( _bottomRight, Vector2.right, Color.green );

    transform.Translate( _movePosition, Space.Self );

    SetRayOrigins();
    CalculateMovement();
  }

  // Collision
  #region Collision
    // Collision Below
    #region CollisionBelow

    private void CollisionBelow()
    {
      Friction = 0f;
      ReducedSpeed = 0f;
      MitigateDamage = 0;

      if( _movePosition.y < -0.0001f )
      {
        _conditions.isFalling = true;
      }
      else
      {
        _conditions.isFalling = false;
      }

      // If player going up, then return function
      if( !_conditions.isFalling )
      {
        _conditions.isCollidingBelow = false;
        return;
      }

      // Calculate ray length
      float rayLength = _boundsHeight / 2f + _skin * 1.3f;

      if( _movePosition.y < 0 )
      {
        rayLength += Mathf.Abs( _movePosition.y );
      }

      // Calculate ray origin
      Vector2 leftOrigin = ( _bottomLeft + _topLeft ) / 2f;
      Vector2 rightOrigin = ( _bottomRight + _topRight ) / 2f;
      
      leftOrigin += ( Vector2 ) ( transform.up * _skin ) + ( Vector2 ) ( transform.right * _movePosition.x );
      rightOrigin += ( Vector2 ) ( transform.up * _skin ) + ( Vector2 ) ( transform.right * _movePosition.x );

      // Raycast
      for( int i = 0; i < verticalRayAmount; i++ )
      {
        Vector2 rayOrigin = Vector2.Lerp( leftOrigin, rightOrigin, ( float ) i / ( float ) ( verticalRayAmount - 1 ) );
        RaycastHit2D rayHit = Physics2D.Raycast( rayOrigin, -transform.up, rayLength, collideWith );

        Debug.DrawRay( rayOrigin, -transform.up * rayLength, Color.white);

        // Hit Conditions
        if( rayHit )
        {
          GameObject hitObject = rayHit.collider.gameObject;

          if( _force.y > 0 ) 
          {
            _movePosition.y = _force.y * Time.deltaTime;
            _conditions.isCollidingBelow = false;
          }
          else
          {
            _movePosition.y = -rayHit.distance + _boundsHeight / 2f + _skin * 1.3f;
          }

          _conditions.isCollidingBelow = true;
          _conditions.isFalling = false;

          if( Mathf.Abs( _movePosition.y ) < 0.0001f )
          {
            _movePosition.y = 0f;
          }

          if( hitObject.GetComponent<SpecialSurface>() != null )
          {
            Friction = hitObject.GetComponent<SpecialSurface>().Friction;
            ReducedSpeed = hitObject.GetComponent<SpecialSurface>().ReducedSpeed;
            MitigateDamage = hitObject.GetComponent<SpecialSurface>().MitigateDamage;
          }

          if( hitObject.GetComponent<MovingPlatform>() != null )
          {
            _movingPlatform = hitObject.GetComponent<MovingPlatform>();
          }
        }
      }
    }

    #endregion

    // Collision Horizontal
    #region CollisionHorizontal

    private void HorizontalCollision( int direction )
    {
      Vector2 rayHorizBottom = ( _bottomLeft + _bottomRight ) / 2f;
      Vector2 rayHorizTop = ( _topLeft + _topRight ) / 2f;

      rayHorizBottom += ( Vector2 ) transform.up * _skin;
      rayHorizTop -= ( Vector2 ) transform.up * _skin;

      // Calculate ray length
      float rayLength = Mathf.Abs( _force.x * Time.deltaTime ) + _boundsWidth / 2f + _skin * 2f;

      for( int i = 0; i < horizontalRayAmount; i++ )
      {
        Vector2 rayOrigin = Vector2.Lerp( rayHorizBottom, rayHorizTop, ( float ) i / ( horizontalRayAmount - 1 ) );
        RaycastHit2D rayHit = Physics2D.Raycast( rayOrigin, direction * transform.right, rayLength, collideWith );

        Debug.DrawRay( rayOrigin, transform.right * rayLength * direction, Color.magenta);

        if( rayHit )
        {
          if( direction >= 0 )
          {
            _movePosition.x = rayHit.distance - _boundsWidth / 2f - _skin * 2f;
            _conditions.isCollidingRight = true;
          }
          else
          {
            _movePosition.x = -rayHit.distance + _boundsWidth / 2f + _skin * 2f;
            _conditions.isCollidingLeft = true;
          }

          _force.x = 0f;
        }
      }
    }

    #endregion

    // Collision Above
    #region CollisionAbove

    private void CollisionAbove()
    {
      if( _movePosition.y < 0 )
      {
        return;
      }

      // Calculate ray length
      float rayLength = _movePosition.y + _boundsHeight / 2f;

      // Calculate origin points
      Vector2 rayTopLeft = ( _bottomLeft + _topLeft ) / 2f;
      Vector2 rayTopRight = ( _bottomRight + _topRight ) / 2f;

      rayTopLeft += ( Vector2 ) transform.right * _movePosition.x;
      rayTopRight += ( Vector2 ) transform.right * _movePosition.x;

      for( int i = 0; i < verticalRayAmount; i++ )
      {
        Vector2 rayOrigin = Vector2.Lerp( rayTopLeft, rayTopRight, ( float ) i / ( float ) ( verticalRayAmount - 1 ) );
        RaycastHit2D rayHit = Physics2D.Raycast( rayOrigin, transform.up, rayLength, collideWith );

        Debug.DrawRay( rayOrigin, transform.up * rayLength, Color.red );

        if( rayHit )
        {
          _movePosition.y = rayHit.distance - _boundsHeight / 2f;
          _conditions.isCollidingAbove = true;
        }
      }
    }

    #endregion

  #endregion

  #region Moving Platform

  private void EnterPlatformMovement()
  {
    Vector3 moveDirection;

    if( _movingPlatform == null )
    {
      return;
    }

    if( _movingPlatform.CollidingWithPlayer )
    {
      if( _movingPlatform.MoveSpeed != 0 )
      {
        if( _movingPlatform.Direction == Path.MoveDirections.RIGHT )
        {
          moveDirection = Vector3.right;
        }
        else if( _movingPlatform.Direction == Path.MoveDirections.LEFT )
        {
          moveDirection = Vector3.left;
        }
        else
        {
          if( _movingPlatform.Direction == Path.MoveDirections.UP )
          {
            moveDirection = Vector3.up;
          }
          else
          {
            moveDirection = Vector3.down;
          }
        }
        
        transform.Translate( moveDirection * _movingPlatform.MoveSpeed * Time.deltaTime );
      }
    }
  }

  #endregion

  // Gravity and Movement
  #region GravityAndMovement

  // Clamp force applied
  private void CalculateMovement()
  {
    if(Time.deltaTime > 0)
    {
      _force = _movePosition / Time.deltaTime;
    }
  }

  // Initialize the movePosition
  private void StartMovement()
  {
    _movePosition = _force * Time.deltaTime;
    _conditions.Reset();
  }

  // Set new x movement
  public void SetHorizontalForce( float xForce )
  {
    _force.x = xForce;
  }

  // Set new y movement
  public void SetVerticalForce( float yForce )
  {
    _force.y = yForce;
  }

  // Calculate gravity to apply
  private void ApplyGravity()
  {
    _currentGravity = gravity;

    if( _force.y < 0 )
    {
      _currentGravity *= fallMultiplier;
    }

    _force.y += _currentGravity * Time.deltaTime;

    if( _force.y < fallClamp ) 
    {
      _force.y = fallClamp;
    }
  }

  #endregion

  // Direction
  #region Direction

  // Manage the direction facing
  private void GetFaceDirection()
  {
    _faceDirection = _internalFaceDirection;

    // If FacingRight is true
    FacingRight = _faceDirection == 1;

    if( _force.x > 0.0001f )
    {
      _faceDirection = 1f;
      FacingRight = true;
    }
    else if( _force.x < -0.0001f )
    {
      _faceDirection = -1f;
      FacingRight = false;
    }

    _internalFaceDirection = _faceDirection;
  }

  // Rotate player orientation
  private void RotateModel()
  {
    if( FacingRight )
    {
      transform.localScale = new Vector3( 1f, 1f, 1f );
    }
    else
    {
      transform.localScale = new Vector3( -1f, 1f, 1f );
    }
  }

  #endregion


  // SetRayOrigins
  #region SetRayOrigins

  private void SetRayOrigins()
  {
    Bounds playerBounds = _boxCollider2D.bounds;

    _topLeft = new Vector2( playerBounds.min.x, playerBounds.max.y );
    _topRight = new Vector2( playerBounds.max.x, playerBounds.max.y );
    _bottomLeft = new Vector2( playerBounds.min.x, playerBounds.min.y );
    _bottomRight = new Vector2( playerBounds.max.x, playerBounds.min.y );

    _boundsWidth = Vector2.Distance( _bottomLeft, _bottomRight );
    _boundsHeight = Vector2.Distance( _bottomLeft, _topLeft );
  }

  #endregion
}
