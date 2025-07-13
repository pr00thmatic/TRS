using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Events;
using Shared;
using System.Collections.Generic;

namespace Controls {
  public class DeviceDetector : SingletonMonoBehaviour<DeviceDetector> {
    [Header("Configuration")]
    [SerializeField] private DeviceDefinition[] deviceDefinitions;

    [Header("Information")]
    [SerializeField] private DeviceDefinition currentDevice;
    public static DeviceDefinition CurrentDevice =>
      !Instance.currentDevice? Instance.deviceDefinitions[0] : Instance.currentDevice;
    private Dictionary<string, DeviceDefinition> nameToDeviceHash = new();

    override protected void Awake () {
      base.Awake();
      foreach (var definition in deviceDefinitions) nameToDeviceHash[definition.unityInputDeviceName] = definition;
      #if !UNITY_EDITOR
      deviceDefinitions = null;
      #endif
    }

    void OnEnable () => InputSystem.onActionChange += HandleActionChange;
    void OnDisable () => InputSystem.onActionChange -= HandleActionChange;

    private void HandleActionChange (object arg1, InputActionChange change) {
      if (arg1 is not InputAction action || action == null ||
          action.activeControl == null || action.activeControl.device == null) return;

      InputDevice device = action.activeControl.device;
      if (device.displayName == CurrentDevice.unityInputDeviceName) return;
      if (nameToDeviceHash.TryGetValue(device.displayName, out DeviceDefinition newDetectedDevice))
        currentDevice = newDetectedDevice;

      // TODO: give the player a warning to let them know idk what are they playing with
      if (currentDevice == null) Debug.LogWarning("Failed to identify player's controller!");

      OnDeviceChanged?.Invoke(CurrentDevice);
    }


    public static UnityEvent<DeviceDefinition> OnDeviceChanged { get; private set; } = new();
  }
}
