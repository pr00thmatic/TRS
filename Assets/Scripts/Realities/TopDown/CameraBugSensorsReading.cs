using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Realities.TopDown {
  [System.Serializable]
  public class CameraBugSensorsReading {
    public Vector3 targetForward;
    public float targetAngularSpeed;
    public float readingTimestamp;

    public CameraBugSensorsReading (CameraBug camera) {
      targetForward = camera.RealTimeTargetForward;
      targetAngularSpeed = camera.RealTimeTargetSpeed;
      readingTimestamp = Time.time;
    }
  }
}
