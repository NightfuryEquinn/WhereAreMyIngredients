using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
  [ Header( "Setting" ) ]
  [ SerializeField ] private Transform spawnPoint;

  public Transform SpawnPoint => spawnPoint;
}
