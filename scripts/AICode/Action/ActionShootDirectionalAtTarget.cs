using System;
using UnityEngine;

[ CreateAssetMenu( menuName = "AI/Actions/Action Shoot Directional At Target", fileName = "ActionShootDirectionalAtTarget" ) ]
public class ActionShootDirectionalAtTarget : AIAction
{
  public float msBetweenShots = 100f;
  private float nextShotTime;

  public override void Action( StateController controller )
  {
    FireTarget( controller );
  }

  // Fire projectiles towards the target position
  private void FireTarget( StateController controller )
  {
    if( controller.Pooler == null || controller.Target == null )
    {
      return;
    }

    if( Time.time > nextShotTime )
    {
      // Get direction to target
      Vector3 dirToTarget = controller.Target.transform.position - controller.transform.position;
      Vector3 normalizeDirToTarget = dirToTarget.normalized;

      // Get projectile from pool
      GameObject projectile = controller.Pooler.GetObjectFromPool();
      projectile.transform.position = controller.FirePoint.position;
      projectile.SetActive( true );

      // Get projectile reference
      Projectile newProjectile = projectile.GetComponent<Projectile>();
      
      // Rotate projectile by 180 degrees
      Vector3 PlayerToEnemy = controller.Target.transform.position - controller.transform.position;
      float xDifference = PlayerToEnemy.x;
      
      if( xDifference < 0 )
      {
        newProjectile.SetDirection( new Vector3( - normalizeDirToTarget.x, normalizeDirToTarget.y, 0f ) );
        newProjectile.transform.eulerAngles = new Vector3( 0, 180, 0 );
      }
      else
      {
        newProjectile.SetDirection( new Vector3( normalizeDirToTarget.x, normalizeDirToTarget.y, 0f ) );
        newProjectile.transform.eulerAngles = new Vector3( 0, 0, 0 );
      }
      

      newProjectile.EnableProjectile();

      SoundManager.instance.PlaySound( AudioLibrary.instance.EnemyProjectileClip );

      // Update shot time
      nextShotTime = Time.time + msBetweenShots / 100f;
    }
  }

  private void OnEnable()
  {
    nextShotTime = 0f;
  }
}
