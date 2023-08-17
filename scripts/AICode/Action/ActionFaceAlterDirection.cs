using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( menuName = "AI/Actions/Action Face Alter Direction", fileName = "ActionFaceAlterDirection" ) ]
public class ActionFaceAlterDirection : AIAction
{
  public override void Action( StateController controller )
  {
    FaceAlterDirection( controller );
  }

  // Face the direction of our movement
  private void FaceAlterDirection( StateController controller )
  {
    if( controller.RefPath != null )
    {
      if( controller.RefPath.Direction == Path.MoveDirections.RIGHT )
      {
        controller.transform.localScale = new Vector3( -1, 1, 1 );
      }
      else
      {
        controller.transform.localScale = new Vector3( 1, 1, 1 );
      }
    }
  }
}
