using UnityEngine;
using UnityEngine.Animations;

namespace Shared {
  public class LazyHand : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField] private Transform hand;
    [SerializeField] private ParentConstraint parentConstraint;
    [SerializeField] private LazyTranslation translator;

    void Update () => translator.target = parentConstraint.constraintActive ? parentConstraint.transform : hand;
  }
}