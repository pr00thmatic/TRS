using UnityEngine;

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
      float distanceRatio = 1 - Mathf.Clamp01(distanceVector.magnitude / distance);
      float angleRatio = 1 - Mathf.Clamp01(Vector3.Angle(angleSource.forward, angleVector.normalized) / angle);
      return angleRatio <= 0 || distanceRatio <= 0? Mathf.Min(angleRatio, distanceRatio) : (angleRatio + distanceRatio) / 2f;
    }
  }
}
