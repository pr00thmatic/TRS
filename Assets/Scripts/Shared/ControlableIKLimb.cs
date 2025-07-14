using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace Shared {
  public class ControlableIKLimb : MonoBehaviour {
    [Header("Initialization")]
    public PlayerReferences references;
    public TwoBoneIKConstraint ikConstraint;
    public ParentConstraint parentConstraint;
    public Transform target;
    public Transform hint;
    public Transform distanceSource;
    public Transform angleSource;

    [Header("Configuration")]
    public float weightSpeed = 4;

    [Header("Information")]
    [SerializeField] private float CurrentLikelihood { get => ratioSelector.currentRatio; set => ratioSelector.currentRatio = value; }
    public float TargetWeight => CurrentController != null && ratioSelector.currentRatio > 0? CurrentController.Weight : 0;
    public MinimumRatioSelector<IIkLimbControlTaker> ratioSelector;
    public IIkLimbControlTaker CurrentController => ratioSelector.currentTarget;

    public void ReleaseControl (IIkLimbControlTaker controlTaker) {
      parentConstraint.constraintActive = false;
      if (parentConstraint.sourceCount != 0) parentConstraint.RemoveSource(0);
    }

    public void GrantControl (IIkLimbControlTaker controlTaker) {
      if (parentConstraint.sourceCount == 0)
        parentConstraint.AddSource(new ConstraintSource() { sourceTransform = controlTaker.IkTarget, weight = 1f });
      else
        parentConstraint.SetSource(0, new ConstraintSource() { sourceTransform = controlTaker.IkTarget, weight = 1f });
      parentConstraint.constraintActive = true;
      CurrentLikelihood = controlTaker.LikelihoodToControl(this);
    }

    void Update () {
      ikConstraint.weight = Mathf.MoveTowards(ikConstraint.weight, TargetWeight, weightSpeed * Time.deltaTime);
      if (CurrentController != null) {
        CurrentLikelihood = CurrentController.LikelihoodToControl(this);
      }
    }

    public void OnTriggerStay (Collider c) {
      if (ratioSelector.Compare(c, found => found.GetTargetLimb(references) == this? found.LikelihoodToControl(this) : 0))
        GrantControl(ratioSelector.currentTarget);
    }

    public void OnTriggerExit (Collider c) {
      var exitResult = ratioSelector.Exit(c);
      if (exitResult.Item1)
        ReleaseControl(exitResult.Item2);
    }
  }
}
