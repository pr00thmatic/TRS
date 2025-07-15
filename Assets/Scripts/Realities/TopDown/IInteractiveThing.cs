using UnityEngine.Events;

namespace Realities.TopDown {
  public interface IInteractiveThing {
    public float GetRatio (PlayerInteractor interactor);
    public UnityEvent<IInteractiveThing, PlayerInteractor, bool> OnFocusChange { get; }
  }
}
