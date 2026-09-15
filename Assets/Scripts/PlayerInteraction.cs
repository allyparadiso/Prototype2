using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractionRaycast();
        gameManager.TriggerRandomEffect();
    }

    public void InteractionRaycast()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}
