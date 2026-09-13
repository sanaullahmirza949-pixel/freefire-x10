using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    public Camera playerCamera;
    public Weapon equippedWeapon;

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    float rotationSpeed = 8f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (playerCamera == null) playerCamera = Camera.main;
    }

    void Update()
    {
        // Simple WASD / touch input for movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = playerCamera.transform.right;
        Vector3 desiredMove = (camForward * v + camRight * h).normalized;

        if (controller.isGrounded)
        {
            moveDirection = desiredMove * moveSpeed;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);

        // Rotate player to face movement direction if moving
        if (desiredMove.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(desiredMove);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotationSpeed);
        }

        // Shooting
        if (equippedWeapon != null)
        {
            if (Input.GetButton("Fire1"))
                equippedWeapon.TryShoot();
        }
    }
}
