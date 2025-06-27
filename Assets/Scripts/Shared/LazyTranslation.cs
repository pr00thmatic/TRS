using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Shared {
  public class LazyTranslation : MonoBehaviour {
    public Transform target;
    public float speed = 5;
    public float angularSpeed = 360;

    void Update () {
      if (!target) return;
      transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, angularSpeed * Time.deltaTime);
    }
  }
}
