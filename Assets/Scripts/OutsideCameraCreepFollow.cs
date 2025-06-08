using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutsideCameraCreepFollow : MonoBehaviour {
  [Header("Configuration")]
  public Transform target;
  public float speed = 4;
  public float acceleration = 3;
  public float deccelerationDistance = 2;

  [Header("Information")]
  public Vector3 targetPosition;
  public Vector3 offset;
  public float currentSpeed = 0;
  public float distance;

  void Start () {
    offset = transform.position - target.position;
  }

  void Update () {
    targetPosition = target.position + offset;
    targetPosition.y = transform.position.y;
    currentSpeed = Mathf.Min(speed, currentSpeed + acceleration * Time.deltaTime);
    distance = (targetPosition - transform.position).magnitude;
    currentSpeed = Mathf.Lerp(0, currentSpeed, distance / deccelerationDistance);

    transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed);
  }
}
