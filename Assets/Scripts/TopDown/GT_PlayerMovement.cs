// using UnityEngine;

// public class GT_PlayerMovement : MonoBehaviour
// {
//     [SerializeField] private CharacterController controller;
//     [SerializeField] private PlayerInput input; // Reference to your input script
//     [SerializeField] private float moveSpeed = 5f;
//     [SerializeField] private float jumpHeight = 2f;
//     [SerializeField] private float gravity = -9.81f;

//     private Vector3 velocity; // Internal velocity for gravity/jumping

//     void Awake() { /* Get references if not serialized */ }

//     void Update()
//     {
//         // Input is read here or from PlayerInput
//         Vector3 moveDirection = new Vector3(input.MoveInput.x, 0, input.MoveInput.y);

//         // Apply movement based on input (world space, camera relative, etc.)
//         Vector3 actualMove = transform.TransformDirection(moveDirection) * moveSpeed;

//         // Handle gravity (velocity.y)
//         if (controller.isGrounded && velocity.y < 0) {
//             velocity.y = -2f; // Small constant force to keep grounded
//         }
//         velocity.y += gravity * Time.deltaTime;

//         // Apply movement + gravity. Move() handles sliding.
//         controller.Move((actualMove + velocity) * Time.deltaTime);

//         // Rotate if needed (handled by a separate rotation script or logic)
//     }
// }
