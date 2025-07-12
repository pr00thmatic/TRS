using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class ProximityBasedLimbTaker : MonoBehaviour, IIkLimbControlTaker {
    [Header("Initialization")]
    [field: SerializeField] public Transform IkTarget { get; private set; }

    [Header("Configuration")]
    [SerializeField] private float angle = 30;
    [SerializeField] private float distance = 0.6f;
    [SerializeField] private HandSide targetHand;

    [Header("Information")]
    public float Weight => 1;

    public float LikelihoodToControl (ControlableIKLimb limb) {
      Vector3 distanceVector = IkTarget.position - limb.distanceSource.position;
      Vector3 angleVector = Utils.Vectors.SetY(distanceVector, 0);
      float distanceRatio = 1 - Mathf.Clamp(distanceVector.magnitude / distance, 0, 1);
      float angleRatio = 1 - Mathf.Clamp(Vector3.Angle(limb.angleSource.forward, angleVector.normalized) / angle, 0, 1);

      return angleRatio <= 0 || distanceRatio <= 0? Mathf.Min(angleRatio, distanceRatio) : (angleRatio + distanceRatio) / 2f;
    }

    public ControlableIKLimb GetTargetLimb (PlayerReferences player) => player.GetHand(targetHand);
  }
}
