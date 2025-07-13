using UnityEngine;
using UnityEngine.InputSystem;
using Shared;

namespace Realities.TopDown {
  public class TopDownMovementControl : MonoBehaviour {
    public const float DISTANCE_EPSILON = 0.1f;

    [Header("Initialization")]
    [SerializeField] private SurveillanceSystem surveillanceSystem;
    [SerializeField] private Animator animator;
    [SerializeField] private LazyAim lazyAim;
    [SerializeField] private CharacterController controller;

    [Header("Configuration")]
    [SerializeField] private float speed = 2;
    [SerializeField] private float animationSmoothnessSpeed = 3;

    [Header("Information")]
    [SerializeField] private Vector3 commandedDirection;

    public float AnimationSpeed { get => animator.GetFloat("speed"); set => animator.SetFloat("speed", value); }
    public float CharacterSpeed => controller.velocity.magnitude;
    public float DesiredSpeed => commandedDirection.magnitude * speed;

    void Reset () {
      animator = GetComponentInChildren<Animator>();
      surveillanceSystem = FindFirstObjectByType<SurveillanceSystem>();
      lazyAim = GetComponent<LazyAim>();
      controller = GetComponent<CharacterController>();
    }

    void FixedUpdate () {
      controller.Move(controller.transform.forward * DesiredSpeed * Time.fixedDeltaTime);
    }

    void OnMove (InputValue inputValue) {
      Vector2 value = inputValue.Get<Vector2>();
      commandedDirection = Vector3.ClampMagnitude(value.x * surveillanceSystem.Right +
                                                  value.y * surveillanceSystem.Forward, 1);
    }

    void Update () {
      if (DesiredSpeed > DISTANCE_EPSILON) lazyAim.targetForward = commandedDirection.normalized;
      AnimationSpeed = Mathf.MoveTowards(AnimationSpeed, CharacterSpeed, animationSmoothnessSpeed * Time.deltaTime);
    }
  }
}
