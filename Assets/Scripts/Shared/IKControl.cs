using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace Shared {
  public class IKControl : MonoBehaviour {
    [Header("Initialization")]
    public IIkControlTaker controller;
    public TwoBoneIKConstraint ikConstraint;
    public ParentConstraint parentConstraint;
    public Transform target;
    public Transform hint;

    [Header("Configuration")]
    public float weightSpeed = 4;

    public void ReleaseControl (IIkControlTaker controlTaker) {
      if (controller != controlTaker) return;
      controller = null;
      parentConstraint.constraintActive = false;
      if (parentConstraint.sourceCount != 0) parentConstraint.RemoveSource(0);
    }

    public void RequestControl (IIkControlTaker controlTaker) {
      if (controller == controlTaker) return;
      controller = controlTaker;
      if (parentConstraint.sourceCount == 0)
        parentConstraint.AddSource(new ConstraintSource() { sourceTransform = controlTaker.IkTarget, weight = 1f });
      else
        parentConstraint.SetSource(0, new ConstraintSource() { sourceTransform = controlTaker.IkTarget, weight = 1f });
      parentConstraint.constraintActive = true;
    }

    void Update () {
      ikConstraint.weight = Mathf.MoveTowards(ikConstraint.weight, controller != null? controller.Weight : 0, weightSpeed * Time.deltaTime);
    }
  }
}
