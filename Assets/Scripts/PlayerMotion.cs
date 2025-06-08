using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMotion : MonoBehaviour {
  [Header("Configuration")]
  public float speed = 3;

  [Header("Information")]
  public Vector3 targetPosition;

  void Update () {
    targetPosition = transform.position;
    targetPosition += Input.GetAxis("Horizontal") * -Vector3.forward * 3;

    if (Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out RaycastHit hit, 3)) {
      transform.position = hit.point;
    }

    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
  }
}
