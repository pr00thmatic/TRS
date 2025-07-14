using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  [System.Serializable]
  public class ProximityBasedRatioAssigner {
    [Header("Initialization")]
    public Transform positionTarget;

    [Header("Configuration")]
    [SerializeField] private float angle = 30;
    [SerializeField] private float distance = 0.6f;
    [SerializeField] private bool ignoreHeight = true;

    public float GetRatio (Transform distanceSource, Transform angleSource) {
      Vector3 distanceVector = positionTarget.position - distanceSource.position;
      if (ignoreHeight) distanceVector.y = 0;
      Vector3 angleVector = distanceVector;
      if (ignoreHeight) angleVector.y = 0;
      float distanceRatio = 1 - Mathf.Clamp(distanceVector.magnitude / distance, 0, 1);
      float angleRatio = 1 - Mathf.Clamp(Vector3.Angle(angleSource.forward, angleVector.normalized) / angle, 0, 1);
      return angleRatio <= 0 || distanceRatio <= 0? Mathf.Min(angleRatio, distanceRatio) : (angleRatio + distanceRatio) / 2f;
    }
  }
}
