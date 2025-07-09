using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class LazyAim : MonoBehaviour {
    [Header("Configuration")]
    public Vector3 targetForward;
    public float angularSpeed = 180;

    void Awake () => targetForward = transform.forward;
    void FixedUpdate () {
      if (targetForward.magnitude == 0) return;
      transform.forward = Vector3.RotateTowards(transform.forward, targetForward, angularSpeed * Time.deltaTime, 1);
    }
  }
}
