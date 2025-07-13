using UnityEngine;
using Controls;

namespace UI {
  public class UXControlContainer : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField] private Animator animator;

    void OnEnable () {
      UpdateDevice(DeviceDetector.CurrentDevice);
      DeviceDetector.OnDeviceChanged.AddListener(UpdateDevice);
    }

    void OnDisable () => DeviceDetector.OnDeviceChanged.RemoveListener(UpdateDevice);

    void UpdateDevice (DeviceDefinition currentDevice) {
      animator.SetInteger("device", DeviceDetector.CurrentDevice.animatorKey);
    }

    public void AnimEvent_OnHideEnd () => gameObject.SetActive(false);
  }
}
