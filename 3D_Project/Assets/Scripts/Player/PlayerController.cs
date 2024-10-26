using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;


    [Header("Move")]
    public float moveSpeed;
    private Vector2 currentMoveInput;

    [Header("Look")]
    private float camCurrentXRot;
    public float minXLook;
    public float maxXLook;
    public float lookSensitively;
    private Vector2 mouseDelta;
    public Transform cameraContainer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
}
