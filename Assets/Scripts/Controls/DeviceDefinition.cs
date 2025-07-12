using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Controls {
  [CreateAssetMenu(fileName = "DeviceDefinition", menuName = "pr00/Controls/DeviceDefinition")]
  public class DeviceDefinition : ScriptableObject {
    public string unityInputDeviceName;
    public int animatorKey;
  }
}
