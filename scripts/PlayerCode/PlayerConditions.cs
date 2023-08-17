using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions
{
  public bool isCollidingAbove { get; set; }
  public bool isCollidingRight { get; set; }
  public bool isCollidingBelow { get; set; }
  public bool isCollidingLeft { get; set; }
  public bool isFalling { get; set; }
  public bool isJumping { get; set; }

  public void Reset()
  {
    isCollidingAbove = false;
    isCollidingRight = false;
    isCollidingBelow = false;
    isCollidingLeft = false;

    isFalling = false;
  }
}
