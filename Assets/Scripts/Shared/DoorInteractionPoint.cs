using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using Shared;

namespace Shared {
  public class DoorInteractionPoint : MonoBehaviour {
    [Header("Initialization")]
    public Transform playerDetector;
    public Transform ikTarget;

    [Header("Configuration")]
    public float weight;
  }
}
