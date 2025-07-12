using UnityEngine;

namespace Shared
{
  public interface IIkLimbControlTaker {
    public Transform IkTarget { get; }

    // a number that tells how much influence to exercise onto the ik limb.
    // Usually 1 and doesn't require smoothing coz the limb already does it.
    // but for things with animations (like the door) it is useful for the control taker to provide different values
    // depending on its animation
    public float Weight { get; }

    // a number from 0 to 1 where 0 is no likelihood and 1 is no doubt.
    // usually a convination of distance and angle between the object and the IK trying to reach it.
    public float LikelihoodToControl (ControlableIKLimb control);
    public ControlableIKLimb GetTargetLimb (PlayerReferences player);
  }
}
