using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSCameraContoller : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;

    [Header("Components Needed")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController CharaterController;
    [SerializeField] private Animator AnimationController;
    [SerializeField] private Transform Player;

    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Sensetivity;
    [SerializeField] private float Gravity = 9.81f;

    private bool seated;
    public bool Seated
    {
        get
        {
            return seated;
        }
        set
        {
            seated = value;
            AnimationController.SetFloat("XVector", 0);
            AnimationController.SetFloat("YVector", 0);
            AnimationController.SetFloat("ZVector", 0);
            AnimationController.SetBool("Seated", seated);
            CharaterController.enabled = !seated;

            if (seated)
                PlayerCamera.position -= new Vector3(0, 0.2f, 0);
            else
                PlayerCamera.position += new Vector3(0, 0.2f, 0);
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        if (!Seated)
        {
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * 2;
            MovePlayer();
        }

        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MoveCamera();

    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        if (CharaterController.isGrounded)
        {
            MoveVector.y = -1f;

            if (Input.GetKey(KeyCode.Space))
            {
                MoveVector.y = JumpForce;
            }
        }
        else
        {
            MoveVector.y -= Gravity * Gravity * Time.deltaTime;
        }

        CharaterController.Move(MoveVector * Time.deltaTime);

        AnimationController.SetFloat("XVector", PlayerMovementInput.x);
        AnimationController.SetFloat("YVector", PlayerMovementInput.y);
        AnimationController.SetFloat("ZVector", PlayerMovementInput.z);
    }
    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * Sensetivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, PlayerMouseInput.x * Sensetivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
