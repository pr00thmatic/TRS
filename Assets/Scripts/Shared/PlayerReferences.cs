using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class PlayerReferences : MonoBehaviour {
    public ControlableIKLimb leftHand;
    public ControlableIKLimb rightHand;

    public ControlableIKLimb GetHand (HandSide hand) => hand == HandSide.Right ? rightHand : leftHand;
  }
}
