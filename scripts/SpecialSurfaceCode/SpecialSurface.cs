using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSurface : MonoBehaviour
{
  [ Header( "Settings" ) ]
  [ SerializeField ] [ Range( 0, 1 ) ] private float friction = 0.125f;
  [ SerializeField ] [ Range( 0, 1 ) ] private float reduceSpeedMultiplier = 0.5f;
  [ SerializeField ] private int mitigateDamage = 0;

  public float Friction => friction;
  public float ReducedSpeed => reduceSpeedMultiplier;
  public int MitigateDamage => mitigateDamage;
}
