using UnityEngine;
using UnityEngine.AI;
using Shared;

namespace Realities.TopDown {
  public partial class TopDownMovementControl : MonoBehaviour {
    public const float DISTANCE_EPSILON = 0.1f;

    [Header("Initialization")]
    public SurveillanceSystem surveillanceSystem;
    public Animator animator;
    public LazyAim lazyAim;
    public NavMeshAgent agent;

    [Header("Configuration")]
    public float speed = 2;
    public float animationSmoothnessSpeed = 3;

    [Header("Information")]
    public Vector3 commandedDirection;
    public float desiredSpeed;
    public float characterSpeed;
    public float animationSpeed;
    public Vector3 lastPosition;

    void Reset () {
      animator = GetComponentInChildren<Animator>();
      surveillanceSystem = FindFirstObjectByType<SurveillanceSystem>();
      lazyAim = GetComponent<LazyAim>();
      agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate () {
      lastPosition = agent.transform.position;
      agent.Move(agent.transform.forward * desiredSpeed * Time.deltaTime);
      characterSpeed = (agent.transform.position - lastPosition).magnitude / Time.deltaTime;
    }

    void Update () {
      commandedDirection = Vector3.ClampMagnitude(Input.GetAxis("Horizontal") * surveillanceSystem.Right +
                                                  Input.GetAxis("Vertical") * surveillanceSystem.Forward, 1);
      desiredSpeed = commandedDirection.magnitude * speed;
      if (desiredSpeed > DISTANCE_EPSILON) lazyAim.targetForward = commandedDirection.normalized;

      animationSpeed = Mathf.MoveTowards(animationSpeed, characterSpeed, animationSmoothnessSpeed * Time.deltaTime);
      animator.SetFloat("speed", animationSpeed);
    }
  }
}
