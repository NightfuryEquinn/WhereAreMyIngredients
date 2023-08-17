using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( menuName = "AI/Decisions/Decision Line Detection", fileName = "DecisionLineDetection" ) ]
public class DecisionLineDetection : AIDecision
{
  public LayerMask playerMask;
  public float rayLength = 1.5f;

  public override bool Decide( StateController controller )
  {
    return DetectPlayer( controller );
  }

  // Returns if the object detected the player
  private bool DetectPlayer( StateController controller )
  {
    Vector3 dir = controller.RefPath.Direction == Path.MoveDirections.RIGHT ? Vector3.right : Vector3.left;
    
    RaycastHit2D hit = Physics2D.Raycast( controller.transform.position, dir, rayLength, playerMask );
    controller.DebugRay( rayLength, controller.transform.position, dir, hit );

    if( hit )
    {
      controller.Target = hit.collider.GetComponent<PlayerMotor>();
      return true;
    }

    controller.Target = null;
    return false;
  }
}
