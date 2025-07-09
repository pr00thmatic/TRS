using UnityEngine;
using System.Collections.Generic;
using Shared;

namespace Realities.TopDown {
  public class CameraBug : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField] private Camera camera;
    [SerializeField] private Transform targetFollow;
    [SerializeField] private Transform targetViewAvoidance;
    [SerializeField] private LazyAim aimer;

    [Header("Configuration")]
    public float maxPerceptionRangeDegrees = 70; // yes! can be tweaked by other scripts
    public float maxRadianRotationSpeed = 1.5f; // this one too
    public Vector2 maximumViewportDistanceTolerated = new(0.2f, 0.2f);
    public float lag = 0.5f;
    public float infoUpdateRate = 0.5f;
    public float minimumRadianRotationSpeed = 0.05f;

    [Header("Information")]
    [SerializeField] private float AngleToViewer =>
    Vector3.Angle(targetViewAvoidance.position - camera.transform.position, -targetViewAvoidance.forward);
    private Queue<CameraBugSensorsReading> readings = new(); // i'd love to visualize this bad boi... but i don't own Odin ;n;
    [SerializeField] private float timeSinceLastUpdate;
    [SerializeField] private CameraBugSensorsReading aknowledgedRead;
    [SerializeField] private int bufferSize;

    public CameraBugSensorsReading PeekReading => readings.Count > 0? readings.Peek() : null;
    public Vector2 TrackedViewportPointDistance =>
      Utils.Vectors.Abs(camera.WorldToViewportPoint(targetFollow.position) - new Vector3(0.5f, 0.5f));
    public bool IsBeingSeen => AngleToViewer < maxPerceptionRangeDegrees;
    public bool IsInsideFrustrum => Utils.Vectors.SmallerByComponent(TrackedViewportPointDistance, maximumViewportDistanceTolerated);
    public Vector3 RealTimeTargetForward => targetFollow.position - transform.position;
    public float RealTimeTargetSpeed => IsInsideFrustrum? 0 : IsBeingSeen? minimumRadianRotationSpeed : maxRadianRotationSpeed;

    void Start () {
      aknowledgedRead = new(this);
      aknowledgedRead.targetAngularSpeed = 0; // I don't want the first read to be acted upon... must wait for the lag!!
    }

    void Update () {
      bufferSize = readings.Count;
      if (Time.time - timeSinceLastUpdate > infoUpdateRate) {
        readings.Enqueue(new CameraBugSensorsReading(this));
        timeSinceLastUpdate = Time.time;
      }

      if (PeekReading != null && Time.time - PeekReading.readingTimestamp > lag) {
        aknowledgedRead = readings.Dequeue();
      }

      aimer.targetForward = aknowledgedRead.targetForward;
      aimer.angularSpeed = aknowledgedRead.targetAngularSpeed;
    }
  }
}
