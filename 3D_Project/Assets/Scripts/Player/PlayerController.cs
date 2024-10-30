using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


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
    public bool canLook = true;


    [Header("Jump")]
    public int jumpPower;

    [Header("View")]
    public Camera[] camera;
    public Action onOpenInventory;

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
        if (canLook)
        {
            Look();
        }
    }
    private void Move()
    {
        Vector3 dir = transform.forward * currentMoveInput.y + transform.right * currentMoveInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    public IEnumerator Run(float amount)
    {
        float originalMoveSpeed = moveSpeed;
        moveSpeed += amount;
        yield return new WaitForSeconds(8);
        moveSpeed = originalMoveSpeed;
    }

    private void Look()
    {
        camCurrentXRot += mouseDelta.y * lookSensitively;
        camCurrentXRot = Mathf.Clamp(camCurrentXRot, minXLook, maxXLook);
        transform.eulerAngles += new Vector3(-camCurrentXRot - transform.eulerAngles.x, mouseDelta.x * lookSensitively, 0);
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

    public void OnChangeView(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (camera[0].gameObject.activeSelf)
            {
                camera[0].gameObject.SetActive(false);
                camera[1].gameObject.SetActive(true);
            }
            else if (camera[1].gameObject.activeSelf)
            {
                camera[0].gameObject.SetActive(true);
                camera[1].gameObject.SetActive(false);
            }

        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            onOpenInventory?.Invoke();
            ToggleCursor();
        }
    }

    private void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

}
