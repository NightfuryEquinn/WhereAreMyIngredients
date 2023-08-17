using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu( menuName = "AI/State" ) ]
public class AIState : ScriptableObject
{ 
  public AIAction[] Actions;
  public AITransition[] Transitions;

  // Run this state
  public void RunState( StateController controller )
  {
    ExecuteActions( controller );
    EvaluateTransitions( controller );
  }

  // Calls all actions methods
  public void ExecuteActions( StateController controller )
  {
    foreach( AIAction action in Actions )
    {
      action.Action( controller );
    }
  }

  // Checks every frame if we meet certain condition
  // To transition to another state
  public void EvaluateTransitions( StateController controller )
  {
    if( Transitions != null || Transitions.Length > 0 )
    {
      for( int i = 0; i < Transitions.Length; i++ )
      {
        bool decisionValue = Transitions[ i ].Decision.Decide( controller );
        if( decisionValue )
        {
          controller.TransitionToState( Transitions[ i ].TrueState );
        }
        else
        {
          controller.TransitionToState( Transitions[ i ].FalseState );
        }
      }
    }
  }
}