using UnityEngine;
using Shared.Utils;
using UnityEngine.Assertions;

namespace Shared {
  public class DoorInteractor : MonoBehaviour, IIkControlTaker {
    [Header("Initialization")]
    public DoorInteractionPoint inside;
    public DoorInteractionPoint outside;

    [Header("Configuration")]
    public HandSide insideHand;
    public HandSide outsideHand;

    [Header("Information")]
    public PlayerReferences detectedPlayer;
    public DoorInteractionPoint InteractionPoint => !detectedPlayer? null : (IsInside? inside : outside);
    public bool IsInside => detectedPlayer? transform.InverseTransformPoint(detectedPlayer.transform.position).z > 0 : false;
    public float AngleToDoor => detectedPlayer? Vector3.Angle(detectedPlayer.transform.forward, Math.Signify(IsInside) * -transform.forward) : -360;
    public IKControl TargetHand => detectedPlayer? detectedPlayer.GetHand(IsInside? insideHand : outsideHand) : null;
    public IKControl OtherHand => detectedPlayer? detectedPlayer.GetHand((IsInside? insideHand : outsideHand).GetOpposite()) : null;

    public void OnTriggerStayHandler (PlayerReferences player) {
      detectedPlayer = player;
      if (CanTakeControl(TargetHand)) RequestControl();
      else ReleaseControl();
    }

    public void OnTriggerExitHandler (PlayerReferences player) {
      ReleaseControl();
      detectedPlayer = null;
    }

    void RequestControl () {
      if (TargetHand.controller != (IIkControlTaker) this) TargetHand.RequestControl(this);
      if (OtherHand.controller == (IIkControlTaker) this) OtherHand.ReleaseControl(this);
    }

    void ReleaseControl () {
      Assert.IsTrue(detectedPlayer != null, "This door couldn't release it's IK Controls! Player might be left reaching for the knob even if away");
      detectedPlayer.rightHand.ReleaseControl(this);
      detectedPlayer.leftHand.ReleaseControl(this);
    }

    #region IIkControlTaker
    public Transform IkTarget => detectedPlayer? InteractionPoint.ikTarget : null;
    public float Weight => detectedPlayer && CanTakeControl(TargetHand)? InteractionPoint.weight : -1;

    public bool CanTakeControl (IKControl ik) => detectedPlayer && AngleToDoor < 45;
    #endregion
  }
}
