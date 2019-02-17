using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 2.2f;
    public float CrouchSpeed = 1.2f;
    public float WalkSpeed = 2.2f;
    public float SprintSpeed = 20.3f;
    public float RotationSpeed = 2f;
    public float jumpForce = 4f;
    public GameObject eyes;

    CharacterController Player;

    private bool hasJumped, isCrouched, isSprinting;

    float moveFB;
    float moveLR;

    float rotX;
    float rotY;

    float vertVelocity;

    void Start()
    {
        Speed = WalkSpeed;
        Player = GetComponent<CharacterController>();
    }

    //Movement (FB,LR, rotate, Jump, Crouch, Sprint, Speed)
    void Update()
    {
        moveFB = Input.GetAxis("Vertical") * Speed;
        moveLR = Input.GetAxis("Horizontal") * Speed;

        rotX = Input.GetAxis("Mouse X") * RotationSpeed;
        rotY -= Input.GetAxis("Mouse Y") * RotationSpeed;

        rotY = Mathf.Clamp(rotY, -60f, 60f);

        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        transform.Rotate(0, rotX, 0);

        eyes.transform.localRotation = Quaternion.Euler(rotY, 0, 0);

        movement = transform.rotation * movement;
        Player.Move(movement * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            Speed = WalkSpeed;
            hasJumped = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            if (isCrouched == false)
            {
                Player.height = Player.height / 2;
                isCrouched = true;
                Speed = CrouchSpeed;
            }
            else
            {
                Player.height = Player.height * 2;
                isCrouched = false;
                Speed = WalkSpeed;
            }
        }

        if (Input.GetButtonDown("Sprint"))
        {
            if (isSprinting == false)
            {
                    isSprinting = true;
                    Speed = SprintSpeed;
            }
            else
            {
                isSprinting = false;
                Speed = WalkSpeed;
            }
        }

        ApplyGravity();

    }

    //Apply Gravity to player
    private void ApplyGravity()
    {
        if (Player.isGrounded == true)
        {
            if (hasJumped == false)
            {
                vertVelocity = Physics.gravity.y;

            }
            else
            {
                vertVelocity = jumpForce;
            }
        }
        else
        {
            vertVelocity += Physics.gravity.y * Time.deltaTime;
            vertVelocity = Mathf.Clamp(vertVelocity, -50f, jumpForce);
            hasJumped = false;
        }

    }
}