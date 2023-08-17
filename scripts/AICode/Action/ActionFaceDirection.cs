using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( menuName = "AI/Actions/Action Face Direction", fileName = "ActionFaceDirection" ) ]
public class ActionFaceDirection : AIAction
{
  public override void Action( StateController controller )
  {
    FaceDirection( controller );
  }

  // Face the direction of our movement
  private void FaceDirection( StateController controller )
  {
    if( controller.RefPath != null )
    {
      if( controller.RefPath.Direction == Path.MoveDirections.RIGHT )
      {
        controller.transform.localScale = new Vector3( 1, 1, 1 );
      }
      else
      {
        controller.transform.localScale = new Vector3( -1, 1, 1 );
      }
    }
  }
}
