using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shared;

namespace Realities.TopDown {
  public class TemporalDoorTester : MonoBehaviour {
    public ProximityBasedInteractiveThing interactive;
    public GameObject focusedIndicator;

    void OnEnable () {
      interactive.OnFocusChange.AddListener(HandleFocusChange);
    }
    void OnDisable () {
      interactive.OnFocusChange.RemoveListener(HandleFocusChange);
    }

    public void HandleFocusChange (IInteractiveThing interactive, PlayerInteractor interactor, bool isFocused) {
      Debug.Log("is focused? " + isFocused);
      focusedIndicator.SetActive(isFocused);
    }
  }
}
