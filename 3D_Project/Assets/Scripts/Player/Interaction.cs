using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;
    public GameObject currentInteractGameObject;
    private IInteractable currentInteractable;
    public UIManager uIManager;


    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != currentInteractGameObject)
                {
                    currentInteractGameObject = hit.collider.gameObject;
                    currentInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }

            }
            else
            {
                currentInteractGameObject = null;
                currentInteractable = null;
                UIManager.Instance.HidePromptText();
            }
        }
    }

    private void SetPromptText()
    {
        if (currentInteractable != null)
        {
            UIManager.Instance.SetPromptText(currentInteractable.GetInteractPrompt());
        }
    }



    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && currentInteractable != null)
        {
            currentInteractable.OnInteract();
            currentInteractGameObject = null;
            currentInteractable = null;
            UIManager.Instance.HidePromptText();
        }
    }


}
