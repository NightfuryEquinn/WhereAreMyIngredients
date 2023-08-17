using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
  [ Header( "Music" ) ]
  [ SerializeField ] private AudioClip[] mainThemes;

  private AudioSource _audioSource;
  private ObjectPooler _pooler;

  private void Start()
  {
    _audioSource = GetComponent<AudioSource>();
    _pooler = GetComponent<ObjectPooler>();

    PlayMusic( 0 );
  }

  // Play BGM
  public void PlayMusic( int musicIndex )
  {
    if( _audioSource == null )
    {
      return;
    }

    _audioSource.clip = mainThemes[ musicIndex ];
    _audioSource.Play();
  }

  // Plays a sound
  public void PlaySound( AudioClip clip, float volume = 1f )
  {
    // Get audio source gameobject
    GameObject newAudioSourceGO = _pooler.GetObjectFromPool();
    newAudioSourceGO.SetActive( true );

    // Get audio source from object
    AudioSource source = newAudioSourceGO.GetComponent<AudioSource>();

    // Setup audio source
    source.clip = clip;
    source.volume = volume;
    source.Play();

    StartCoroutine( IEReturnToPool( newAudioSourceGO, clip.length + 0.1f ) );
  }

  // Return on sound object back to the pool
  private IEnumerator IEReturnToPool( GameObject objectToReturn, float time )
  {
    yield return new WaitForSeconds( time );
    objectToReturn.SetActive( false );
  }
}
