using UnityEngine;

public class LevelComponent : MonoBehaviour, Damageable
{
  [ Header( "Settings" ) ]
  [ SerializeField ] protected bool instantKill;
  [ SerializeField ] protected int loseAmount = 5;

  public virtual void Damage( PlayerMotor player )
  {
    int parseLoseAmount = loseAmount;

    if( player != null )
    {
      if( instantKill )
      {
        player.GetComponent<PlayerHealth>().KillPlayer();
      }
      else
      {
        player.GetComponent<PlayerHealth>().LoseLife( parseLoseAmount );
      }
    }
  }
}