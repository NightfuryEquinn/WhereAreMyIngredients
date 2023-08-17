using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : Singleton<AudioLibrary>
{
  [ Header( "Clips" ) ]
  [ SerializeField ] private AudioClip jumpClip;
  [ SerializeField ] private AudioClip walkClip;
  [ SerializeField ] private AudioClip hurtClip;
  [ SerializeField ] private AudioClip deathClip;

  [ SerializeField ] private AudioClip lightAttack1Clip;
  [ SerializeField ] private AudioClip lightAttack2Clip;
  [ SerializeField ] private AudioClip lightAttack3Clip;

  [ SerializeField ] private AudioClip heavyAttack1Clip;
  [ SerializeField ] private AudioClip heavyAttack2Clip;

  [ SerializeField ] private AudioClip quickChantClip;
  [ SerializeField ] private AudioClip heavyChantClip;
  [ SerializeField ] private AudioClip meleeChantClip;

  [ SerializeField ] private AudioClip projectileCollisionClip;

  [ SerializeField ] private AudioClip enemyProjectileClip;
  [ SerializeField ] private AudioClip enemyDeathClip;

  [ SerializeField ] private AudioClip collectablesCollectedClip;

  public AudioClip JumpClip => jumpClip;
  public AudioClip WalkClip => walkClip;
  public AudioClip HurtClip => hurtClip;
  public AudioClip DeathClip => deathClip;

  public AudioClip LightAttack1Clip => lightAttack1Clip;
  public AudioClip LightAttack2Clip => lightAttack2Clip;
  public AudioClip LightAttack3Clip => lightAttack3Clip;

  public AudioClip HeavyAttack1Clip => heavyAttack1Clip;
  public AudioClip HeavyAttack2Clip => heavyAttack2Clip;

  public AudioClip QuickChantClip => quickChantClip;
  public AudioClip HeavyChantClip => heavyChantClip;
  public AudioClip MeleeChantClip => meleeChantClip;

  public AudioClip ProjectileCollisionClip => projectileCollisionClip;

  public AudioClip EnemyProjectileClip => enemyProjectileClip;
  public AudioClip EnemyDeathClip => enemyDeathClip;

  public AudioClip CollectablesCollectedClip => collectablesCollectedClip;
}
