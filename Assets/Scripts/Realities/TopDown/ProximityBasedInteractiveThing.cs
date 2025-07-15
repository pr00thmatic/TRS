using UnityEngine;
using UnityEngine.Events;
using Shared;

namespace Realities.TopDown {
  public class ProximityBasedInteractiveThing : MonoBehaviour, IInteractiveThing {
    [Header("Configuration")]
    public ProximityBasedRatioAssigner proximity;

    public float GetRatio (PlayerInteractor interactor) => proximity.GetRatio(interactor.positionSource, interactor.angleSource);

    public UnityEvent<IInteractiveThing, PlayerInteractor, bool> OnFocusChange { get; private set; } = new();
  }
}
