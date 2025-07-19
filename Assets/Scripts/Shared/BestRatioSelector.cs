using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Shared {
  /// <summary>
  /// The generic type T must be implemented by MonoBehaviour in order to work with the BestRatioSelector
  /// </summary>
  [System.Serializable]
  public class BestRatioSelector<T> where T : class {
    public T currentTarget;
    public float currentRatio;

    // returns true if the target has changed
    public bool Compare (Collider c, System.Func<T, float> RatioGetter) {
      T found = c.GetComponentInParent<T>();
      Assert.IsTrue(typeof(T).IsInterface || typeof(T).IsAssignableFrom(typeof(MonoBehaviour)), "T has to be an implementation of MonoBehaviour! (or an interface... just make sure those interfaces are implementations of MonoBehaviours as well");

      if (found == null || found == currentTarget) return false;

      float foundRatio = RatioGetter.Invoke(found);
      if ((currentTarget == null && foundRatio > 0) || (currentTarget != null && currentRatio < foundRatio)) {
        var lastTarget = currentTarget;
        currentTarget = found;
        if (lastTarget != null) OnSelectionLost?.Invoke(lastTarget);
        OnSelectionAcquired?.Invoke(currentTarget);
        return true;
      }

      return false;
    }

    // returns (true if this exit caused the currentTarget to be cleared, the last thing that was currentTarget)
    public (bool, T) Exit (Collider c) {
      T found = c.GetComponentInParent<T>();
      Assert.IsTrue(typeof(T).IsInterface || typeof(T).IsAssignableFrom(typeof(MonoBehaviour)), "T has to be an implementation of MonoBehaviour! (or an interface... just make sure those interfaces are implementations of MonoBehaviours as well");

      if (found != null && currentTarget != null && currentTarget == found) {
        var leftTarget = currentTarget;
        currentTarget = default;
        currentRatio = 0;
        if (leftTarget != null) OnSelectionLost?.Invoke(leftTarget);
        return (true, leftTarget);
      }

      return (false, currentTarget);
    }

    public UnityEvent<T> OnSelectionLost { get; private set; } = new();
    public UnityEvent<T> OnSelectionAcquired { get; private set; } = new();
  }
}
