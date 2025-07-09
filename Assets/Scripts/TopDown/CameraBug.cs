using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Shared;

namespace Realities.TopDown {
  public class CameraBug : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField] private Camera camera;
    [SerializeField] private Transform targetFollow;
    [SerializeField] private Transform targetViewAvoidance;
    [SerializeField] private LazyAim aimer;

    // a more sofisticated set might be needed in the future
    public Transform TargetFollow { get => targetFollow; set => targetFollow = value; }

    [Header("Configuration")]
    public float maxPerceptionRangeDegrees = 70; // yes! can be tweaked by other scripts
    public float maxRadianRotationSpeed = 1.5f; // this one too
    public float degAngleToSpeedMax = 45;
    public Vector2 maximumViewportDistanceTolerated = new(0.2f, 0.2f);
    public float overshootTime = 0.2f;
    public float minimumRadianRotationSpeed = 0.05f;

    [Header("Information")]
    [SerializeField] private float angleToViewer;
    [SerializeField] private float lastTimeOutsideFrustrum;

    public Vector2 TrackedViewportPointDistance => camera.WorldToViewportPoint(TargetFollow.position) - new Vector3(0.5f, 0.5f);
    public bool IsBeingSeen => angleToViewer < maxPerceptionRangeDegrees;
    public bool IsInsideFrustrum =>
      Mathf.Abs(TrackedViewportPointDistance.x) < maximumViewportDistanceTolerated.x &&
      Mathf.Abs(TrackedViewportPointDistance.y) < maximumViewportDistanceTolerated.y;

    public float CameraSpeed {
      get {
        if (IsInsideFrustrum) {
          if (Time.time - lastTimeOutsideFrustrum > overshootTime) return 0;
        } else lastTimeOutsideFrustrum = Time.time;

        float speed;
        if (IsBeingSeen && !IsInsideFrustrum) speed = minimumRadianRotationSpeed;
        else speed = Mathf.Clamp((angleToViewer - maxPerceptionRangeDegrees) / degAngleToSpeedMax, 0, 1) * maxRadianRotationSpeed;
        return Mathf.Max(minimumRadianRotationSpeed, speed);
      }
    }

    void Start () => lastTimeOutsideFrustrum = -100;
    void Update () {
      angleToViewer = Vector3.Angle(targetViewAvoidance.position - camera.transform.position, -targetViewAvoidance.forward);
      aimer.targetForward = TargetFollow.position - transform.position;
      aimer.angularSpeed = CameraSpeed;
    }
  }
}
