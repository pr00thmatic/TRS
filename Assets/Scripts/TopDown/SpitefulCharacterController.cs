using UnityEngine;
using Shared;
using Realities.TopDown;

[RequireComponent(typeof(CharacterController))]
public class SpitefulCharacterController : MonoBehaviour
{
  public SurveillanceSystem surveillanceSystem;
  public LazyAim aimer;
  private CharacterController _controller;
  public float moveSpeed = 5f;

  // Public for easy tweaking in Inspector during testing
  public float skinWidth = 0.08f; 
  public float minMoveDistance = 0.001f; // Try very small or 0
  public float stepOffset = 0.3f;
  public float slopeLimit = 45f;

  void Awake()
  {
    _controller = GetComponent<CharacterController>();
    // _controller.skinWidth = skinWidth; // Assign here or let Inspector override
    // _controller.minMoveDistance = minMoveDistance;
    // _controller.stepOffset = stepOffset;
    // _controller.slopeLimit = slopeLimit;
  }

  void FixedUpdate()
  {
    Vector3 moveDirection = Vector3.ClampMagnitude((Input.GetAxis("Horizontal") * surveillanceSystem.Right +
                                                    Input.GetAxis("Vertical") * surveillanceSystem.Forward), 1);
    if (moveDirection.magnitude > 0.1f) aimer.targetForward = moveDirection;

    // Apply movement. CharacterController.Move expects a displacement.
    // No gravity or vertical logic for now, just horizontal.
    _controller.Move(transform.forward * moveSpeed * Time.deltaTime * moveDirection.magnitude);
  }
}
