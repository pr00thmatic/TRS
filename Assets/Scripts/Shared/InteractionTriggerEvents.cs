using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class InteractionTriggerEvents : MonoBehaviour {
    [field: SerializeField] public UnityEvent<PlayerReferences> TriggerEnter { get; private set; } = new();
    [field: SerializeField] public UnityEvent<PlayerReferences> TriggerStay { get; private set; } = new();
    [field: SerializeField] public UnityEvent<PlayerReferences> TriggerExit { get; private set; } = new();

    void OnTriggerEnter (Collider c) => TriggerEnter.Invoke(c.GetComponentInParent<PlayerReferences>());
    void OnTriggerStay (Collider c) => TriggerStay.Invoke(c.GetComponentInParent<PlayerReferences>());
    void OnTriggerExit (Collider c) => TriggerExit.Invoke(c.GetComponentInParent<PlayerReferences>());
  }
}
