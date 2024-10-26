using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;


    [Header("Move")]
    public float moveSpeed;
    public LayerMask groundLayerMask;
    private Vector2 currentMoveInput;


    [Header("Look")]
    public float minXLook;
    private float camCurrentXRot;
    public float maxXLook;
    public float lookSensitively;
    private Vector2 mouseDelta;
    public Transform cameraContainer;


    [Header("Jump")]
    public int jumpPower;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
    }
    private void Move()
    {
        Vector3 dir = transform.forward * currentMoveInput.y + transform.right * currentMoveInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    private void Look()
    {
        camCurrentXRot += mouseDelta.y * lookSensitively;
        camCurrentXRot = Mathf.Clamp(camCurrentXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurrentXRot, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitively, 0);
    }

    private bool isGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            currentMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            currentMoveInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded())
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

}
