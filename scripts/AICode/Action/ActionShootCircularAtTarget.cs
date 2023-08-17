using System;
using UnityEngine;

[ CreateAssetMenu( menuName = "AI/Actions/Action Shoot Circular At Target", fileName = "ActionShootCircularAtTarget" ) ]
public class ActionShootCircularAtTarget : AIAction
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
      
      newProjectile.SetDirection( new Vector3( normalizeDirToTarget.x, normalizeDirToTarget.y, 0f ) );

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
