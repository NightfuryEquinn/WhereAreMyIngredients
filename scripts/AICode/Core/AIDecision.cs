using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : ScriptableObject
{
  // Override to add custom behaviour
  public abstract bool Decide( StateController controller );
}