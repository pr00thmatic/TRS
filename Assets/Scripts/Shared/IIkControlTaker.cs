using UnityEngine;

namespace Shared
{
  public interface IIkControlTaker
  {
      public Transform IkTarget { get; }
      public float Weight { get; }

      public bool CanTakeControl (IKControl ik);
    }
}