using UnityEngine;

namespace Shared {
  [System.Serializable]
  public class BestRatioSelector<T> {
    public T currentTarget;
    public float currentRatio;

    // returns true if the target has changed
    public bool Compare (Collider c, System.Func<T, float> RatioGetter) {
      T found = c.GetComponentInParent<T>();
      if (found == null || found.Equals(currentTarget)) return false;

      float foundRatio = RatioGetter.Invoke(found);
      if ((currentTarget == null && foundRatio > 0) || (currentTarget != null && currentRatio < foundRatio)) {
        currentTarget = found;
        return true;
      }

      return false;
    }

    // returns (true if this exit caused the currentTarget to be cleared, the last thing that was currentTarget)
    public (bool, T) Exit (Collider c) {
      T found = c.GetComponentInParent<T>();
      if (found != null && currentTarget != null && currentTarget.Equals(found)) {
        var leftTarget = currentTarget;
        currentTarget = default;
        currentRatio = 0;
        return (true, leftTarget);
      }

      return (false, currentTarget);
    }
  }
}