using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PMove : MonoBehaviour
{
    public float walkingSpeed = 7.5f, crouchSpeed = 4.5f, runningSpeed = 11.5f,jumpSpeed = 8.0f, gravity = 20.0f;
    public float lookSpeed = 2.0f, lookXLimit = 45.0f, rotationX = 0;
    CharacterController characterController;
    [SerializeField] Camera playerCamera;
    Vector3 moveDirection = Vector3.zero;

    public bool canMove,inside;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        inside = Physics.Raycast(transform.position, transform.up, 1f);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isCrouching = Input.GetKey(KeyCode.LeftControl) || inside;
        float curSpeedX = canMove ? (isRunning ? runningSpeed : isCrouching ? crouchSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : isCrouching ? crouchSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (canMove && isCrouching)
        {
            characterController.height = 0.7f;
        }
        else if (canMove && !inside)
        {
            characterController.height = 1.7f;
        }


        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
