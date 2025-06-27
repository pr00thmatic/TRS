using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class PlayerReferences : MonoBehaviour {
    public IKControl leftHand;
    public IKControl rightHand;

    public IKControl GetHand (HandSide hand) => hand == HandSide.Right ? rightHand : leftHand;
  }
}
