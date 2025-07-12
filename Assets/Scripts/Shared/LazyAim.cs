using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public abstract class LazyAim : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField] protected Transform target;
    public Transform Target => target? target : transform;

    [Header("Configuration")]
    public Vector3 targetForward;
    public float angularSpeed = 180;

    void Awake () => targetForward = Target.forward;
    protected void PerformUpdate () {
      if (targetForward.magnitude == 0) return;
      Target.forward = Vector3.RotateTowards(Target.forward, targetForward, angularSpeed * Time.deltaTime, 1);
    }
  }
}
