using UnityEngine;
using Shared;

namespace Realities.TopDown {
  public class PlayerInteractor : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField] public Transform positionSource;
    [SerializeField] public Transform angleSource;

    [Header("Configuration")]
    public BestRatioSelector<IInteractiveThing> selector;

    void OnEnable () {
      selector.OnSelectionLost.AddListener(HandleSelectionLost);
      selector.OnSelectionAcquired.AddListener(HandleSelectionAcquired);
    }

    void OnDisable () {
      selector.OnSelectionLost.RemoveListener(HandleSelectionLost);
      selector.OnSelectionAcquired.RemoveListener(HandleSelectionAcquired);
    }

    private void Update () {
      if (selector.currentTarget != null)
        selector.currentRatio = selector.currentTarget.GetRatio(this);
    }

    public void OnTriggerStay (Collider c) =>
      selector.Compare(c, interactive => interactive.GetRatio(this));

    public void OnTriggerExit (Collider c) => selector.Exit(c);

    public void HandleSelectionLost (IInteractiveThing interactiveThing) =>
      interactiveThing.OnFocusChange?.Invoke(interactiveThing, this, false);

    public void HandleSelectionAcquired (IInteractiveThing interactiveThing) =>
      interactiveThing.OnFocusChange?.Invoke(interactiveThing, this, true);
  }
}
