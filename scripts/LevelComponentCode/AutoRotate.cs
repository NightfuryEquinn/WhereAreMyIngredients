using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
  [ Header( "Setting" ) ]
  [ SerializeField ] private Vector3 rotateAxis;
  [ SerializeField ] private float speed = 100f;
  [ SerializeField ] private float maxRotationDegree = 90f;
  [ SerializeField ] private float smoothRatio = 5f;


  private float currentRotationDegree = 0f;
  private bool rotateForward = false;

  private void Update()
  {
    Rotate();
  }

  private void Rotate()
  {
    // Calculate new rotation amount
    float rotationAmount = speed * Time.deltaTime * ( rotateForward ? 1f : -1f );

    // If next rotation exceeds the max rotation angle, cap it
    if( Mathf.Abs( currentRotationDegree + rotationAmount ) > maxRotationDegree )
    {
      rotationAmount = maxRotationDegree - Mathf.Abs( currentRotationDegree );
      rotateForward = !rotateForward;
    }

    Quaternion targetRotation = Quaternion.Euler( rotateAxis * ( currentRotationDegree + rotationAmount ) );

    // Smoothly interpolate between the current rotation and the target rotation
    transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, Time.deltaTime * smoothRatio );

    // Update the current rotation angle
    currentRotationDegree += rotationAmount;
  }
}
