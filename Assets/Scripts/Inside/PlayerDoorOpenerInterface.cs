using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using System.Collections;
using System.Collections.Generic;

public class PlayerDoorOpenerInterface : MonoBehaviour {
  [Header("Initialization")]
  public Transform playerDetector;
  public Transform ikTarget;
  public bool isInside;

  [Header("Configuration")]
  public float ikControl;

  [Header("Information")]
  public float angle;
  public int IsInsideSignified => isInside? 1 : -1;
  public bool isActive = false;

  public void OnTriggerStayHandler (PlayerReferences player) {
    if ((IsInsideSignified * playerDetector.InverseTransformPoint(player.transform.position).z) < 0) return;

    angle = Vector3.Angle(player.transform.forward, -IsInsideSignified * playerDetector.forward);
    if (angle > 45) {
      OnTriggerExitHandler(player);
      return;
    }

    GetTargetIKConstraint(player).weight = ikControl;
    var parentConstraint = GetTargetIKConstraint(player).GetComponentInChildren<ParentConstraint>();
    parentConstraint.enabled = true;
    parentConstraint.SetSource(0, new() { sourceTransform = ikTarget, weight = 1f });
    isActive = true;
  }

  public void OnTriggerExitHandler (PlayerReferences player) {
    var parentConstraint = GetTargetIKConstraint(player).GetComponentInChildren<ParentConstraint>().enabled = false;
    GetTargetIKConstraint(player).weight = 0;
    isActive = false;
  }

  public TwoBoneIKConstraint GetTargetIKConstraint (PlayerReferences player) => isInside? player.rightArm : player.leftArm;
}
