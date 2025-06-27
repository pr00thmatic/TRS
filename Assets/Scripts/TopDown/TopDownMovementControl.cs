using UnityEngine;
using Shared;

namespace Realities.TopDown {
  [RequireComponent(typeof(LazyAim))]
  public class TopDownMovementControl : MonoBehaviour {
    [Header("Initialization")]
    public SurveillanceSystem surveillanceSystem;
    public Animator animator;
    public LazyAim aimer;

    [Header("Configuration")]
    public float speed = 1;
    public float animSpeed = 0.5f;

    [Header("Information")]
    public Vector3 direction;
    public Vector3 lastPosition; // to prevent it from doing the walk animation when walking into a wall
    public float currentSpeed;

    void Start () => lastPosition = transform.position;

    void OnValidate () {
      animator = GetComponentInChildren<Animator>();
      surveillanceSystem = FindFirstObjectByType<SurveillanceSystem>();
      aimer = GetComponent<LazyAim>();
    }

    void FixedUpdate () {
      direction = Vector3.ClampMagnitude((Input.GetAxis("Horizontal") * surveillanceSystem.Right +
                                          Input.GetAxis("Vertical") * surveillanceSystem.Forward), 1);
      transform.position += direction * speed * Time.deltaTime;
      currentSpeed = (transform.position - lastPosition).magnitude / Time.deltaTime;
      animator.SetFloat("speed", currentSpeed * animSpeed);

      if (direction.magnitude != 0) aimer.targetForward = direction;
      lastPosition = transform.position;
    }
  }
}
