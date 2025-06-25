using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InsidePlayerMotion : MonoBehaviour {
  public InsideSurveillanceSystem surveillanceSystem;
  public float speed = 1;
  public float animSpeed = 0.5f;
  public float currentSpeed = 0;
  public Animator animator;
  Vector3 direction;

  void FixedUpdate () {
    direction = (Input.GetAxis("Horizontal") * surveillanceSystem.Right + Input.GetAxis("Vertical") * surveillanceSystem.Forward);
    currentSpeed = (direction * speed).magnitude;
    transform.position += direction * speed * Time.deltaTime;

    if (currentSpeed > 0.01) transform.forward = direction;

    animator.SetFloat("speed", currentSpeed / animSpeed);
  }
}
