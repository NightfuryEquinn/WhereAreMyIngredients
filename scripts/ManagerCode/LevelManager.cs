using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
  public static Action<PlayerMotor> OnPlayerSpawn;

  [ Header( "Settings" ) ]
  [ SerializeField ] private GameObject playerPrefab;

  [ Header( "Levels" ) ]
  [ SerializeField ] private Level[] levels;

  [ Header( "Camera" ) ]
  [ SerializeField ] private Camera2D _camera;

  [ Header( "Environment" ) ]
  [ SerializeField ] private Sprite mountainImage;
  [ SerializeField ] private Sprite caveImage;

  private Transform levelStartPoint;

  private PlayerMotor _currentPlayer;
  private int _nextLevel;

  public void StartLevel( int levelIndex ) 
  {
    InitLevel( levelIndex );
    SpawnPlayer( playerPrefab, levels[ levelIndex ].SpawnPoint );

    SoundManager.instance.PlayMusic( levelIndex );

    RevivePlayer();

    Image changeImage = GameObject.Find( "BackgroundCover" ).GetComponent<Image>();
    changeImage.sprite = mountainImage;

    if( levelIndex == 3 )
    {
      changeImage.sprite = caveImage;
    }

    if( levelIndex == 4 )
    {
      changeImage.sprite = caveImage;
    }
  }

  public void Obliteration()
  {
    Destroy( GameObject.Find( "Player(Clone)" ) );

    GameObject quickChantIcon = GameObject.Find( "QuickChantIcon" );
    GameObject quickChantIconDarken = GameObject.Find( "QuickChantIconDarken" );

    Image quickChantIconImage = quickChantIcon.GetComponent<Image>();
    Image quickChantIconDarkenImage = quickChantIconDarken.GetComponent<Image>();

    quickChantIconImage.enabled = false;
    quickChantIconDarkenImage.enabled = false;

    GameObject heavyChantIcon = GameObject.Find( "HeavyChantIcon" );
    GameObject heavyChantIconDarken = GameObject.Find( "HeavyChantIconDarken" );

    Image heavyChantIconImage = heavyChantIcon.GetComponent<Image>();
    Image heavyChantIconDarkenImage = heavyChantIconDarken.GetComponent<Image>();

    heavyChantIconImage.enabled = false;
    heavyChantIconDarkenImage.enabled = false;

    GameObject meleeChantIcon = GameObject.Find( "MeleeChantIcon" );
    GameObject meleeChantIconDarken = GameObject.Find( "MeleeChantIconDarken" );

    Image meleeChantIconImage = meleeChantIcon.GetComponent<Image>();
    Image meleeChantIconDarkenImage = meleeChantIconDarken.GetComponent<Image>();

    meleeChantIconImage.enabled = false;
    meleeChantIconDarkenImage.enabled = false;
  }

  private void Start()
  {
    // Call Event
    OnPlayerSpawn?.Invoke( _currentPlayer );
  }

  private void Update()
  {
    if ( Input.GetKeyDown( KeyCode.R ) )
    {
      RevivePlayer();
    }
  }

  private void InitLevel( int levelIndex )
  {
    foreach( Level level in levels )
    {
      level.gameObject.SetActive( false );
    }

    levels[ levelIndex ].gameObject.SetActive( true );
    levelStartPoint = levels[ levelIndex ].SpawnPoint;
  }

  private void SpawnPlayer( GameObject player, Transform spawnPoint )
  {
    if ( player != null )
    {
      _currentPlayer = Instantiate( player, spawnPoint.position, Quaternion.identity ).GetComponent<PlayerMotor>();
      _currentPlayer.GetComponent<PlayerHealth>().ResetLife();
    }
  }

  private void RevivePlayer()
  {
    if ( _currentPlayer != null )
    {
      _currentPlayer.gameObject.SetActive( true );
      _currentPlayer.SpawnPlayer( levelStartPoint );
      _currentPlayer.GetComponent<PlayerHealth>().ResetLife();
      _currentPlayer.GetComponent<PlayerHealth>().Revive();
    }
  }

  public void PlayerDeath( PlayerMotor player )
  {
    player.gameObject.SetActive( false );
  }

  private void MovePlayerToStartPosition( Transform newSpawnPoint )
  {
    if( _currentPlayer != null )
    {
      _currentPlayer.transform.position = new Vector3( newSpawnPoint.position.x, newSpawnPoint.position.y, 0f );
    }
  }

  private void LoadLevel()
  {
    GameManager.instance.GameState = GameManager.GameStates.LevelLoaded;
    _nextLevel = GameManager.instance.CurrentLevelCompleted + 1;
    InitLevel( _nextLevel );
    MovePlayerToStartPosition( levels[ _nextLevel ].SpawnPoint );
  }

  private void OnEnable()
  {
    PlayerHealth.OnDeath += PlayerDeath;
    GameManager.LoadNextLevel += LoadLevel;
  }

  private void OnDisable()
  {
    PlayerHealth.OnDeath -= PlayerDeath;
    GameManager.LoadNextLevel -= LoadLevel;
  }
}
