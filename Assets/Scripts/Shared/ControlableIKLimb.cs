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
    [SerializeField] private float currentLikelihood;
    public float TargetWeight => currentController != null? currentController.Weight : 0;
    public IIkLimbControlTaker currentController;

    public void ReleaseControl (IIkLimbControlTaker controlTaker) {
      if (currentController != controlTaker) return;
      currentController = null;
      parentConstraint.constraintActive = false;
      if (parentConstraint.sourceCount != 0) parentConstraint.RemoveSource(0);
    }

    public void GrantControl (IIkLimbControlTaker controlTaker) {
      if (currentController == controlTaker) return;
      currentController = controlTaker;
      if (parentConstraint.sourceCount == 0)
        parentConstraint.AddSource(new ConstraintSource() { sourceTransform = controlTaker.IkTarget, weight = 1f });
      else
        parentConstraint.SetSource(0, new ConstraintSource() { sourceTransform = controlTaker.IkTarget, weight = 1f });
      parentConstraint.constraintActive = true;
      currentLikelihood = controlTaker.LikelihoodToControl(this);
    }

    void Update () {
      ikConstraint.weight = Mathf.MoveTowards(ikConstraint.weight, TargetWeight, weightSpeed * Time.deltaTime);
      if (currentController != null) {
        currentLikelihood = currentController.LikelihoodToControl(this);
        if (currentLikelihood <= 0) ReleaseControl(currentController);
      }
    }

    public void OnTriggerStay (Collider c) {
      IIkLimbControlTaker foundController = c.GetComponentInParent<IIkLimbControlTaker>();
      if (foundController == null || foundController == currentController) return;
      if (foundController.GetTargetLimb(references) != this) return;
      float foundLikelihood = foundController.LikelihoodToControl(this);
      if ((currentController == null && foundLikelihood > 0) || (currentController != null && currentLikelihood < foundLikelihood))
        GrantControl(foundController);
    }

    public void OnTriggerExit (Collider c) {
      IIkLimbControlTaker foundController = c.GetComponentInParent<IIkLimbControlTaker>();
      if (foundController != null && currentController == foundController) ReleaseControl(foundController);
    }
  }
}
