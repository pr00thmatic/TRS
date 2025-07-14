using UnityEngine;

namespace Shared {
  public class ProximityBasedLimbTaker : MonoBehaviour, IIkLimbControlTaker {
    [Header("Initialization")]
    [field: SerializeField] public Transform IkTarget { get; private set; }

    [Header("Configuration")]
    public ProximityBasedRatioAssigner proximitySensor;
    [SerializeField] private HandSide targetHand;

    [Header("Information")]
    public float Weight => 1;

    public float LikelihoodToControl (ControlableIKLimb limb) =>
      proximitySensor.GetRatio(limb.distanceSource, limb.angleSource);

    public ControlableIKLimb GetTargetLimb (PlayerReferences player) => player.GetHand(targetHand);
  }
}
