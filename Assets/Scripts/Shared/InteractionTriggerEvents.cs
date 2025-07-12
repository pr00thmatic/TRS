using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class InteractionTriggerEvents : MonoBehaviour {
    [field: SerializeField] public UnityEvent<Collider> TriggerEnter { get; private set; } = new();
    [field: SerializeField] public UnityEvent<Collider> TriggerStay { get; private set; } = new();
    [field: SerializeField] public UnityEvent<Collider> TriggerExit { get; private set; } = new();

    void OnTriggerEnter (Collider c) => TriggerEnter.Invoke(c);
    void OnTriggerStay (Collider c) => TriggerStay.Invoke(c);
    void OnTriggerExit (Collider c) => TriggerExit.Invoke(c);
  }
}
