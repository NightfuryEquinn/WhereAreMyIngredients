using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject
{
  // Override to add custom behaviour
  public abstract void Action( StateController controller );
}