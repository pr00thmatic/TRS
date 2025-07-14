using UnityEngine;
using Shared;

namespace Realities.TopDown {
  public class ProximityBasedInteractiveThing : MonoBehaviour, IInteractiveThing {
    [Header("Configuration")]
    public ProximityBasedRatioAssigner proximity;

    // public float GetRatio (PlayerInteractor interactor) => proximity.GetRatio(interactor.positionSource, interactor.angleSource);
  }
}
