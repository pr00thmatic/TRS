using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Realities.TopDown {
  public class SurveillanceSystem : MonoBehaviour {
    public Camera currentCamera;
    public Vector3 Right => -Vector3.Cross(currentCamera.transform.forward, Vector3.up).normalized;
    public Vector3 Forward => Vector3.Cross(currentCamera.transform.right, Vector3.up).normalized;
  }
}
