using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Events;

namespace Controls {
  public class DeviceDetector : MonoBehaviour {
    private static DeviceDetector _instance;
    public static DeviceDetector Instance {
      get { if (!_instance) _instance = FindFirstObjectByType<DeviceDetector>(); return _instance; }
    }

    [SerializeField] private DeviceDefinition currentDevice;
    public static DeviceDefinition CurrentDevice =>
      !Instance.currentDevice? Instance.deviceDefinitions[0] : Instance.currentDevice;
    [SerializeField] private DeviceDefinition[] deviceDefinitions;

    void Awake () => _instance = this;
    void OnEnable () => InputSystem.onActionChange += HandleActionChange;
    void OnDisable () => InputSystem.onActionChange -= HandleActionChange;

    private void HandleActionChange (object arg1, InputActionChange change) {
      if (arg1 is not InputAction action || action == null || action.activeControl == null || action.activeControl.device == null) return;
      InputDevice device = action.activeControl.device;
      if (device.displayName == CurrentDevice.unityInputDeviceName) return;
      currentDevice = deviceDefinitions.FirstOrDefault(definition => definition.unityInputDeviceName == device.displayName);
      OnDeviceChanged?.Invoke(currentDevice);
    }


    public static UnityEvent<DeviceDefinition> OnDeviceChanged { get; private set; } = new();
  }
}
